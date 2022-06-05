using Domain.Modals.WageType;
using System.Net.Http.Json;

namespace HRMClient.Services
{
    public interface IWageTypeService
    {
        Task<List<Wage_TypeToGet>> GetList();
        Task Add(Wage_TypeToAdd WageTypeToAdd);
        Task Update(Wage_TypeToUpdate WageTypeToUpdate);
        Task Delete(int id);
    }
    public class WageTypeService : IWageTypeService
    {
        private readonly HttpClient _httpClient;
        public WageTypeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task Add(Wage_TypeToAdd WageTypeToAdd)
        {
            await _httpClient.PostAsJsonAsync("/api/Wage_Type/Insert", WageTypeToAdd);
        }

        public async Task Delete(int id)
        {
            await _httpClient.DeleteAsync($"/api/Wage_Type/Delete?id={id}");
        }

        public async Task<List<Wage_TypeToGet>> GetList()
        {
            return await _httpClient.GetFromJsonAsync<List<Wage_TypeToGet>>("/api/Wage_Type/GetList");
        }

        public async Task Update(Wage_TypeToUpdate WageTypeToUpdate)
        {
            await _httpClient.PutAsJsonAsync("/api/Wage_Type/Update", WageTypeToUpdate);
        }
    }
}
