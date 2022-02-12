using Blazorcrud.Client.Shared;
using Blazorcrud.Shared.Data;
using Blazorcrud.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace Blazorcrud.Client.Services
{
    public class UserService: IUserService
    {
        private IHttpService _httpService;
        private ILocalStorageService _localStorageService;
        private NavigationManager _navigationManager;
        private string _userKey = "user";

        public User User {get; private set;}

        public UserService(IHttpService httpService, ILocalStorageService localStorageService, NavigationManager navigationManager)
        {
            _httpService = httpService;
            _localStorageService = localStorageService;
            _navigationManager = navigationManager;
        }

        public async Task Initialize()
        {
            User = await _localStorageService.GetItem<User>(_userKey);
        }

        public async Task Login(Login model)
        {
            User = await _httpService.Post<User>("/api/user/authenticate", model);
            await _localStorageService.SetItem(_userKey, User);
        }

        public async Task Logout()
        {
            User = null;
            await _localStorageService.RemoveItem(_userKey);
            _navigationManager.NavigateTo("/user/login");
        }

        public async Task<PagedResult<User>> GetUsers(string? name, string page)
        {
            return await _httpService.Get<PagedResult<User>>("api/user" + "?page=" + page + "&name=" + name);
        }

        public async Task<User> GetUser(int id)
        {
            return await _httpService.Get<User>($"api/user/{id}");
        }

        public async Task DeleteUser(int id)
        {
            await _httpService.Delete($"api/user/{id}");
            // auto logout if the user deleted their own record
            if (id == User.Id)
                await Logout();
        }

        public async Task AddUser(User user)
        {
            await _httpService.Post($"api/user", user);
        }

        public async Task UpdateUser(User user)
        {
            await _httpService.Put($"api/user", user);
            // update local storage if the user updated their own record
            if (user.Id == User.Id)
            {
                User.FirstName = user.FirstName;
                User.LastName = user.LastName;
                User.Username = user.Username;
                await _localStorageService.SetItem(_userKey, User);
            }
        }
    }
}