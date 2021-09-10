using Qareeb.Shared.Models;
using QareebApp.Features;
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
        Task<AdminUserReponse> Update(Guid id,AdminUserUpdateRequest model);
        Task<AdminUserReponse> Create(CreateAdminForm model);
        Task<AdminUserReponse> GetById(Guid id);
        Task Delete(Guid id);
    }

    public class AdminUserService : IAdminUserService
    {
        private readonly IHttpService _httpService;

        public AdminUserService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<AdminUserReponse> Create(CreateAdminForm model)
        {
            var response = await _httpService.PostFormData<AdminUserReponse>($"/api/AdminUsers",model);
            return response;
        }

        public async Task Delete(Guid id)
        {
            await _httpService.Delete($"/api/AdminUsers/{id}");
        }

        public async Task<PagedList<AdminUserReponse>> GetAll(PagingRequest model)
        {
            var response =  await _httpService.GetListPaging<AdminUserReponse>($"/api/AdminUsers?SearchKey={model.SearchKey}&PageNumber={model.PageNumber}&PageSize={model.PageSize}");
            return response;
        }

        public  async Task<AdminUserReponse> GetById(Guid id)
        {
            return await _httpService.Get<AdminUserReponse>($"/api/AdminUsers/{id}");
        }

        public async Task<AdminUserReponse> Update(Guid id,AdminUserUpdateRequest model)
        {
            var response = await _httpService.Put<AdminUserReponse>($"/api/AdminUsers/{id}", model);
            return response;
        }
    }
}
