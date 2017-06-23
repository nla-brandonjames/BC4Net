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
    public class FilterOrders : Filter, IFilter
    {
        /// <summary>
        /// The minimum id of the order.
        /// </summary>
        public int? MinimumId { get; set; }

        /// <summary>
        /// The maximum id of the order.
        /// </summary>
        public int? MaximumId { get; set; }

        /// <summary>
        /// The minimum total for the order.
        /// </summary>
        public decimal? MinimumTotal { get; set; }

        /// <summary>
        /// The maximum total for the order.
        /// </summary>
        public decimal? MaximumTotal { get; set; }

        /// <summary>
        /// Filter orders by customers.
        /// </summary>
        public int? CustomerId { get; set; }

        /// <summary>
        /// Filter orders by the order status.
        /// </summary>
        public int? StatusId { get; set; }

        /// <summary>
        /// Filter orders by the deleted flag.
        /// </summary>
        public bool? IsDeleted { get; set; }

        /// <summary>
        /// Filter orders by payment method.
        /// </summary>
        public string PaymentMethod { get; set; }

        /// <summary>
        /// Retrieve all orders created after a specified date. 
        /// The date should be URL encoded, for example "Mon, 12 Sep 2011 06:40:17 +0000" 
        /// would be "Mon%2C%2012%20Sep%202011%2006%3A40%3A17%20%2B0000"
        /// </summary>
        public DateTime? MinimumDateCreated { get; set; }

        /// <summary>
        /// Retrieve all orders created before a specified date. 
        /// The date should be URL encoded, for example "Mon, 12 Sep 2011 06:40:17 +0000" 
        /// would be "Mon%2C%2012%20Sep%202011%2006%3A40%3A17%20%2B0000"
        /// </summary>
        public DateTime? MaximumDateCreated { get; set; }

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
            if (MinimumTotal != null)
            {
                filters.Add("min_total", MinimumTotal.Value.ToString());
            }
            if (MaximumTotal != null)
            {
                filters.Add("max_total", MaximumTotal.Value.ToString());
            }
            if (CustomerId != null)
            {
                filters.Add("customer_id", CustomerId.Value.ToString());
            }
            if (StatusId != null)
            {
                filters.Add("status_id", StatusId.Value.ToString());
            }
            if (IsDeleted != null)
            {
                filters.Add("is_deleted", IsDeleted.Value.ToString());
            }
            if (PaymentMethod != null)
            {
                filters.Add("payment_method", PaymentMethod);
            }
            if (MinimumDateCreated != null)
            {
                filters.Add("min_date_created", String.Format(RFC2822_DATE_FORMAT, MinimumDateCreated));
            }
            if (MaximumDateCreated != null)
            {
                filters.Add("max_date_created", String.Format(RFC2822_DATE_FORMAT, MaximumDateCreated));
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
