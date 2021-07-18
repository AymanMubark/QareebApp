using Qareeb.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QareebApp.Services
{
    public interface IOrderService
    {
        public Task<PagedList<OrderResponse>> GetOrders(PagingRequest model = null);
    }
    public class OrderService : IOrderService
    {
        private readonly IHttpService _httpService;

        public OrderService(IHttpService httpService)
        {
            _httpService = httpService;
        }
        public async Task<PagedList<OrderResponse>> GetOrders(PagingRequest model = null)
        {
            return await _httpService.GetListPaging<OrderResponse>($"/api/Orders/all?PageNumber={model.PageNumber}&PageSize={model.PageSize}");
        }
    }
}
