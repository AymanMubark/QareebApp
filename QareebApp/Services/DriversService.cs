using Qareeb.Shared.Models;
using QareebApp.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QareebApp.Services
{
    public interface IDriverService
    {
        public Task<PagedList<DriverResponse>> GetDrivers(PagingRequest model = null);
        public Task<DriverResponse> Create(CreateDriverForm model);
    }
    public class DriverService : IDriverService
    {
        private readonly IHttpService _httpService;

        public DriverService(IHttpService httpService)
        {
            _httpService = httpService;
        }
        public async Task<PagedList<DriverResponse>> GetDrivers(PagingRequest model = null)
        {
            return await _httpService.GetListPaging<DriverResponse>($"/api/Drivers/all?PageNumber={model.PageNumber}&PageSize={model.PageSize}");
        }
        public async Task<DriverResponse> Create(CreateDriverForm model)
        {
            var response = await _httpService.PostFormData<DriverResponse>($"/api/Drivers", model);
            return response;
        }
    }
}
