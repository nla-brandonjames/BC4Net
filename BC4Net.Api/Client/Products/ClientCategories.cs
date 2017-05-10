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
    public class ClientCategories : 
        ClientBase, 
        IParentResourcePaging<Category>, 
        IParentResourceGetUpdateDeleteCreate<Category>,
        IParentResourceCount
    {
        public ClientCategories(Configuration configuration, BCAuthentication authentication) : base(configuration, authentication) { }

        public async Task<IClientResponse<ItemCount>> CountAsync()
        {
            string resourceEndpoint = "categories/count";
            return await CountAsync<ItemCount>(resourceEndpoint);
        }
        public async Task<IClientResponse<ItemCount>> CountAsync(IFilter filter)
        {
            string resourceEndpoint = "categories/count";
            return await CountAsync<ItemCount>(resourceEndpoint, filter);
        }
        public async Task<IClientResponse<List<Category>>> GetAsync(IFilter filter)
        {
            string resourceEndpoint = "categories";
            return await GetDataAsync<List<Category>>(resourceEndpoint, filter);
        }
        public async Task<IClientResponse<Category>> GetAsync(int id)
        {
            string resourceEndpoint = string.Format("categories/{0}", id);
            return await GetDataAsync<Category>(resourceEndpoint);
        }
        public async Task<IClientResponse<Category>> GetAsync(int id, IFilter filter)
        {
            string resourceEndpoint = string.Format("categories/{0}", id);
            return await GetDataAsync<Category>(resourceEndpoint, filter);
        }
        public async Task<IClientResponse<List<Category>>> GetAsync(string resourceEndPoint)
        {
            return await GetDataAsync<List<Category>>(resourceEndPoint);
        }
        public async Task<IClientResponse<List<Category>>> GetAsync(string resourceEndPoint, IFilter filter)
        {
            return await GetDataAsync<List<Category>>(resourceEndPoint, filter);
        }

        public async Task<IClientResponse<Category>> UpdateAsync(int id, string json)
        {
            string resourceEndpoint = string.Format("categories/{0}", id);
            return await PutDataAsync<Category>(resourceEndpoint, json);
        }
        public async Task<IClientResponse<Category>> UpdateAsync(int id, object obj)
        {
            string resourceEndpoint = string.Format("categories/{0}", id);
            return await PutDataAsync<Category>(resourceEndpoint, obj.SerializeObject());
        }

        public async Task<IClientResponse<Category>> CreateAsync(string json)
        {
            string resourceEndpoint = "categories";
            return await PostDataAsync<Category>(resourceEndpoint, json);
        }
        public async Task<IClientResponse<Category>> CreateAsync(object obj)
        {
            string resourceEndpoint = "categories";
            return await PostDataAsync<Category>(resourceEndpoint, obj.SerializeObject());
        }

        public async Task<IClientResponse<bool>> DeleteAsync(int id)
        {
            string resourceEndpoint = string.Format("categories/{0}", id);
            return await DeleteDataAsync(resourceEndpoint);
        }

        public async Task<IClientResponse<HttpOptions>> GetHttpOptionsAsync()
        {
            string resourceEndpoint = string.Format("categories");
            return await GetHttpOptionsDataAsync<HttpOptions>(resourceEndpoint);
        }
        public async Task<IClientResponse<HttpOptions>> GetHttpOptionsAsync(int id)
        {
            string resourceEndpoint = string.Format("categories/{0}", id);
            return await GetHttpOptionsDataAsync<HttpOptions>(resourceEndpoint);
        }


        public async Task<IList<Category>> GetListAsync()
        {
            var filter = new FilterCategories();
            return await GetListAsync(filter);
        }
        public async Task<IList<Category>> GetListAsync(IFilter filter)
        {
            var items = await RecordPagingAsync(filter, this);
            return items;
        }
    }
}
