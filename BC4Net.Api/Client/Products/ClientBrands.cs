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
    public class ClientBrands :
        ClientBase,
        IParentResourcePaging<Brand>,
        IParentResourceGetUpdateDeleteCreate<Brand>,
        IParentResourceCount
    {
        public ClientBrands(Configuration configuration, BCAuthentication authentication) : base(configuration, authentication) { }

        public async Task<IClientResponse<ItemCount>> CountAsync()
        {
            string resourceEndpoint = "brands/count";
            return await CountAsync<ItemCount>(resourceEndpoint);
        }
        public async Task<IClientResponse<ItemCount>> CountAsync(IFilter filter)
        {
            string resourceEndpoint = "brands/count";
            return await CountAsync<ItemCount>(resourceEndpoint, filter);
        }
        public async Task<IClientResponse<List<Brand>>> GetAsync(IFilter filter)
        {
            string resourceEndpoint = "brands";
            return await GetDataAsync<List<Brand>>(resourceEndpoint, filter);
        }
        public async Task<IClientResponse<Brand>> GetAsync(int id, IFilter filter)
        {
            string resourceEndpoint = $"brands/{id}";
            return await GetDataAsync<Brand>(resourceEndpoint, filter);
        }
        public async Task<IClientResponse<Brand>> GetAsync(int id)
        {
            string resourceEndpoint = $"brands/{id}";
            return await GetDataAsync<Brand>(resourceEndpoint);
        }
        public async Task<IClientResponse<List<Brand>>> GetAsync(string resourceEndPoint)
        {
            return await GetDataAsync<List<Brand>>(resourceEndPoint);
        }
        public async Task<IClientResponse<List<Brand>>> GetAsync(string resourceEndPoint, IFilter filter)
        {
            return await GetDataAsync<List<Brand>>(resourceEndPoint, filter);
        }

        public async Task<IClientResponse<Brand>> UpdateAsync(int id, string json)
        {
            string resourceEndpoint = $"brands/{id}";
            return await PutDataAsync<Brand>(resourceEndpoint, json);
        }
        public async Task<IClientResponse<Brand>> UpdateAsync(int id, object obj)
        {
            string resourceEndpoint = $"brands/{id}";
            return await PutDataAsync<Brand>(resourceEndpoint, obj.SerializeObject());
        }

        public async Task<IClientResponse<Brand>> CreateAsync(string json)
        {
            string resourceEndpoint = "brands";
            return await PostDataAsync<Brand>(resourceEndpoint, json);
        }
        public async Task<IClientResponse<Brand>> CreateAsync(object obj)
        {
            string resourceEndpoint = "brands";
            return await PostDataAsync<Brand>(resourceEndpoint, obj.SerializeObject());
        }

        public async Task<IClientResponse<bool>> DeleteAsync(int id)
        {
            string resourceEndpoint = $"brands/{id}";
            return await DeleteDataAsync(resourceEndpoint);
        }

        public async Task<IClientResponse<HttpOptions>> GetHttpOptionsAsync()
        {
            string resourceEndpoint = "brands";
            return await GetHttpOptionsDataAsync<HttpOptions>(resourceEndpoint);
        }
        public async Task<IClientResponse<HttpOptions>> GetHttpOptionsAsync(int id)
        {
            string resourceEndpoint = $"brands/{id}";
            return await GetHttpOptionsDataAsync<HttpOptions>(resourceEndpoint);
        }

        public async Task<IList<Brand>> GetListAsync()
        {
            FilterOrders filter = new FilterOrders();
            return await GetListAsync(filter);
        }
        public async Task<IList<Brand>> GetListAsync(IFilter filter)
        {
            var items = await RecordPagingAsync<Brand>(filter, this);
            return items;
        }
    }
}