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
using RestSharp.Portable;
using Newtonsoft.Json;
using RestSharp.Portable.HttpClient;
using RestSharp.Portable.Authenticators;
using System.Threading.Tasks;
using System.Collections;

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
            var request = new RestRequest(resourceEndpoint);
            if (filter != null)
            {
                filter.AddFilter(request);
            }

            var response = RestGetAsync<T>(request);

            var clientResponse = new ClientResponse<T>()
            {
                RestResponse = await response,
            };

            if (response.Result.Data != null)
            {
                clientResponse.Data = response.Result.Data;
            }

            DeserializeErrorData(clientResponse);
            return clientResponse as IClientResponse<T>;
        }

        protected async Task<IClientResponse<T>> GetDataAsync<T>(string resourceEndpoint, IFilter filter = null) where T : new()
        {
            var request = new RestRequest(resourceEndpoint);

            if (filter != null)
            {
                filter.AddFilter(request);
            }

            var response = RestGetAsync<T>(request);

            var clientResponse = new ClientResponse<T>()
            {
                RestResponse = await response,
            };
            if (response.Result.Data != null)
            {
                clientResponse.Data = response.Result.Data;
            }

            DeserializeErrorData(clientResponse);
            return clientResponse;
        }

        protected async Task<IClientResponse<T>> PutDataAsync<T>(string resourceEndpoint, string json) where T : new()
        {
            var request = new RestRequest(resourceEndpoint);

            request.AddParameter("application/json", json, ParameterType.RequestBody);

            var response = RestPutAsync<T>(request);

            var clientResponse = new ClientResponse<T>()
            {
                RestResponse = await response,
                Data = response.Result.Data
            };

            DeserializeErrorData(clientResponse);
            return clientResponse;
        }

        protected async Task<IClientResponse<T>> PostDataAsync<T>(string resourceEndpoint, string json) where T : new()
        {
            var request = new RestRequest(resourceEndpoint);

            request.AddParameter("application/json", json, ParameterType.RequestBody);

            var response = RestPostAsync<T>(request);

            var clientResponse = new ClientResponse<T>()
            {
                RestResponse = await response,
                Data = response.Result.Data
            };

            DeserializeErrorData(clientResponse);
            return clientResponse;
        }

        protected async Task<IClientResponse<bool>> DeleteDataAsync(string resourceEndpoint)
        {
            IClientResponse<bool> clientResponse = null;

            //Just making sure you want to delete data --just for little extra safety
            if (_Configuration.AllowDeletions)
            {

                var request = new RestRequest(resourceEndpoint);

                var response = RestDeleteAsync<object>(request);

                clientResponse = new ClientResponse<bool>()
                {
                    RestResponse = await response,
                    Data = response.Result.StatusCode == System.Net.HttpStatusCode.NoContent ? true : false
                };

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
            var request = new RestRequest(resourceEndpoint);

            var response = RestOptionsAsync<T>(request);

            var clientResponse = new ClientResponse<T>()
            {
                RestResponse = await response,
                Data = response.Result.Data
            };

            DeserializeErrorData(clientResponse);
            return clientResponse;
        }

        //Private Methods
        private void DeserializeErrorData<T>(IClientResponse<T> response)
        {

            if (response.RestResponse.StatusCode == System.Net.HttpStatusCode.OK) return;
            if (response.RestResponse.StatusCode == System.Net.HttpStatusCode.Created) return;
            if (response.RestResponse.StatusCode == System.Net.HttpStatusCode.Accepted) return;
            if (response.RestResponse.StatusCode == System.Net.HttpStatusCode.NoContent) return;

            try
            {
                response.ResponseErrors = JsonConvert.DeserializeObject<List<Domain.Error>>(response.RestResponse.Content);
            }
            catch (JsonSerializationException ex)
            {
                Console.WriteLine("Trouble Deserialize Error Object", ex);
                throw;
            }
        }

        private async Task<IRestResponse<T>> RestGetAsync<T>(IRestRequest request) where T : new()
        {
            request.Method = Method.GET;

            return await RestExecuteAsync<T>(request);
        }

        private async Task<IRestResponse<T>> RestPutAsync<T>(IRestRequest request) where T : new()
        {
            request.Method = Method.PUT;

            return await RestExecuteAsync<T>(request);
        }

        private async Task<IRestResponse<T>> RestPostAsync<T>(IRestRequest request) where T : new()
        {
            request.Method = Method.POST;

            return await RestExecuteAsync<T>(request);
        }

        private async Task<IRestResponse<T>> RestDeleteAsync<T>(IRestRequest request) where T : new()
        {
            request.Method = Method.DELETE;

            return await RestExecuteAsync<T>(request);
        }

        private async Task<IRestResponse<T>> RestOptionsAsync<T>(IRestRequest request) where T : new()
        {
            request.Method = Method.OPTIONS;

            return await RestExecuteAsync<T>(request);
        }

        private async Task<IRestResponse<T>> RestExecuteAsync<T>(IRestRequest request) where T : new()
        {
            request.AddParameter("Accept", "application/json", ParameterType.HttpHeader);
            request.AddParameter("User-Agent", _Configuration.UserAgent, ParameterType.HttpHeader);

            var client = new RestClient(_Configuration.ServiceURL);

            if (_Authentication == null)
            {
                client.Authenticator = new HttpBasicAuthenticator(_Configuration.UserName, _Configuration.UserApiKey);            
            }
            else
            {
                request.AddParameter("X-Auth-Client", _Configuration.UserName, ParameterType.HttpHeader);
			    request.AddParameter("X-Auth-Token", _Authentication.AccessToken, ParameterType.HttpHeader);
            }

            client.Timeout = TimeSpan.FromSeconds(_Configuration.RequestTimeout);

            client.ContentHandlers["application/json"] = new Deserializers.NewtonSoftJsonDeserializer();

            var response = await client.Execute<T>(request);

            CheckForThrottling(response);

            return response;
        }

        private void CheckForThrottling(IRestResponse response)
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

        protected static void ShowIdAndApiLimit(object id, IRestResponse restResponse)
        {
            var apiLimit = restResponse.Headers.Where(x => x.Key == "X-BC-ApiLimit-Remaining").FirstOrDefault().Value;
            Console.WriteLine("Id {0} -- API Limit: {1}", id, apiLimit);
        }

        protected void StatusCodeLogging(IRestResponse response, Type type)
        {
            if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                Console.WriteLine("[{0}] - Http Status Code: {1} - {2}", type.Name, (int)response.StatusCode, response.StatusDescription);
            }
            else
            {
                Console.WriteLine("[{0}] - Http Status Code: {1} - {2}", type.Name, (int)response.StatusCode, response.StatusDescription);
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

        private static string GetErrorStatus(IRestResponse response)
        {
            string str = string.Format("Http Status Code: {0} - {1} URL: {2}",
                            (int)response.StatusCode,
                            response.StatusDescription,
                            response.ResponseUri.AbsoluteUri);
            return str;
        }

        private static string GetPagingStatus(IRestResponse response, int? page, int count)
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
