using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using Qareeb.Shared.Entities;
using Qareeb.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static QareebApp.Services.LocalStorageService;
using QareebApp.Shared;
namespace QareebApp.Services
{
    public interface IHttpService
    {
        Task<T> Get<T>(string uri);
        public Task<PagedList<T>> GetListPaging<T>(string uri);
        Task<T> Post<T>(string uri, object value);
    }

    public class HttpService : IHttpService
    {
        private HttpClient _httpClient;
        private NavigationManager _navigationManager;
        private ILocalStorageService _localStorageService;
        private IConfiguration _configuration;

        public HttpService(
            HttpClient httpClient,
            NavigationManager navigationManager,
            ILocalStorageService localStorageService,
            IConfiguration configuration
        )
        {
            _httpClient = httpClient;
            _navigationManager = navigationManager;
            _localStorageService = localStorageService;
            _configuration = configuration;
        }


        public Task<T> Get<T>(string uri)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            return null;
        }


        public async Task<PagedList<T>> GetListPaging<T>(string uri)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            HttpResponseMessage response = await sendRequest(request);
            if (response != null)
            {
                var content = await response.Content.ReadFromJsonAsync<IEnumerable<T>>();
                MetaData MetaData = JsonSerializer.Deserialize<MetaData>(response.Headers.GetValues("X-Pagination").First());
                var list = new PagedList<T>(content.ToList(), MetaData);
                return list;
            }
            return default;
        }

        public async Task<T> Post<T>(string uri, object value)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, uri);
            request.Content = new StringContent(JsonSerializer.Serialize(value), Encoding.UTF8, "application/json");
            return await sendRequestWithData<T>(request);
        }

        // helper methods


        private async Task<T> sendRequestWithData<T>(HttpRequestMessage request)
        {
            HttpResponseMessage response = await sendRequest(request);
            var content = await response.Content.ReadFromJsonAsync<T>();
            return content;
        }
        private async Task<HttpResponseMessage> sendRequest(HttpRequestMessage request)
        {
            // add jwt auth header if user is logged in and request is to the api url
            var user = await _localStorageService.GetItem<AdminUserLoginResponse>("user");
            var isApiUrl = !request.RequestUri.IsAbsoluteUri;
            if (user != null && isApiUrl)
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", user.Token);

            var response = await _httpClient.SendAsync(request);

            // auto logout on 401 response
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                _navigationManager.NavigateTo("logout");
                return default;
            }

            // throw exception on error response
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadFromJsonAsync<Dictionary<string, string>>();
                throw new Exception(error["message"]);
            }
            return response;
        }


    }
}
