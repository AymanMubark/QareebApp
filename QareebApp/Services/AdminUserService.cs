using Qareeb.Shared.Entities;
using Qareeb.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QareebApp.Services
{

    public interface IAdminUserService
    {
        Task<IEnumerable<AdminUserReponse>> GetAll();
    }

    public class AdminUserService : IAdminUserService
    {
        private readonly IHttpService _httpService;

        public AdminUserService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<IEnumerable<AdminUserReponse>> GetAll()
        {
            return await _httpService.Get<IEnumerable<AdminUserReponse>>("/api/AdminUsers");
        }
    }
}
