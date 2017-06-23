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

namespace BigCommerce4Net.Api
{
    public interface IChildResourceGet<T>
    {
        Task<IClientResponse<List<T>>> GetAsync(int id);
        Task<IClientResponse<T>> GetAsync(int id, int childId);
        Task<IClientResponse<List<T>>> GetAsync(string resourceEndPoint);
        Task<IClientResponse<List<T>>> GetAsync(string resourceEndPoint, IFilter filter);

        Task<IClientResponse<HttpOptions>> GetHttpOptionsAsync(int id);
        Task<IClientResponse<HttpOptions>> GetHttpOptionsAsync(int id, int childId);
    }
}
