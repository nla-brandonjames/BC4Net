using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

using Api = BigCommerce4Net.Api;

namespace BC4Net.Api_Tests.Orders
{
    public class ClientOrders_TF : BaseTest
    {
        [Fact]
        public async Task Can_Get_CountAsync()
        {
            var response = await Client.Orders.CountAsync();

            Assert.Equal(response.RestResponse.StatusCode, System.Net.HttpStatusCode.OK);
            Assert.True(response.Data.Count > 0);
        }

        [Fact]
        public async Task Can_Get_FilteredAsync()
        {
            var filter = new Api.FilterOrders()
            {
                MaximumId = 4000
            };

            var response = await Client.Orders.GetAsync(filter);

            Assert.Equal(response.RestResponse.StatusCode, System.Net.HttpStatusCode.OK);
            Assert.True(response.Data.Count > 0);
        }

        [Fact]
        public async Task Can_Get_Filtered_RecordPagingAsync()
        {
            var filter = new Api.FilterOrders()
            {
                MinimumId = 3000
            };
            var response_count = await Client.Orders.CountAsync(filter);
            var response_list = await Client.Orders.GetListAsync(filter);

            Assert.Equal(response_list.Count, response_count.Data.Count);
        }

        [Fact]
        public async Task Can_Get_Non_Filtered_RecordPagingAsync()
        {
            var response_count = await Client.Orders.CountAsync();
            var response_list = await Client.Orders.GetListAsync();

            Assert.Equal(response_list.Count, response_count.Data.Count);
        }

        [Fact]
        public async Task Can_Get_ShippingAddresses_And_AddAsync()
        {
            var response_order = await Client.Orders.GetAsync(TEST_ORDER_ID);

            var response_shipping_addresses = await Client.OrdersShippingAddresses.GetAsync(response_order.Data.Id);

            Assert.True(response_shipping_addresses.Data.Count > 0);
            Assert.Equal(response_order.Data.Id, response_shipping_addresses.Data[0].OrderId);
        }

        [Fact]
        public async Task Can_Get_OrdersProducts_And_AddAsync()
        {
            var response_order = await Client.Orders.GetAsync(TEST_ORDER_ID);

            await Client.OrdersProducts.GetAsync(response_order.Data);

            Assert.True(response_order.Data.Products.Count > 0);
            Assert.Equal(response_order.Data.Id, response_order.Data.Products[0].OrderId);
        }

        [Fact]
        public async Task Can_Get_GetHttpOptionsAsync()
        {
            var response = await Client.Orders.GetHttpOptionsAsync();
            Assert.Equal(response.RestResponse.StatusCode, System.Net.HttpStatusCode.OK);
            Assert.NotEqual(response.Data, null);
        }
    }
}
