#region License
//   Copyright 2013 Ken Worst - R.C. Worst & Company Inc.
//
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License. 
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;

namespace BigCommerce4Net.Api
{
    public abstract class ClientBase
    {
        protected readonly Configuration _Configuration;
        protected readonly BCAuthentication _Authentication;

        protected ClientBase(Configuration _configuration, BCAuthentication _authentication = null)
        {
            _configuration.AreConfigurationSet();
            _Configuration = _configuration;
            _Authentication = _authentication;
        }

        protected async Task<IClientResponse<T>> CountAsync<T>(string resourceEndpoint) where T : new()
        {
            return await CountAsync<T>(resourceEndpoint, null);
        }

        protected async Task<IClientResponse<T>> CountAsync<T>(string resourceEndpoint, IFilter filter) where T : new()
        {
            return await GetDataAsync<T>(resourceEndpoint, filter);
        }

        protected async Task<IClientResponse<T>> GetDataAsync<T>(string resourceEndpoint, IFilter filter = null) where T : new()
        {
            if (filter != null)
            {
                resourceEndpoint = filter.AddFilter(resourceEndpoint);
            }

            var request = new HttpRequestMessage();
            request.RequestUri = new Uri(new Uri(_Configuration.ServiceURL), resourceEndpoint);
            var clientResponse = await RestGetAsync<T>(request);

            DeserializeErrorData(clientResponse);
            return clientResponse;
        }

        protected async Task<IClientResponse<T>> PutDataAsync<T>(string resourceEndpoint, object updateData) where T : new()
        {
            var request = new HttpRequestMessage();
            request.RequestUri = new Uri(new Uri(_Configuration.ServiceURL), resourceEndpoint);
            request.Content = new StringContent(JsonConvert.SerializeObject(updateData), System.Text.Encoding.UTF8, "application/json");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var clientResponse = await RestPutAsync<T>(request);

            DeserializeErrorData(clientResponse);
            return clientResponse;
        }

        protected async Task<IClientResponse<T>> PostDataAsync<T>(string resourceEndpoint, object updateData) where T : new()
        {
            var request = new HttpRequestMessage();
            request.RequestUri = new Uri(new Uri(_Configuration.ServiceURL), resourceEndpoint);
            request.Content = new StringContent(JsonConvert.SerializeObject(updateData), System.Text.Encoding.UTF8, "application/json");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var clientResponse = await RestPostAsync<T>(request);

            DeserializeErrorData(clientResponse);
            return clientResponse;
        }

        protected async Task<IClientResponse<bool>> DeleteDataAsync(string resourceEndpoint)
        {
            IClientResponse<bool> clientResponse = null;

            //Just making sure you want to delete data --just for little extra safety
            if (_Configuration.AllowDeletions)
            {
                var request = new HttpRequestMessage();
                request.RequestUri = new Uri(new Uri(_Configuration.ServiceURL), resourceEndpoint);

                clientResponse = await RestDeleteAsync<bool>(request);
            }
            else
            {
                clientResponse = new ClientResponse<bool>()
                {
                    RestResponse = null,
                    Data = false
                };
            }
            DeserializeErrorData(clientResponse);
            return clientResponse;
        }

        protected async Task<IClientResponse<T>> GetHttpOptionsDataAsync<T>(string resourceEndpoint) where T : new()
        {
            var request = new HttpRequestMessage();
            request.RequestUri = new Uri(new Uri(_Configuration.ServiceURL), resourceEndpoint);

            var clientResponse = await RestOptionsAsync<T>(request);

            DeserializeErrorData(clientResponse);
            return clientResponse;
        }

        //Private Methods
        private void DeserializeErrorData<T>(IClientResponse<T> response)
        {

            switch (response.RestResponse.StatusCode)
            {
                case System.Net.HttpStatusCode.OK:
                case System.Net.HttpStatusCode.Created:
                case System.Net.HttpStatusCode.Accepted:
                case System.Net.HttpStatusCode.NoContent:
                    return;
            }

            try
            {
                response.ResponseErrors = JsonConvert.DeserializeObject<List<Domain.Error>>(response.RestResponse.Content.ToString());
            }
            catch (JsonSerializationException ex)
            {
                Console.WriteLine("Trouble Deserialize Error Object", ex);
                throw;
            }
        }

        private async Task<ClientResponse<T>> RestGetAsync<T>(HttpRequestMessage request) where T : new()
        {
            request.Method = HttpMethod.Get;

            return await RestExecuteAsync<T>(request);
        }

        private async Task<ClientResponse<T>> RestPutAsync<T>(HttpRequestMessage request) where T : new()
        {
            request.Method = HttpMethod.Put;

            return await RestExecuteAsync<T>(request);
        }

        private async Task<ClientResponse<T>> RestPostAsync<T>(HttpRequestMessage request) where T : new()
        {
            request.Method = HttpMethod.Post;

            return await RestExecuteAsync<T>(request);
        }

        private async Task<ClientResponse<T>> RestDeleteAsync<T>(HttpRequestMessage request) where T : new()
        {
            request.Method = HttpMethod.Delete;

            return await RestExecuteAsync<T>(request);
        }

        private async Task<ClientResponse<T>> RestOptionsAsync<T>(HttpRequestMessage request) where T : new()
        {
            request.Method = HttpMethod.Options;

            return await RestExecuteAsync<T>(request);
        }

