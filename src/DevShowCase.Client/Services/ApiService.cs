using System.Net.Http.Json;
using DevShowcase.Shared.DTOs.Portfolio;
namespace DevShowcase.Client.Services;

public class ApiService(HttpClient httpClient)
{
    // Experience
    public async Task<List<ExperienceDto>> GetExperiencesAsync()
    {
        return await httpClient.GetFromJsonAsync<List<ExperienceDto>>("api/experience") ?? new();
    }
    public async Task<ExperienceDto?> GetExperienceByIdAsync(int id)
    {
        return await httpClient.GetFromJsonAsync<ExperienceDto>($"api/experience/{id}");
    }
    public async Task CreateExperienceAsync(CreateExperienceDto dto)
    {
        await httpClient.PostAsJsonAsync("api/experience", dto);
    }
    public async Task UpdateExperienceAsync(int id, CreateExperienceDto dto)
    {
        await httpClient.PutAsJsonAsync($"api/experience/{id}", dto);
    }
    public async Task DeleteExperienceAsync(int id)
    {
        await httpClient.DeleteAsync($"api/experience/{id}");
    }
    // Projects (Samma mönster)
    public async Task<List<ProjectDto>> GetProjectsAsync()
    {
        return await httpClient.GetFromJsonAsync<List<ProjectDto>>("api/project") ?? new();
    }

    public async Task CreateProjectAsync(CreateProjectDto dto)
    {
        await httpClient.PostAsJsonAsync("api/project", dto);
    }

    public async Task UpdateProjectAsync(int id, CreateProjectDto dto)
    {
        await httpClient.PutAsJsonAsync($"api/project/{id}", dto);
    }
    
    public async Task DeleteProjectAsync(int id)
    {
        await httpClient.DeleteAsync($"api/project/{id}");
    }
}