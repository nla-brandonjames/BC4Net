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
    public class ClientOptionSetOption : 
        ClientBase,
        IParentResourcePaging<OptionSetOption>, 
        IParentResourceGetUpdateDeleteCreate<OptionSetOption>,
        IParentResourceCount
    {
        public ClientOptionSetOption(Configuration configuration, BCAuthentication authentication) : base(configuration, authentication) { }

        public async Task<IClientResponse<ItemCount>> CountAsync()
        {
            string resourceEndpoint = string.Format("optionsets/options/count");
            return await CountAsync<ItemCount>(resourceEndpoint);
        }
        public async Task<IClientResponse<ItemCount>> CountAsync(IFilter filter)
        {
            string resourceEndpoint = string.Format("optionsets/options/count");
            return await CountAsync<ItemCount>(resourceEndpoint, filter);
        }

        public async Task<IClientResponse<OptionSetOption>> GetAsync(int id)
        {
            string resourceEndpoint = string.Format("optionsets/options/{0}", id);
            return await GetDataAsync<OptionSetOption>(resourceEndpoint);
        }
        public async Task<IClientResponse<OptionSetOption>> GetAsync(int id, IFilter filter)
        {
            string resourceEndpoint = string.Format("optionsets/options/{0}", id);
            return await GetDataAsync<OptionSetOption>(resourceEndpoint, filter);
        }
        public async Task<IClientResponse<List<OptionSetOption>>> GetAsync()
        {
            string resourceEndpoint = string.Format("optionsets/options");
            return await GetDataAsync<List<OptionSetOption>>(resourceEndpoint);
        }
        public async Task<IClientResponse<List<OptionSetOption>>> GetAsync(IFilter filter)
        {
            string resourceEndpoint = string.Format("optionsets/options");
            return await GetDataAsync<List<OptionSetOption>>(resourceEndpoint, filter);
        }
        public async Task<IClientResponse<List<OptionSetOption>>> GetAsync(string resourceEndPoint)
        {
            return await GetDataAsync<List<OptionSetOption>>(resourceEndPoint);
        }
        public async Task<IClientResponse<List<OptionSetOption>>> GetAsync(string resourceEndPoint, IFilter filter)
        {
            return await GetDataAsync<List<OptionSetOption>>(resourceEndPoint, filter);
        }

        public async Task<IClientResponse<OptionSetOption>> UpdateAsync(int id, string json)
        {
            string resourceEndpoint = string.Format("optionsets/options/{0}", id);
            return await PutDataAsync<OptionSetOption>(resourceEndpoint, json);
        }
        public async Task<IClientResponse<OptionSetOption>> UpdateAsync(int id, object obj)
        {
            string resourceEndpoint = string.Format("optionsets/options/{0}", id);
            return await PutDataAsync<OptionSetOption>(resourceEndpoint, obj.SerializeObject());
        }

        public async Task<IClientResponse<OptionSetOption>> CreateAsync(string json)
        {
            string resourceEndpoint = "optionsets/options";
            return await PostDataAsync<OptionSetOption>(resourceEndpoint, json);
        }
        public async Task<IClientResponse<OptionSetOption>> CreateAsync(object obj)
        {
            string resourceEndpoint = "optionsets/options";
            return await PostDataAsync<OptionSetOption>(resourceEndpoint, obj.SerializeObject());
        }

        public async Task<IClientResponse<bool>> DeleteAsync(int id)
        {
            string resourceEndpoint = string.Format("optionsets/options/{0}", id);
            return await DeleteDataAsync(resourceEndpoint);
        }

        public async Task<IClientResponse<HttpOptions>> GetHttpOptionsAsync()
        {
            string resourceEndpoint = string.Format("optionsets/options");
            return await GetHttpOptionsDataAsync<HttpOptions>(resourceEndpoint);
        }
        public async Task<IClientResponse<HttpOptions>> GetHttpOptionsAsync(int id)
        {
            string resourceEndpoint = string.Format("optionsets/options/{0}", id);
            return await GetHttpOptionsDataAsync<HttpOptions>(resourceEndpoint);
        }


        public async Task<IList<OptionSetOption>> GetListAsync()
        {
            var filter = new Filter();
            return await GetListAsync(filter);
        }
        public async Task<IList<OptionSetOption>> GetListAsync(IFilter filter)
        {
            var items = await RecordPagingAsync(filter, this);
            return items;
        }
    }
}
