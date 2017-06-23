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
    public class ClientOptions : 
        ClientBase,
        IParentResourcePaging<Option>,
        IParentResourceGetUpdateDeleteCreate<Option>,
        IParentResourceCount
    {
        public ClientOptions(Configuration configuration, BCAuthentication authentication) : base(configuration, authentication) { }

        public async Task<IClientResponse<ItemCount>> CountAsync()
        {
            string resourceEndpoint = string.Format("options/count");
            return await CountAsync<ItemCount>(resourceEndpoint);
        }
        public async Task<IClientResponse<ItemCount>> CountAsync(IFilter filter)
        {
            string resourceEndpoint = string.Format("options/count");
            return await CountAsync<ItemCount>(resourceEndpoint, filter);
        }

        public async Task<IClientResponse<Option>> GetAsync(int id)
        {
            string resourceEndpoint = string.Format("options/{0}", id);
            return await GetDataAsync<Option>(resourceEndpoint);
        }
        public async Task<IClientResponse<Option>> GetAsync(int id, IFilter filter)
        {
            string resourceEndpoint = string.Format("options/{0}", id);
            return await GetDataAsync<Option>(resourceEndpoint, filter);
        }
        public async Task<IClientResponse<List<Option>>> GetAsync()
        {
            string resourceEndpoint = string.Format("options");
            return await GetDataAsync<List<Option>>(resourceEndpoint);
        }
        public async Task<IClientResponse<List<Option>>> GetAsync(IFilter filter)
        {
            string resourceEndpoint = string.Format("options");
            return await GetDataAsync<List<Option>>(resourceEndpoint, filter);
        }
        public async Task<IClientResponse<List<Option>>> GetAsync(string resourceEndPoint)
        {
            return await GetDataAsync<List<Option>>(resourceEndPoint);
        }
        public async Task<IClientResponse<List<Option>>> GetAsync(string resourceEndPoint, IFilter filter)
        {
            return await GetDataAsync<List<Option>>(resourceEndPoint, filter);
        }

        public async Task<IClientResponse<Option>> UpdateAsync(int id, string json)
        {
            string resourceEndpoint = string.Format("options/{0}", id);
            return await PutDataAsync<Option>(resourceEndpoint, json);
        }
        public async Task<IClientResponse<Option>> UpdateAsync(int id, object obj)
        {
            string resourceEndpoint = string.Format("options/{0}", id);
            return await PutDataAsync<Option>(resourceEndpoint, obj.SerializeObject());
        }

        public async Task<IClientResponse<Option>> CreateAsync(string json)
        {
            string resourceEndpoint = "options";
            return await PostDataAsync<Option>(resourceEndpoint, json);
        }
        public async Task<IClientResponse<Option>> CreateAsync(object obj)
        {
            string resourceEndpoint = "options";
            return await PostDataAsync<Option>(resourceEndpoint, obj.SerializeObject());
        }

        public async Task<IClientResponse<bool>> DeleteAsync(int id)
        {
            string resourceEndpoint = string.Format("options/{0}", id);
            return await DeleteDataAsync(resourceEndpoint);
        }

        public async Task<IClientResponse<HttpOptions>> GetHttpOptionsAsync()
        {
            string resourceEndpoint = string.Format("options");
            return await GetHttpOptionsDataAsync<HttpOptions>(resourceEndpoint);
        }
        public async Task<IClientResponse<HttpOptions>> GetHttpOptionsAsync(int id)
        {
            string resourceEndpoint = string.Format("options/{0}", id);
            return await GetHttpOptionsDataAsync<HttpOptions>(resourceEndpoint);
        }

        public async Task<IList<Option>> GetListAsync()
        {
            var filter = new FilterOptions();
            return await GetListAsync(filter);
        }
        public async Task<IList<Option>> GetListAsync(IFilter filter)
        {
            var items = await RecordPagingAsync(filter, this);
            return items;
        }
    }
}