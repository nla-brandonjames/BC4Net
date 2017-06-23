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

using System.Collections.Generic;
using BigCommerce4Net.Domain;
using BigCommerce4Net.Api.ExtensionMethods;
using System.Threading.Tasks;

namespace BigCommerce4Net.Api.ResourceClients
{
    public class ClientCustomers : 
        ClientBase, 
        IParentResourcePaging<Customer>, 
        IParentResourceGetUpdateDeleteCreate<Customer>, 
        IParentResourceCount
    {
        public ClientCustomers(Configuration configuration, BCAuthentication authentication) : base(configuration, authentication) { }

        public async Task<IClientResponse<ItemCount>> CountAsync()
        {
            string resourceEndpoint = "customers/count";
            return await CountAsync<ItemCount>(resourceEndpoint);
        }
        public async Task<IClientResponse<ItemCount>> CountAsync(IFilter filter)
        {
            string resourceEndpoint = "customers/count";
            return await CountAsync<ItemCount>(resourceEndpoint, filter);
        }
        public async Task<IClientResponse<List<Customer>>> GetAsync(IFilter filter)
        {
            string resourceEndpoint = "customers";
            return await GetDataAsync<List<Customer>>(resourceEndpoint, filter);
        }
        public async Task<IClientResponse<Customer>> GetAsync(int id)
        {
            string resourceEndpoint = string.Format("customers/{0}", id);
            return await GetDataAsync<Customer>(resourceEndpoint, null);
        }
        public async Task<IClientResponse<Customer>> GetAsync(int id, IFilter filter)
        {
            string resourceEndpoint = string.Format("customers/{0}", id);
            return await GetDataAsync<Customer>(resourceEndpoint, filter);
        }
        public async Task<IClientResponse<List<Customer>>> GetAsync(string resourceEndPoint)
        {
            return await GetDataAsync<List<Customer>>(resourceEndPoint);
        }
        public async Task<IClientResponse<List<Customer>>> GetAsync(string resourceEndPoint, IFilter filter)
        {
            return await GetDataAsync<List<Customer>>(resourceEndPoint, filter);
        }

        public async Task<IClientResponse<Customer>> UpdateAsync(int Id, string json)
        {
            string resourceEndpoint = string.Format("customers/{0}", Id);
            return await PutDataAsync<Customer>(resourceEndpoint, json);
        }
        public async Task<IClientResponse<Customer>> UpdateAsync(int Id, object obj)
        {
            string resourceEndpoint = string.Format("customers/{0}", Id);
            return await PutDataAsync<Customer>(resourceEndpoint, obj.SerializeObject());
        }

        public async Task<IClientResponse<Customer>> CreateAsync(string json)
        {
            string resourceEndpoint = string.Format("customers");
            return await PostDataAsync<Customer>(resourceEndpoint, json);
        }
        public async Task<IClientResponse<Customer>> CreateAsync(object obj)
        {
            string resourceEndpoint = string.Format("customers");
            return await PostDataAsync<Customer>(resourceEndpoint, obj.SerializeObject());
        }

        public async Task<IClientResponse<bool>> DeleteAsync(int Id)
        {
            string resourceEndpoint = string.Format("customers/{0}", Id);
            return await DeleteDataAsync(resourceEndpoint);
        }

        public async Task<IClientResponse<HttpOptions>> GetHttpOptionsAsync(int id)
        {
            string resourceEndpoint = string.Format("customers/{0}", id);
            return await GetHttpOptionsDataAsync<HttpOptions>(resourceEndpoint);
        }
        public async Task<IClientResponse<HttpOptions>> GetHttpOptionsAsync()
        {
            string resourceEndpoint = string.Format("customers");
            return await GetHttpOptionsDataAsync<HttpOptions>(resourceEndpoint);
        }

        public async Task<List<Customer>> GetListAsync()
        {
            FilterCustomers filter = new FilterCustomers();
            return await GetListAsync(filter);
        }
        public async Task<List<Customer>> GetListAsync(IFilter filter)
        {
            var items = await RecordPagingAsync(filter, this);
            return items;
        }
    }
}
