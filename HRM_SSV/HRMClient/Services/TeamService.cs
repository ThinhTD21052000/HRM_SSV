using Domain.Modals.Team;
using System.Net.Http.Json;

namespace HRMClient.Services
{
    public interface ITeamService
    {
        Task<List<TeamToGet>> GetList();
        Task Add(TeamToAdd TeamToAdd);
        Task Update(TeamToUpdate TeamToUpdate);
        Task Delete(int id);
    }
    public class TeamService : ITeamService
    {
        private readonly HttpClient _httpClient;
        public TeamService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task Add(TeamToAdd TeamToAdd)
        {
            await _httpClient.PostAsJsonAsync("/api/Team/Insert", TeamToAdd);
        }

        public async Task Delete(int id)
        {
            await _httpClient.DeleteAsync($"/api/Team/Delete?id={id}");
        }

        public async Task<List<TeamToGet>> GetList()
        {
            return await _httpClient.GetFromJsonAsync<List<TeamToGet>>("/api/Team/GetList");
        }

        public async Task Update(TeamToUpdate TeamToUpdate)
        {
            await _httpClient.PutAsJsonAsync("/api/Team/Update", TeamToUpdate);
        }
    }
}
