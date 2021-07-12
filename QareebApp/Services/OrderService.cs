using Qareeb.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QareebApp.Services
{
    public interface IOrderService
    {
        public Task<IEnumerable<OrderResponse>> GetOrders();
    }
    public class OrderService : IOrderService
    {
        private readonly IHttpService _httpService;

        public OrderService(IHttpService httpService)
        {
            _httpService = httpService;
        }
        public async Task<IEnumerable<OrderResponse>> GetOrders()
        {
            return await _httpService.Get<IEnumerable<OrderResponse>>("/api/Orders/all");
        }
    }
}
