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

    public async Task<DevShowcase.Shared.DTOs.Public.PortfolioDto?> GetPortfolioAsync(string username)
    {
        try
        {
            return await httpClient.GetFromJsonAsync<DevShowcase.Shared.DTOs.Public.PortfolioDto>($"api/portfolio/{username}");
        }
        catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return null;
        }
    }

    // Education
    public async Task<List<EducationDto>> GetEducationAsync()
    {
        return await httpClient.GetFromJsonAsync<List<EducationDto>>("api/education") ?? new();
    }
    public async Task<EducationDto?> GetEducationByIdAsync(int id)
    {
        return await httpClient.GetFromJsonAsync<EducationDto>($"api/education/{id}");
    }
    public async Task CreateEducationAsync(CreateEducationDto dto)
    {
        await httpClient.PostAsJsonAsync("api/education", dto);
    }
    public async Task UpdateEducationAsync(int id, CreateEducationDto dto)
    {
        await httpClient.PutAsJsonAsync($"api/education/{id}", dto);
    }
    public async Task DeleteEducationAsync(int id)
    {
        await httpClient.DeleteAsync($"api/education/{id}");
    }

    // Skills
    public async Task<List<SkillDto>> GetSkillsAsync()
    {
        return await httpClient.GetFromJsonAsync<List<SkillDto>>("api/skill") ?? new();
    }
    public async Task<SkillDto?> GetSkillByIdAsync(int id)
    {
        return await httpClient.GetFromJsonAsync<SkillDto>($"api/skill/{id}");
    }
    public async Task CreateSkillAsync(CreateSkillDto dto)
    {
        await httpClient.PostAsJsonAsync("api/skill", dto);
    }
    public async Task UpdateSkillAsync(int id, CreateSkillDto dto)
    {
        await httpClient.PutAsJsonAsync($"api/skill/{id}", dto);
    }
    public async Task DeleteSkillAsync(int id)
    {
        await httpClient.DeleteAsync($"api/skill/{id}");
    }

    // Profile
    public async Task<DevShowcase.Shared.DTOs.Profile.UpdateProfileDto?> GetMyProfileAsync()
    {
        return await httpClient.GetFromJsonAsync<DevShowcase.Shared.DTOs.Profile.UpdateProfileDto>("api/profile");
    }
    public async Task UpdateProfileAsync(DevShowcase.Shared.DTOs.Profile.UpdateProfileDto dto)
    {
        await httpClient.PutAsJsonAsync("api/profile", dto);
    }
}