using Domain.Modals.Position;
using System.Net.Http.Json;

namespace Client.Services
{
    public interface IPositionService
    {
        Task<List<PositionToGet>> GetList();
        Task Add(PositionToAdd positionToAdd);
        Task Update(PositionToUpdate positionToUpdate);
        Task Delete(int id);
    }
    public class PositionService : IPositionService
    {
        private readonly HttpClient _httpClient;
        public PositionService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task Add(PositionToAdd positionToAdd)
        {
            await _httpClient.PostAsJsonAsync("/api/Position/Insert", positionToAdd);
        }

        public async Task Delete(int id)
        {
            await _httpClient.DeleteAsync($"/api/Position/Delete?id={id}");
        }

        public async Task<List<PositionToGet>> GetList()
        {
            return await _httpClient.GetFromJsonAsync<List<PositionToGet>>("/api/Position/GetList");
        }

        public async Task Update(PositionToUpdate positionToUpdate)
        {
            await _httpClient.PutAsJsonAsync("/api/Position/Update", positionToUpdate);
        }
    }
}
