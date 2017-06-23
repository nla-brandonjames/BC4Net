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
using System.Linq;

namespace BigCommerce4Net.Api
{
    public class FilterCategories : Filter, IFilter
    {
        /// <summary>
        /// The minimum id of the Category.
        /// </summary>
        public int? MinimumId { get; set; }

        /// <summary>
        /// The maximum id of the Category.
        /// </summary>
        public int? MaximumId { get; set; }

        /// <summary>
        /// Filter by category name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Filter by category parent ID.
        /// </summary>
        public int? ParentId { get; set; }

        /// <summary>
        /// Filter by category visibility.
        /// </summary>
        public bool? IsVisible { get; set; }
        
        public override string AddFilter(string request)
        {
            request = base.AddFilter(request);
            var filters = new Dictionary<string, string>();

            if (MinimumId != null)
            {
                filters.Add("min_id", MinimumId.Value.ToString());
            }
            if (MaximumId != null)
            {
                filters.Add("max_id", MaximumId.Value.ToString());
            }
            if (Name != null)
            {
                filters.Add("name", Name);
            }
            if (ParentId != null)
            {
                filters.Add("parent_id", ParentId.Value.ToString());
            }
            if (IsVisible != null)
            {
                filters.Add("is_visible", IsVisible.Value.ToString());
            }

            var filterString = EncodeFilterString(filters);

            if (!request.Contains("?") && filters.Keys.Count > 0)
            {
                request += "?" + filterString;
            }

            return request;
        }
    }
}
