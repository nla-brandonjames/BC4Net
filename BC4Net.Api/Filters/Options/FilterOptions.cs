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

using System.Linq;
using System.Collections.Generic;

namespace BigCommerce4Net.Api
{
    public class FilterOptions : Filter, IFilter
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string OptionType { get; set; }

        public override string AddFilter(string request)
        {
            request += base.AddFilter(request);
            var filters = new Dictionary<string, string>();

            if (Name != null) {
                filters.Add("name", Name);
            }

            if (DisplayName != null) {
                filters.Add("display_name", DisplayName);
            }

            if (OptionType != null) {
                filters.Add("type", OptionType);
            }

            var filterString = string.Join("&",
                    filters.Select(kvp =>
                    string.Format("{0}={1}", kvp.Key, System.Net.WebUtility.UrlEncode(kvp.Value))));

            if (!request.Contains("?") && filters.Keys.Count > 0)
            {
                request += "?";
            }

            return request + filterString;
        }
    }
}