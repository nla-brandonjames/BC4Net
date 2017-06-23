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
    public class FilterProducts : Filter, IFilter
    {
        /// <summary>
        /// The minimum id of the product.
        /// </summary>
        public int? MinimumId { get; set; }

        /// <summary>
        /// The maximum id of the product.
        /// </summary>
        public int? MaximumId { get; set; }

        /// <summary>
        /// The product name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The product sku.
        /// </summary>
        public string Sku { get; set; }

        /// <summary>
        /// The product description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The product condition.
        /// </summary>
        public string Condition { get; set; }

        /// <summary>
        /// Filter on the availability of the product.
        /// </summary>
        public string Availability { get; set; }

        /// <summary>
        /// Filter by the products brand.
        /// </summary>
        public int? BrandId { get; set; }

        /// <summary>
        /// The minimum creation date of the product.
        /// </summary>
        public DateTime? MimimumDateCreated { get; set; }

        /// <summary>
        /// The maximum creation date of the product.
        /// </summary>
        public DateTime? MaximumDateCreated { get; set; }

        /// <summary>
        /// The minimum last modified date of the product. 
        /// </summary>
        public DateTime? MimimumDateModified { get; set; }

        /// <summary>
        /// The maximum last modified date of the product. 
        /// </summary>
        public DateTime? MaximumDateModified { get; set; }

        /// <summary>
        /// The minimum import date of the product. 
        /// </summary>
        public DateTime? MinimumDateLastImported { get; set; }

        /// <summary>
        /// The maximum import date of the product. 
        /// </summary>
        public DateTime? MaximumDateLastImported { get; set; }

        /// <summary>
        /// Filter by product visibility status.
        /// </summary>
        public bool? IsVisible { get; set; }

        /// <summary>
        /// Filter by product featured status.
        /// </summary>
        public bool? IsFeatured { get; set; }

        /// <summary>
        /// Filter by inventory level.
        /// </summary>
        public int? MimimumInventoryLevel { get; set; }

        /// <summary>
        /// Filter by inventory level.
        /// </summary>
        public int? MaximumInventoryLevel { get; set; }

        /// <summary>
        /// Filter on product price.
        /// </summary>
        public decimal? MimimumPrice { get; set; }

        /// <summary>
        /// Filter on product price.
        /// </summary>
        public decimal? MaximumPrice { get; set; }

        /// <summary>
        /// Filter on the number of the product that has been sold.
        /// </summary>
        public int? MimimumNumberSold { get; set; }

        /// <summary>
        /// Filter on the number of the product that has been sold. 
        /// </summary>
        public int? MaximumNumberSold { get; set; }

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
            if (Sku != null)
            {
                filters.Add("sku", Sku);
            }
            if (Description != null)
            {
                filters.Add("description", Description);
            }
            if (Condition != null)
            {
                filters.Add("condition", Condition);
            }
            if (Availability != null)
            {
                filters.Add("availability", Availability);
            }
            if (BrandId != null)
            {
                filters.Add("brand_id", BrandId.Value.ToString());
            }
            if (MimimumDateCreated != null)
            {
                filters.Add("min_date_created",
                    String.Format(RFC2822_DATE_FORMAT, MimimumDateCreated));
            }
            if (MaximumDateCreated != null)
            {
                filters.Add("max_date_created",
                    String.Format(RFC2822_DATE_FORMAT, MaximumDateCreated));
            }
            if (MimimumDateModified != null)
            {
                filters.Add("min_date_modified",
                    String.Format(RFC2822_DATE_FORMAT, MimimumDateModified));
            }
            if (MaximumDateModified != null)
            {
                filters.Add("max_date_modified",
                    String.Format(RFC2822_DATE_FORMAT, MaximumDateModified));
            }
            if (MinimumDateLastImported != null)
            {
                filters.Add("min_date_last_imported",
                    String.Format(RFC2822_DATE_FORMAT, MinimumDateLastImported));
            }
            if (MaximumDateLastImported != null)
            {
                filters.Add("max_date_last_imported",
                    String.Format(RFC2822_DATE_FORMAT, MaximumDateLastImported));
            }
            if (IsVisible != null)
            {
                filters.Add("is_visible", IsVisible.Value.ToString());
            }
            if (IsFeatured != null)
            {
                filters.Add("is_featured", IsFeatured.Value.ToString());
            }
            if (MimimumInventoryLevel != null)
            {
                filters.Add("min_inventory_level", MimimumInventoryLevel.Value.ToString());
            }
            if (MaximumInventoryLevel != null)
            {
                filters.Add("max_inventory_level", MaximumInventoryLevel.Value.ToString());
            }
            if (MimimumPrice != null)
            {
                filters.Add("min_price", MimimumPrice.Value.ToString());
            }
            if (MaximumPrice != null)
            {
                filters.Add("max_price", MaximumPrice.Value.ToString());
            }
            if (MimimumNumberSold != null)
            {
                filters.Add("min_number_sold", MimimumNumberSold.Value.ToString());
            }
            if (MaximumNumberSold != null)
            {
                filters.Add("max_number_sold", MaximumNumberSold.Value.ToString());
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
