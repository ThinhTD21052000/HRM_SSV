using Domain.Modals.MonthTimekeeping;
using System.Net.Http.Json;

namespace HRMClient.Services
{
    public interface IMonthTimekeepingService
    {
        Task<List<MonthTimekeepingToGet>> GetList();
        Task Add(MonthTimekeepingToAdd MonthTimekeepingToAdd);
        Task Update(MonthTimekeepingToUpdate MonthTimekeepingToUpdate);
        Task Delete(int id);
    }
    public class MonthTimekeepingService : IMonthTimekeepingService
    {
        private readonly HttpClient _httpClient;
        public MonthTimekeepingService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task Add(MonthTimekeepingToAdd MonthTimekeepingToAdd)
        {
            await _httpClient.PostAsJsonAsync("/api/MonthTimekeeping/Insert", MonthTimekeepingToAdd);
        }

        public async Task Delete(int id)
        {
            await _httpClient.DeleteAsync($"/api/MonthTimekeeping/Delete?id={id}");
        }

        public async Task<List<MonthTimekeepingToGet>> GetList()
        {
            return await _httpClient.GetFromJsonAsync<List<MonthTimekeepingToGet>>("/api/MonthTimekeeping/GetList");
        }

        public async Task Update(MonthTimekeepingToUpdate MonthTimekeepingToUpdate)
        {
            await _httpClient.PutAsJsonAsync("/api/MonthTimekeeping/Update", MonthTimekeepingToUpdate);
        }
    }
}
