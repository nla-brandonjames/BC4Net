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
using BigCommerce4Net.Domain;
using System.Threading.Tasks;

namespace BigCommerce4Net.Api.ResourceClients
{
    public class ClientOrderStatuses :
        ClientBase,
        IParentResourceGet<OrderStatus>
    {
        public ClientOrderStatuses(Configuration configuration, BCAuthentication authentication) : base(configuration, authentication) { }

        public async Task<IClientResponse<List<OrderStatus>>> GetAsync()
        {
            string resourceEndpoint = "order_statuses";
            return await GetDataAsync<List<OrderStatus>>(resourceEndpoint);
        }
        public async Task<IClientResponse<List<OrderStatus>>> GetAsync(IFilter filter)
        {
            string resourceEndpoint = "order_statuses";
            return await GetDataAsync<List<OrderStatus>>(resourceEndpoint, filter);
        }
        public async Task<IClientResponse<OrderStatus>> GetAsync(int id)
        {
            string resourceEndpoint = string.Format("order_statuses/{0}", id);
            return await GetDataAsync<OrderStatus>(resourceEndpoint);
        }
        public async Task<IClientResponse<OrderStatus>> GetAsync(int id, IFilter filter)
        {
            string resourceEndpoint = string.Format("order_statuses/{0}", id);
            return await GetDataAsync<OrderStatus>(resourceEndpoint, filter);
        }
        public async Task<IClientResponse<List<OrderStatus>>> GetAsync(string resourceEndPoint)
        {
            return await GetDataAsync<List<OrderStatus>>(resourceEndPoint);
        }
        public async Task<IClientResponse<List<OrderStatus>>> GetAsync(string resourceEndPoint, IFilter filter)
        {
            return await GetDataAsync<List<OrderStatus>>(resourceEndPoint, filter);
        }
        public async Task<IClientResponse<HttpOptions>> GetHttpOptionsAsync()
        {
            string resourceEndpoint = string.Format("order_statuses");
            return await GetHttpOptionsDataAsync<HttpOptions>(resourceEndpoint);
        }
        public async Task<IClientResponse<HttpOptions>> GetHttpOptionsAsync(int id)
        {
            string resourceEndpoint = string.Format("order_statuses/{0}", id);
            return await GetHttpOptionsDataAsync<HttpOptions>(resourceEndpoint);
        }

        public IList<OrderStatus> GetList()
        {

            List<OrderStatus> items = new List<OrderStatus>();
            var response = GetAsync();
            if (response.Result.RestResponse.StatusCode == System.Net.HttpStatusCode.OK &&
                response.Result.Data != null && response.Result.Data != null)
            {
                items.AddRange(response.Result.Data);
            }
            else
            {
                StatusCodeLogging(response.Result.RestResponse, GetType());
            }
            return items;
        }
    }
}
