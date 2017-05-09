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
using System.Threading.Tasks;

namespace BigCommerce4Net.Api.ResourceClients
{
    public class ClientCountries : 
        ClientBase,
        IParentResourcePaging<Country>,
        IParentResourceGet<Country>,
        IParentResourceCount
    {
        public ClientCountries(Configuration configuration) : base(configuration) { }

        public async Task<IClientResponse<ItemCount>> CountAsync()
        {
            string resourceEndpoint = "countries/count";
            return await CountAsync<ItemCount>(resourceEndpoint);
        }
        public async Task<IClientResponse<ItemCount>> CountAsync(IFilter filter)
        {
            string resourceEndpoint = "countries/count";
            return await CountAsync<ItemCount>(resourceEndpoint, filter);
        }

        public async Task<IClientResponse<List<Country>>> GetAsync()
        {
            var filter = new FilterCountries();
            return await GetAsync(filter);
        }
        public async Task<IClientResponse<List<Country>>> GetAsync(IFilter filter)
        {
            string resourceEndpoint = "countries";
            return await GetDataAsync<List<Country>>(resourceEndpoint, filter);
        }
        public async Task<IClientResponse<Country>> GetAsync(int id, IFilter filter)
        {
            string resourceEndpoint = string.Format("countries/{0}", id);
            return await GetDataAsync<Country>(resourceEndpoint, filter);
        }
        public async Task<IClientResponse<Country>> GetAsync(int id)
        {
            string resourceEndpoint = string.Format("countries/{0}", id);
            return await GetDataAsync<Country>(resourceEndpoint);
        }
        public async Task<IClientResponse<List<Country>>> GetAsync(string resourceEndPoint)
        {
            return await GetDataAsync<List<Country>>(resourceEndPoint);
        }
        public async Task<IClientResponse<List<Country>>> GetAsync(string resourceEndPoint, IFilter filter)
        {
            return await GetDataAsync<List<Country>>(resourceEndPoint, filter);
        }

        public async Task<IClientResponse<HttpOptions>> GetHttpOptionsAsync(int id)
        {
            string resourceEndpoint = string.Format("countries/{0}", id);
            return await GetHttpOptionsDataAsync<HttpOptions>(resourceEndpoint);
        }
        public async Task<IClientResponse<HttpOptions>> GetHttpOptionsAsync()
        {
            string resourceEndpoint = string.Format("countries");
            return await GetHttpOptionsDataAsync<HttpOptions>(resourceEndpoint);
        }

        public async Task<IList<Country>> GetListAsync()
        {
            var filter = new FilterCountries();
            return await GetListAsync(filter);
        }
        public async Task<IList<Country>> GetListAsync(IFilter filter)
        {
            var items = await RecordPagingAsync<Country>(filter, this);
            return items;
        }
    }
}
