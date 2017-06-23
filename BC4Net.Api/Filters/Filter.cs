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

namespace BigCommerce4Net.Api
{
    public class Filter : IFilter
    {
        protected const string RFC2822_DATE_FORMAT = "{0:ddd, dd MMM yyyy HH:mm:ss} GMT";

        /// <summary>
        /// How many results you'd like returned by providing a 
        /// limit parameter, which has a maximum of 250
        /// </summary>
        public int? Limit { get; set; }

        /// <summary>
        /// To retrieve a particular page of results, you can 
        /// provide a page parameter, where 1 is the first page
        /// </summary>
        public int? Page { get; set; }

        /// <summary>
        /// GET Operations Only
        /// 
        /// If supplied, then only resources modified since the specified date 
        /// will be returned. If there are no modified objects, then a 304 Not 
        /// Modified response will be sent. Please refer to the individual resource 
        /// pages for support for this header.
        /// </summary>
        public DateTime? MinDateModified { get; set; }

        public virtual string AddFilter(string request)
        {
            var filters = new Dictionary<string, string>();

            if (MinDateModified != null)
            {
                filters.Add("min_date_modified", String.Format(RFC2822_DATE_FORMAT, MinDateModified));
            }

            if (Limit != null)
            {
                filters.Add("limit", Limit.Value.ToString());
            }
            if (Page != null)
            {
                filters.Add("page", Page.Value.ToString());
            }

            var url = string.Format(request + "?{0}",
                    string.Join("&",
                    filters.Select(kvp =>
                    string.Format("{0}={1}", kvp.Key, System.Net.WebUtility.UrlEncode(kvp.Value)))));

            return url;
        }
    }

}
