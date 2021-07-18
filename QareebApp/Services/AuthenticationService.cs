using Microsoft.AspNetCore.Components;
using Qareeb.Shared.Entities;
using Qareeb.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static QareebApp.Services.LocalStorageService;

namespace QareebApp.Services
{

    public interface IAuthenticationService
    {
        AdminUserLoginResponse User { get; }
        Task Initialize();
        Task Login(string username, string password);
        Task Logout();
    }

    public class AuthenticationService : IAuthenticationService
    {
        private readonly IHttpService _httpService;
        private readonly NavigationManager _navigationManager;
        private readonly ILocalStorageService _localStorageService;

        public AdminUserLoginResponse User { get; private set; }

        public AuthenticationService(
            IHttpService httpService,
            NavigationManager navigationManager,
            ILocalStorageService localStorageService
        )
        {
            _httpService = httpService;
            _navigationManager = navigationManager;
            _localStorageService = localStorageService;
        }

        public async Task Initialize()
        {
            User = await _localStorageService.GetItem<AdminUserLoginResponse>("user");
        }

        public async Task Login(string username, string password)
        {
            User = await _httpService.Post<AdminUserLoginResponse>("/api/AdminUsers/login", new AdminUserLoginRequest() { UserName = username, Password = password, });
            await _localStorageService.SetItem("user", User);
        }

        public async Task Logout()
        {
            User = null;
            await _localStorageService.RemoveItem("user");
            _navigationManager.NavigateTo("login");
        }
    }
}
