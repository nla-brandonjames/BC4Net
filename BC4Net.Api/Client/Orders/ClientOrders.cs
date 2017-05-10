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
using System.Text;
using BigCommerce4Net.Domain;
using BigCommerce4Net.Api.ExtensionMethods;
using System.Threading.Tasks;

namespace BigCommerce4Net.Api.ResourceClients
{
    public class ClientOrders : 
        ClientBase, 
        IParentResourcePaging<Order>,
        IParentResourceGetUpdateDelete<Order>,
        IParentResourceCount
    {

        public ClientOrders(Configuration configuration, BCAuthentication authentication) : base(configuration, authentication) { }

        public async Task<IClientResponse<ItemCount>> CountAsync()
        {
            string resourceEndpoint = "orders/count";
            return await CountAsync<ItemCount>(resourceEndpoint);
        }
        public async Task<IClientResponse<ItemCount>> CountAsync(IFilter filter)
        {
            string resourceEndpoint = "orders/count";
            return await CountAsync<ItemCount>(resourceEndpoint, filter);
        }
        public async Task<IClientResponse<List<Order>>> GetAsync(IFilter filter)
        {
            string resourceEndpoint = "orders";
            return await GetDataAsync<List<Order>>(resourceEndpoint, filter);
        }
        public async Task<IClientResponse<Order>> GetAsync(int id)
        {
            string resourceEndpoint = $"orders/{id}";
            return await GetDataAsync<Order>(resourceEndpoint);
        }
        public async Task<IClientResponse<Order>> GetAsync(int id, IFilter filter)
        {
            string resourceEndpoint = $"orders/{id}";
            return await GetDataAsync<Order>(resourceEndpoint, filter);
        }
        public async Task<IClientResponse<List<Order>>> GetAsync(string resourceEndPoint)
        {
            return await GetDataAsync<List<Order>>(resourceEndPoint);
        }
        public async Task<IClientResponse<List<Order>>> GetAsync(string resourceEndPoint, IFilter filter)
        {
            return await GetDataAsync<List<Order>>(resourceEndPoint, filter);
        }

        public async Task<IClientResponse<Order>> UpdateAsync(int id, string json)
        {
            string resourceEndpoint = $"orders/{id}";
            return await PutDataAsync<Order>(resourceEndpoint, json);
        }
        public async Task<IClientResponse<Order>> UpdateAsync(int id, object obj)
        {
            string resourceEndpoint = $"orders/{id}";
            return await PutDataAsync<Order>(resourceEndpoint, obj.SerializeObject());
        }

        public async Task<IClientResponse<bool>> DeleteAsync(int id)
        {
            string resourceEndpoint = $"orders/{id}";
            return await DeleteDataAsync(resourceEndpoint);
        }

        public async Task<IClientResponse<HttpOptions>> GetHttpOptionsAsync()
        {
            string resourceEndpoint = $"orders";
            return await GetHttpOptionsDataAsync<HttpOptions>(resourceEndpoint);
        }
        public async Task<IClientResponse<HttpOptions>> GetHttpOptionsAsync(int id)
        {
            string resourceEndpoint = $"orders/{id}";
            return await GetHttpOptionsDataAsync<HttpOptions>(resourceEndpoint);
        }

        public async Task<IList<Order>> GetListAsync(int recordsPerPage = 250)
        {
            FilterOrders filter = new FilterOrders();
            return await GetListAsync(filter, recordsPerPage);
        }
        public async Task<IList<Order>> GetListAsync(IFilter filter, int recordsPerPage = 250)
        {
            var orders = await RecordPagingAsync<Order>(filter, this);
            return orders;
        }
    }
}
