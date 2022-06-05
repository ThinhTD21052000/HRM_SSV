using Domain.Modals.Company;
using System.Net.Http.Json;

namespace Client.Services
{
    public interface ICompanyService
    {
        Task<CompanyToGet> GetList();
        Task Add(CompanyToAdd CompanyToAdd);
        Task Update(CompanyToUpdate CompanyToUpdate);
        Task Delete(int id);
    }
    public class CompanyService : ICompanyService
    {
        private readonly HttpClient _httpClient;
        public CompanyService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task Add(CompanyToAdd companyToAdd)
        {
            await _httpClient.PostAsJsonAsync("/api/Company/Insert", companyToAdd);
        }

        public async Task Delete(int id)
        {
            await _httpClient.DeleteAsync($"/api/Company/Delete?id={id}");
        }

        public async Task<CompanyToGet> GetList()
        {
            return await _httpClient.GetFromJsonAsync<CompanyToGet>("/api/Company/GetList");
        }

        public async Task Update(CompanyToUpdate companyToUpdate)
        {
            await _httpClient.PutAsJsonAsync("/api/Company/Update", companyToUpdate);
        }
    }
}
