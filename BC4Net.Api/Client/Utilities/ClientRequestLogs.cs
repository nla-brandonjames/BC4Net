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
using System.Threading.Tasks;

namespace BigCommerce4Net.Api.ResourceClients
{
    public class ClientRequestLogs : ClientBase
    {
        public ClientRequestLogs(Configuration configuration)
            :base(configuration)
        {}

        public async Task<IClientResponse<List<RequestLog>>> GetAsync()
        {
            string resourceEndpoint = "request_logs";
            return await GetDataAsync<List<RequestLog>>(resourceEndpoint);
        }
        public async Task<IClientResponse<RequestLog>> GetAsync(int requestLogId)
        {
            string resourceEndpoint = string.Format("request_logs/{0}", requestLogId);
            return await GetDataAsync<RequestLog>(resourceEndpoint);
        }
        public async Task<IClientResponse<List<RequestLog>>> GetAsync(string resourceEndPoint)
        {
            return await GetDataAsync<List<RequestLog>>(resourceEndPoint);
        }

        public async Task<IClientResponse<HttpOptions>> GetHttpOptionsAsync()
        {
            string resourceEndpoint = string.Format("request_logs");
            return await GetHttpOptionsDataAsync<HttpOptions>(resourceEndpoint);
        }
        public async Task<IClientResponse<HttpOptions>> GetHttpOptionsAsync(int requestLogId)
        {
            string resourceEndpoint = string.Format("request_logs/{0}", requestLogId);
            return await GetHttpOptionsDataAsync<HttpOptions>(resourceEndpoint);
        }
    }
}
