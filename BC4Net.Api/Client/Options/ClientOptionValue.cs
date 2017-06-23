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
using System.Threading.Tasks;

namespace BigCommerce4Net.Api.ResourceClients
{
    //TODO: Look at the interface on ClientOptionValue - the online docs suck!
    public class ClientOptionValue : 
        ClientBase, 
        IParentResourcePaging<OptionValue>
    {
        public ClientOptionValue(Configuration configuration, BCAuthentication authentication) : base(configuration, authentication) { }

        public async Task<IClientResponse<ItemCount>> CountAsync()
        {
            string resourceEndpoint = string.Format("options/values/count");
            return await CountAsync<ItemCount>(resourceEndpoint);
        }
        public async Task<IClientResponse<ItemCount>> CountAsync(IFilter filter)
        {
            string resourceEndpoint = string.Format("options/values/count");
            return await CountAsync<ItemCount>(resourceEndpoint, filter);
        }
        public async Task<IClientResponse<OptionValue>> GetAsync(int id, int valueid)
        {
            string resourceEndpoint = string.Format("options/{0}/values/{1}", id, valueid);
            return await GetDataAsync<OptionValue>(resourceEndpoint);
        }
        public async Task<IClientResponse<OptionValue>> GetAsync(int id, int valueid, IFilter filter)
        {
            string resourceEndpoint = string.Format("options/{0}/values/{1}", id, valueid);
            return await GetDataAsync<OptionValue>(resourceEndpoint, filter);
        }
        public async Task<IClientResponse<List<OptionValue>>> GetAsync()
        {
            string resourceEndpoint = string.Format("options/values");
            return await GetDataAsync<List<OptionValue>>(resourceEndpoint);
        }
        public async Task<IClientResponse<List<OptionValue>>> GetAsync(IFilter filter)
        {
            string resourceEndpoint = string.Format("options/values");
            return await GetDataAsync<List<OptionValue>>(resourceEndpoint, filter);
        }
        public async Task<IClientResponse<List<OptionValue>>> GetAsync(string resourceEndPoint)
        {
            return await GetDataAsync<List<OptionValue>>(resourceEndPoint);
        }
        public async Task<IClientResponse<List<OptionValue>>> GetAsync(string resourceEndPoint, IFilter filter)
        {
            return await GetDataAsync<List<OptionValue>>(resourceEndPoint, filter);
        }

        public async Task<IClientResponse<HttpOptions>> GetHttpOptionsAsync()
        {
            string resourceEndpoint = string.Format("options/values");
            return await GetHttpOptionsDataAsync<HttpOptions>(resourceEndpoint);
        }
        public async Task<IClientResponse<HttpOptions>> GetHttpOptionsAsync(int id, int valueid)
        {
            string resourceEndpoint = string.Format("options/{0}/values/{1}", id, valueid);
            return await GetHttpOptionsDataAsync<HttpOptions>(resourceEndpoint);
        }

        public async Task<IList<OptionValue>> GetListAsync()
        {
            var filter = new Filter();
            return await GetListAsync(filter);
        }
        public async Task<IList<OptionValue>> GetListAsync(IFilter filter)
        {
            var items = await RecordPagingAsync(filter, this);
            return items;
        }
    }
}
