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
    public class ClientOptionSet : 
        ClientBase,
        IParentResourcePaging<OptionSet>,
        IParentResourceGetUpdateDeleteCreate<OptionSet>,
        IParentResourceCount
    {
        public ClientOptionSet(Configuration configuration)
            : base(configuration) { }

        public async Task<IClientResponse<ItemCount>> CountAsync()
        {
            string resourceEndpoint = string.Format("optionsets/count");
            return await CountAsync<ItemCount>(resourceEndpoint);
        }
        public async Task<IClientResponse<ItemCount>> CountAsync(IFilter filter)
        {
            string resourceEndpoint = string.Format("optionsets/count");
            return await CountAsync<ItemCount>(resourceEndpoint, filter);
        }

        public async Task<IClientResponse<OptionSet>> GetAsync(int id)
        {
            string resourceEndpoint = string.Format("optionsets/{0}", id);
            return await GetDataAsync<OptionSet>(resourceEndpoint);
        }
        public async Task<IClientResponse<OptionSet>> GetAsync(int id, IFilter filter)
        {
            string resourceEndpoint = string.Format("optionsets/{0}", id);
            return await GetDataAsync<OptionSet>(resourceEndpoint, filter);
        }
        public async Task<IClientResponse<List<OptionSet>>> GetAsync()
        {
            string resourceEndpoint = string.Format("optionsets");
            return await GetDataAsync<List<OptionSet>>(resourceEndpoint);
        }
        public async Task<IClientResponse<List<OptionSet>>> GetAsync(IFilter filter)
        {
            string resourceEndpoint = string.Format("optionsets");
            return await GetDataAsync<List<OptionSet>>(resourceEndpoint, filter);
        }
        public async Task<IClientResponse<List<OptionSet>>> GetAsync(string resourceEndPoint)
        {
            return await GetDataAsync<List<OptionSet>>(resourceEndPoint);
        }
        public async Task<IClientResponse<List<OptionSet>>> GetAsync(string resourceEndPoint, IFilter filter)
        {
            return await GetDataAsync<List<OptionSet>>(resourceEndPoint, filter);
        }

        public async Task<IClientResponse<OptionSet>> UpdateAsync(int id, string json)
        {
            string resourceEndpoint = string.Format("optionsets/{0}", id);
            return await PutDataAsync<OptionSet>(resourceEndpoint, json);
        }
        public async Task<IClientResponse<OptionSet>> UpdateAsync(int id, object obj)
        {
            string resourceEndpoint = string.Format("optionsets/{0}", id);
            return await PutDataAsync<OptionSet>(resourceEndpoint, obj.SerializeObject());
        }

        public async Task<IClientResponse<OptionSet>> CreateAsync(string json)
        {
            string resourceEndpoint = "optionsets";
            return await PostDataAsync<OptionSet>(resourceEndpoint, json);
        }
        public async Task<IClientResponse<OptionSet>> CreateAsync(object obj)
        {
            string resourceEndpoint = "optionsets";
            return await PostDataAsync<OptionSet>(resourceEndpoint, obj.SerializeObject());
        }

        public async Task<IClientResponse<bool>> DeleteAsync(int id)
        {
            string resourceEndpoint = string.Format("optionsets/{0}", id);
            return await DeleteDataAsync(resourceEndpoint);
        }

        public async Task<IClientResponse<HttpOptions>> GetHttpOptionsAsync()
        {
            string resourceEndpoint = string.Format("optionsets");
            return await GetHttpOptionsDataAsync<HttpOptions>(resourceEndpoint);
        }
        public async Task<IClientResponse<HttpOptions>> GetHttpOptionsAsync(int id)
        {
            string resourceEndpoint = string.Format("optionsets/{0}", id);
            return await GetHttpOptionsDataAsync<HttpOptions>(resourceEndpoint);
        }

        public async Task<IList<OptionSet>> GetListAsync()
        {
            var filter = new Filter();
            return await GetListAsync(filter);
        }
        public async Task<IList<OptionSet>> GetListAsync(IFilter filter)
        {
            var items = await RecordPagingAsync(filter, this);
            return items;
        }
    }
}
