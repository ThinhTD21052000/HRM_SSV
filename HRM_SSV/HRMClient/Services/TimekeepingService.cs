using Domain.Modals.Timekeeping;
using System.Net.Http.Json;

namespace HRMClient.Services
{
    public interface ITimekeepingService
    {
        Task<List<TimekeepingToGet>> GetList(int id);
        Task<List<TimekeepingToGet>> GetListByDate(DateTime date);
        Task Add(TimekeepingToAdd TimekeepingToAdd);
        Task Update(TimekeepingToUpdate TimekeepingToUpdate);
        Task Delete(int id);
    }
    public class TimekeepingService : ITimekeepingService
    {
        private readonly HttpClient _httpClient;
        public TimekeepingService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task Add(TimekeepingToAdd TimekeepingToAdd)
        {
            await _httpClient.PostAsJsonAsync("/api/Timekeeping/Insert", TimekeepingToAdd);
        }

        public async Task Delete(int id)
        {
            await _httpClient.DeleteAsync($"/api/Timekeeping/Delete?id={id}");
        }

        public async Task<List<TimekeepingToGet>> GetList(int id)
        {
            return await _httpClient.GetFromJsonAsync<List<TimekeepingToGet>>($"/api/Timekeeping/GetList?id={id}");
        }

        public async Task<List<TimekeepingToGet>> GetListByDate(DateTime date)
        {
            return await _httpClient.GetFromJsonAsync<List<TimekeepingToGet>>($"/api/Timekeeping/GetListByDate?date={date}");
        }

        public async Task Update(TimekeepingToUpdate TimekeepingToUpdate)
        {
            await _httpClient.PutAsJsonAsync("/api/Timekeeping/Update", TimekeepingToUpdate);
        }
    }
}