        private async Task<ClientResponse<T>> RestExecuteAsync<T>(HttpRequestMessage request) where T : new()
        {
            var client = new HttpClient();

            request.Headers.Add("User-Agent", _Configuration.UserAgent);
            request.Headers.Add("Accept", "application/json");
            
            if (_Authentication == null)
            {
                var credentials = new System.Net.NetworkCredential(_Configuration.UserName, _Configuration.UserApiKey);
                var handler = new HttpClientHandler { Credentials = credentials };
                client = new HttpClient(handler);
            }
            else
            {
                request.Headers.Add("X-Auth-Client", _Configuration.UserName);
                request.Headers.Add("X-Auth-Token", _Authentication.AccessToken);
            }
            
            client.Timeout = TimeSpan.FromSeconds(_Configuration.RequestTimeout);

            var response = await client.SendAsync(request);
            var responseStringContent = await response.Content.ReadAsStringAsync();
            //throw new Exception("url: " + request.RequestUri + "\ncontent: " + responseStringContent);

            var clientResponse = new ClientResponse<T>
            {
                Data = JsonConvert.DeserializeObject<T>(responseStringContent),
                RestResponse = response
            };

            CheckForThrottling(response);

            return clientResponse;
        }

        private void CheckForThrottling(HttpResponseMessage response)
        {
            if (_Configuration.RequestThrottling == true)
            {
                var head = response.Headers.Where(x => x.Key == "X-BC-ApiLimit-Remaining").FirstOrDefault();
                if (head.Value != null)
                {
                    bool wasParsed = int.TryParse(head.Value.ToString(), out int limitvalue);
                    if (wasParsed)
                    {
                        if (limitvalue <= 1000)
                        {
                            Console.WriteLine("------ Throttling Enabled Until Request Limit Gets About 1000 ------");
                            Task.Delay(_Configuration.RequestThrottlingDelay);
                        }
                    }
                }
            }
        }

        protected static void ShowIdAndApiLimit(object id, HttpResponseMessage restResponse)
        {
            var apiLimit = restResponse.Headers.Where(x => x.Key == "X-BC-ApiLimit-Remaining").FirstOrDefault().Value;
            Console.WriteLine("Id {0} -- API Limit: {1}", id, apiLimit);
        }

        protected void StatusCodeLogging(HttpResponseMessage response, Type type)
        {
            if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                Console.WriteLine("[{0}] - Http Status Code: {1}", type.Name, (int)response.StatusCode);
            }
            else
            {
                Console.WriteLine("[{0}] - Http Status Code: {1}", type.Name, (int)response.StatusCode);
            }
        }

        protected async Task<List<T>> RecordPagingAsync<T>(IFilter filter, IParentResourcePaging<T> client)
        {
            List<T> items = new List<T>();

            int itemsCount = 0;
            int pageCount = 0;
            int remainingCount = 0;
            int recordsPerPage;

            recordsPerPage = (_Configuration.RecordsPerPage > _Configuration.MaxPageLimit)
                    ? _Configuration.MaxPageLimit : _Configuration.RecordsPerPage;

            itemsCount = (await client.CountAsync(filter)).Data.Count;
            pageCount = itemsCount / recordsPerPage;
            remainingCount = itemsCount % recordsPerPage;

            for (int i = 1; i <= pageCount; i++)
            {
                filter.Page = i;
                filter.Limit = recordsPerPage;

                int retrys = 0;
                do
                {
                    var response = await client.GetAsync(filter);
                    if (response.RestResponse.StatusCode == System.Net.HttpStatusCode.OK && response.Data != null)
                    {
                        items.AddRange(response.Data as List<T>);
                        Console.WriteLine(GetPagingStatus(response.RestResponse, filter.Page, items.Count));
                        retrys = int.MaxValue;
                    }
                    else
                    {
                        retrys++;
                        Console.WriteLine(GetErrorStatus(response.RestResponse));
                        await Task.Delay(_Configuration.ErrorRetryDelay);
                    }

                } while (retrys <= _Configuration.ErrorRetryMax);

                if (retrys != int.MaxValue)
                {
                    throw new HttpServerException("Http Server not responding after retries");
                }
            }

            if (remainingCount > 0)
            {
                filter.Page = pageCount + 1;
                filter.Limit = recordsPerPage;

                int retrys = 0;
                do
                {
                    var response = await client.GetAsync(filter);
                    if (response.RestResponse.StatusCode == System.Net.HttpStatusCode.OK && response.Data != null)
                    {

                        items.AddRange(response.Data as List<T>);

                        Console.WriteLine(GetPagingStatus(response.RestResponse, filter.Page, items.Count));
                        retrys = int.MaxValue;
                    }
                    else
                    {
                        retrys++;
                        Console.WriteLine(GetErrorStatus(response.RestResponse));
                        await Task.Delay(_Configuration.ErrorRetryDelay);
                    }

                } while (retrys <= _Configuration.ErrorRetryMax);

                if (retrys != int.MaxValue)
                {
                    throw new HttpServerException("Http Server not responding after retries");
                }
            }
            return items;
        }

        private static string GetErrorStatus(HttpResponseMessage response)
        {
            string str = string.Format("Http Status Code: {0} - URL: {1}",
                            (int)response.StatusCode,
                            response.RequestMessage.RequestUri.AbsoluteUri);
            return str;
        }

        private static string GetPagingStatus(HttpResponseMessage response, int? page, int count)
        {
            if (page == null)
            {
                page = 0;
            }
            string str = string.Format("Page {0} Record Count {1} -- API Limit: {2}", page, count,
                        response.Headers.Where(x => x.Key == "X-BC-ApiLimit-Remaining").FirstOrDefault().Value);

            return str;
        }
        //private static readonly log4net.ILog log = log4net.LogManager.GetLogger();
    }
}
