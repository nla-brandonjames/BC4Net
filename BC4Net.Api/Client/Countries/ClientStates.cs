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
    public class ClientStates : 
        ClientBase, 
        IParentResourcePaging<State>, 
        IParentResourceGet<State>, 
        IParentResourceCount
    {
        public ClientStates(Configuration configuration, BCAuthentication authentication) : base(configuration, authentication) { }

        public async Task<IClientResponse<ItemCount>> CountAsync()
        {
            string resourceEndpoint = "countries/states/count";
            return await CountAsync<ItemCount>(resourceEndpoint);
        }
        public async Task<IClientResponse<ItemCount>> CountAsync(IFilter filter)
        {
            string resourceEndpoint = "countries/states/count";
            return await CountAsync<ItemCount>(resourceEndpoint, filter);
        }

        public async Task<IClientResponse<List<State>>> GetAsync()
        {
            var filter = new FilterStates();
            return await GetAsync(filter);
        }
        public async Task<IClientResponse<List<State>>> GetAsync(IFilter filter)
        {
            string resourceEndpoint = "countries/states";
            return await GetDataAsync<List<State>>(resourceEndpoint, filter);
        }
        public async Task<IClientResponse<State>> GetAsync(int id, IFilter filter)
        {
            string resourceEndpoint = string.Format("countries/states/{0}", id);
            return await GetDataAsync<State>(resourceEndpoint, filter);
        }
        public async Task<IClientResponse<State>> GetAsync(int id)
        {
            string resourceEndpoint = string.Format("countries/states/{0}", id);
            return await GetDataAsync<State>(resourceEndpoint);
        }
        public async Task<IClientResponse<List<State>>> GetAsync(string resourceEndPoint)
        {
            return await GetDataAsync<List<State>>(resourceEndPoint);
        }
        public async Task<IClientResponse<List<State>>> GetAsync(string resourceEndPoint, IFilter filter)
        {
            return await GetDataAsync<List<State>>(resourceEndPoint, filter);
        }

        public async Task<IClientResponse<List<State>>> GetStatesInCountryAsync(int id)
        {
            string resourceEndpoint = string.Format("countries/{0}/states", id);
            return await GetDataAsync<List<State>>(resourceEndpoint);
        }

        public async Task<IClientResponse<HttpOptions>> GetHttpOptionsAsync(int id)
        {
            string resourceEndpoint = string.Format("countries/states/{0}", id);
            return await GetHttpOptionsDataAsync<HttpOptions>(resourceEndpoint);
        }
        public async Task<IClientResponse<HttpOptions>> GetHttpOptionsAsync()
        {
            string resourceEndpoint = string.Format("countries/states");
            return await GetHttpOptionsDataAsync<HttpOptions>(resourceEndpoint);
        }

        public async Task<IList<State>> GetListAsync()
        {
            var filter = new FilterStates();
            return await GetListAsync(filter);
        }
        public async Task<IList<State>> GetListAsync(IFilter filter)
        {
            var items = await RecordPagingAsync(filter, this);
            return items;
        }
    }
}
