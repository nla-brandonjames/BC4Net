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

namespace BigCommerce4Net.Api
{
    public class FilterCountries : Filter, IFilter
    {
        /// <summary>
        /// Filter by country name.
        /// </summary>
        public string CountryName { get; set; }

        /// <summary>
        /// Filter by ISO-2 country code.
        /// </summary>
        public string CountryIso2 { get; set; }

        /// <summary>
        /// Filter by ISO-3 country code.
        /// </summary>
        public string CountryIso3 { get; set; }

        public override string AddFilter(string request)
        {
            request = base.AddFilter(request);
            var filters = new System.Collections.Generic.Dictionary<string, string>();

            if (CountryName != null)
            {
                filters.Add("country", CountryName);
            }
            if (CountryIso2 != null)
            {
                filters.Add("country_iso2", CountryIso2);
            }
            if (CountryIso3 != null)
            {
                filters.Add("country_iso3", CountryIso3);
            }

            var filterString = EncodeFilterString(filters);

            if (!request.Contains("?") && filters.Keys.Count > 0)
            {
                request += "?" + filterString;
            }

            return request + filterString;
        }
    }
}
