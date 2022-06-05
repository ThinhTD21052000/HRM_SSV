using Domain.Modals.Response;
using Domain.Modals.User;
using System.Net.Http.Json;

namespace Client.Services
{
    public interface IUserService
    {
        Task<List<UserToGet>> GetList();
        Task Add(UserToAdd userToAdd);
        Task Update(UserToUpdate userToUpdate);
        Task Delete(Guid id);
        Task ChangePassword(ChangePasswordReponse reponse);
        Task<bool> CheckPassword(Guid Id, string password);
        Task<UserToGet> Get(Guid id);
    }
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;
        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task Add(UserToAdd userToAdd)
        {
            await _httpClient.PostAsJsonAsync<UserToAdd>("/api/User/Insert", userToAdd);
        }

        public async Task Delete(Guid id)
        {
            await _httpClient.DeleteAsync($"/api/User/Delete?id={id}");
        }
        public async Task<UserToGet> Get(Guid id)
        {
            return await _httpClient.GetFromJsonAsync<UserToGet>($"/api/User/Get?id={id}");
        }

        public async Task<List<UserToGet>> GetList()
        {
            return await _httpClient.GetFromJsonAsync<List<UserToGet>>("/api/User/GetList");
        }

        public async Task Update(UserToUpdate userToUpdate)
        {
            await _httpClient.PutAsJsonAsync<UserToUpdate>("/api/User/Update", userToUpdate);
        }
        public async Task ChangePassword(ChangePasswordReponse reponse)
        {
            await _httpClient.PostAsJsonAsync("/api/User/ChangePassword", reponse);
        }

        public async Task<bool> CheckPassword(Guid Id, string password)
        {
            return await _httpClient.GetFromJsonAsync<bool>($"/api/User/CheckPassword?id={Id}&password={password}");
        }
    }
}
