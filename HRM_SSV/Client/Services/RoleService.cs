using Domain.Modals.Role;
using System.Net.Http.Json;

namespace Client.Services
{
    public interface IRoleService
    {
        Task<List<RoleToGet>> GetList();
        Task Add(RoleToAdd RoleToAdd);
        Task Update(RoleToUpdate RoleToUpdate);
        Task Delete(Guid id);
    }
    public class RoleService : IRoleService
    {
        private readonly HttpClient _httpClient;
        public RoleService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task Add(RoleToAdd RoleToAdd)
        {
            await _httpClient.PostAsJsonAsync("/api/Role/Insert", RoleToAdd);
        }

        public async Task Delete(Guid id)
        {
            await _httpClient.DeleteAsync($"/api/Role/Delete?id={id}");
        }

        public async Task<List<RoleToGet>> GetList()
        {
            return await _httpClient.GetFromJsonAsync<List<RoleToGet>>("/api/Role/GetList");
        }

        public async Task Update(RoleToUpdate RoleToUpdate)
        {
            await _httpClient.PutAsJsonAsync("/api/Role/Update", RoleToUpdate);
        }
    }
}
