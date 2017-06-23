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
using System.Linq;
using System.Collections.Generic;

namespace BigCommerce4Net.Api
{
    public class FilterCustomers : Filter, IFilter
    {
        /// <summary>
        /// The minimum id of the customer.
        /// </summary>
        public int? MinimumId { get; set; }

        /// <summary>
        /// The maximum id of the customer.
        /// </summary>
        public int? MaximumId { get; set; }

        /// <summary>
        /// Filter by first name.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Filter by last name.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Filter by company.
        /// </summary>
        public string Company { get; set; }

        /// <summary>
        /// Filter by email address.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Filter by phone number.
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Filter by store credit.
        /// </summary>
        public decimal? StoreCredit { get; set; }

        /// <summary>
        /// Filter by customer group.
        /// </summary>
        public int? CustomerGroupId { get; set; }

        /// <summary>
        /// The minimum creation date of the customer.
        /// </summary>
        public DateTime? MinDateCreated { get; set; }

        /// <summary>
        /// The maximum creation date of the customer.
        /// </summary>
        public DateTime? MaxDateCreated { get; set; }

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
            if (FirstName != null)
            {
                filters.Add("first_name", FirstName);
            }
            if (LastName != null)
            {
                filters.Add("last_name", LastName);
            }
            if (Company != null)
            {
                filters.Add("company", Company);
            }
            if (Email != null)
            {
                filters.Add("email", Email);
            }
            if (Phone != null)
            {
                filters.Add("phone", Phone);
            }
            if (StoreCredit != null)
            {
                filters.Add("store_credit", StoreCredit.Value.ToString());
            }
            if (CustomerGroupId != null)
            {
                filters.Add("customer_group_id", CustomerGroupId.Value.ToString());
            }
            if (MinDateCreated != null)
            {
                filters.Add("min_date_created", String.Format(RFC2822_DATE_FORMAT, MinDateCreated));
            }
            if (MaxDateCreated != null)
            {
                filters.Add("max_date_created", String.Format(RFC2822_DATE_FORMAT, MaxDateCreated));
            }

            var filterString = EncodeFilterString(filters);

            if (!request.Contains("?") && filters.Keys.Count > 0)
            {
                request += "?";
            }

            return request + filterString;
        }
    }
}
