using Qareeb.Shared.Entities;
using Qareeb.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace QareebApp.Services
{

    public interface IAdminUserService
    {
        Task<PagedList<AdminUserReponse>> GetAll(PagingRequest model);
    }

    public class AdminUserService : IAdminUserService
    {
        private readonly IHttpService _httpService;

        public AdminUserService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<PagedList<AdminUserReponse>> GetAll(PagingRequest model)
        {
            var response =  await _httpService.GetListPaging<AdminUserReponse>($"/api/AdminUsers?PageNumber={model.PageNumber}&PageSize={model.PageSize}");
            return response;
        }
    }
}
