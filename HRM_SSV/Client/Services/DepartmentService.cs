using Domain.Modals.Department;
using System.Net.Http.Json;

namespace Client.Services
{
    public interface IDepartmentService
    {
        Task<List<DepartmentToGet>> GetList();
        Task Add(DepartmentToAdd departmentToAdd);
        Task Update(DepartmentToUpdate departmentToUpdate);
        Task Delete(int id);
    }
    public class DepartmentService : IDepartmentService
    {
        private readonly HttpClient _httpClient;
        public DepartmentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task Add(DepartmentToAdd departmentToAdd)
        {
            await _httpClient.PostAsJsonAsync("/api/Department/Insert", departmentToAdd);
        }

        public async Task Delete(int id)
        {
            await _httpClient.DeleteAsync($"/api/Department/Delete?id={id}");
        }

        public async Task<List<DepartmentToGet>> GetList()
        {
            return await _httpClient.GetFromJsonAsync<List<DepartmentToGet>>("/api/Department/GetList");
        }

        public async Task Update(DepartmentToUpdate departmentToUpdate)
        {
            await _httpClient.PutAsJsonAsync("/api/Department/Update", departmentToUpdate);
        }
    }
}
