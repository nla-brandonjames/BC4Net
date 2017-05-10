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
using BigCommerce4Net.Api.ExtensionMethods;
using System.Threading.Tasks;

namespace BigCommerce4Net.Api.ResourceClients
{
    public class ClientProducts : 
        ClientBase, 
        IParentResourcePaging<Product>, 
        IParentResourceGetUpdateDeleteCreate<Product>,
        IParentResourceCount
    {
        public ClientProducts(Configuration configuration, BCAuthentication authentication) : base(configuration, authentication) { }

        public async Task<IClientResponse<ItemCount>> CountAsync()
        {
            string resourceEndpoint = "products/count";
            return await CountAsync<ItemCount>(resourceEndpoint);
        }
        public async Task<IClientResponse<ItemCount>> CountAsync(IFilter filter)
        {
            string resourceEndpoint = "products/count";
            return await CountAsync<ItemCount>(resourceEndpoint, filter);
        }
        public async Task<IClientResponse<List<Product>>> GetAsync(IFilter filter)
        {
            string resourceEndpoint = "products";
            return await GetDataAsync<List<Product>>(resourceEndpoint, filter);
        }
        public async Task<IClientResponse<Product>> GetAsync(int id)
        {
            string resourceEndpoint = $"products/{id}";
            return await GetDataAsync<Product>(resourceEndpoint);
        }
        public async Task<IClientResponse<Product>> GetAsync(int id, IFilter filter)
        {
            string resourceEndpoint = $"products/{id}";
            return await GetDataAsync<Product>(resourceEndpoint, filter);
        }
        public async Task<IClientResponse<List<Product>>> GetAsync(string resourceEndPoint)
        {
            return await GetDataAsync<List<Product>>(resourceEndPoint);
        }
        public async Task<IClientResponse<List<Product>>> GetAsync(string resourceEndPoint, IFilter filter)
        {
            return await GetDataAsync<List<Product>>(resourceEndPoint, filter);
        }

        public async Task<IClientResponse<Product>> UpdateAsync(int id, string json)
        {
            string resourceEndpoint = $"products/{id}";
            return await PutDataAsync<Product>(resourceEndpoint, json);
        }
        public async Task<IClientResponse<Product>> UpdateAsync(int id, object obj)
        {
            string resourceEndpoint = $"products/{id}";
            return await PutDataAsync<Product>(resourceEndpoint, obj.SerializeObject());
        }

        public async Task<IClientResponse<Product>> CreateAsync(string json)
        {
            string resourceEndpoint = "products";
            return await PostDataAsync<Product>(resourceEndpoint, json);
        }
        public async Task<IClientResponse<Product>> CreateAsync(object obj)
        {
            string resourceEndpoint = "products";
            return await PostDataAsync<Product>(resourceEndpoint, obj.SerializeObject());
        }

        public async Task<IClientResponse<bool>> DeleteAsync(int id)
        {
            string resourceEndpoint = $"products/{id}";
            return await DeleteDataAsync(resourceEndpoint);
        }

        public async Task<IClientResponse<HttpOptions>> GetHttpOptionsAsync()
        {
            var resourceEndpoint = "products";
            return await GetHttpOptionsDataAsync<HttpOptions>(resourceEndpoint);
        }
        public async Task<IClientResponse<HttpOptions>> GetHttpOptionsAsync(int id)
        {
            string resourceEndpoint = $"products/{id}";
            return await GetHttpOptionsDataAsync<HttpOptions>(resourceEndpoint);
        }

        public async Task<IList<Product>> GetListAsync()
        {
            FilterProducts filter = new FilterProducts();
            return await GetListAsync(filter);
        }
        public async Task<IList<Product>> GetListAsync(IFilter filter)
        {
            var items = await RecordPagingAsync(filter, this);
            return items;
        }
    }
}
