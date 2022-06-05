using Domain.Modals.LaborContract;
using System.Net.Http.Json;

namespace HRMClient.Services
{
    public interface ILaborContractService
    {
        Task<List<LaborContractToGet>> GetList(Guid Id);
        Task Add(LaborContractToAdd LaborContractToAdd);
        Task Update(LaborContractToUpdate LaborContractToUpdate);
        Task Delete(int id);
    }
    public class LaborContractService : ILaborContractService
    {
        private readonly HttpClient _httpClient;
        public LaborContractService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task Add(LaborContractToAdd LaborContractToAdd)
        {
            await _httpClient.PostAsJsonAsync("/api/LaborContract/Insert", LaborContractToAdd);
        }

        public async Task Delete(int id)
        {
            await _httpClient.DeleteAsync($"/api/LaborContract/Delete?id={id}");
        }

        public async Task<List<LaborContractToGet>> GetList(Guid Id)
        {
            return await _httpClient.GetFromJsonAsync<List<LaborContractToGet>>($"/api/LaborContract/GetList?id={Id}");
        }

        public async Task Update(LaborContractToUpdate LaborContractToUpdate)
        {
            await _httpClient.PutAsJsonAsync("/api/LaborContract/Update", LaborContractToUpdate);
        }
    }
}
