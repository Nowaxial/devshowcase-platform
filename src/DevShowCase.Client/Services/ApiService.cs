using DevShowcase.Shared.DTOs.Portfolio;
using DevShowcase.Shared.DTOs.Profile;
using DevShowcase.Shared.DTOs.Public;
using DevShowcase.Shared.DTOs.Admin;
using System.Net.Http.Json;
namespace DevShowcase.Client.Services;

public class ApiService(HttpClient httpClient)
{
    // Dashboard
    public async Task<DashboardStatsDto> GetDashboardStatsAsync() => await httpClient.GetFromJsonAsync<DashboardStatsDto>("api/dashboard/stats") ?? new();

    // Experience
    public async Task<List<ExperienceDto>> GetExperiencesAsync()
    {
        return await httpClient.GetFromJsonAsync<List<ExperienceDto>>("api/experience") ?? new();
    }
    public async Task<ExperienceDto?> GetExperienceByIdAsync(int id)
    {
        return await httpClient.GetFromJsonAsync<ExperienceDto>($"api/experience/{id}");
    }
    // Experience
    public async Task CreateExperienceAsync(CreateExperienceDto dto)
    {
        var response = await httpClient.PostAsJsonAsync("api/experience", dto);
        response.EnsureSuccessStatusCode();
    }
    public async Task UpdateExperienceAsync(int id, CreateExperienceDto dto)
    {
        var response = await httpClient.PutAsJsonAsync($"api/experience/{id}", dto);
        response.EnsureSuccessStatusCode();
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
        var response = await httpClient.PostAsJsonAsync("api/project", dto);
        response.EnsureSuccessStatusCode();
    }

    public async Task UpdateProjectAsync(int id, CreateProjectDto dto)
    {
        var response = await httpClient.PutAsJsonAsync($"api/project/{id}", dto);
        response.EnsureSuccessStatusCode();
    }

    public async Task DeleteProjectAsync(int id)
    {
        await httpClient.DeleteAsync($"api/project/{id}");
    }

    // Public Portfolio
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
        var response = await httpClient.PostAsJsonAsync("api/education", dto);
        response.EnsureSuccessStatusCode();
    }
    public async Task UpdateEducationAsync(int id, CreateEducationDto dto)
    {
        var response = await httpClient.PutAsJsonAsync($"api/education/{id}", dto);
        response.EnsureSuccessStatusCode();
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
        var response = await httpClient.PostAsJsonAsync("api/skill", dto);
        response.EnsureSuccessStatusCode();
    }
    public async Task UpdateSkillAsync(int id, CreateSkillDto dto)
    {
        var response = await httpClient.PutAsJsonAsync($"api/skill/{id}", dto);
        response.EnsureSuccessStatusCode();
    }
    public async Task DeleteSkillAsync(int id)
    {
        await httpClient.DeleteAsync($"api/skill/{id}");
    }

    // Profile
    public async Task<UpdateProfileDto?> GetMyProfileAsync()
    {
        return await httpClient.GetFromJsonAsync<UpdateProfileDto>("api/profile");
    }
    public async Task UpdateProfileAsync(UpdateProfileDto dto)
    {
        var response = await httpClient.PutAsJsonAsync("api/profile", dto);
        response.EnsureSuccessStatusCode();
    }

    // License
    public async Task<List<LicenseDto>> GetLicensesAsync() => await httpClient.GetFromJsonAsync<List<LicenseDto>>("api/license") ?? new();
    public async Task CreateLicenseAsync(CreateLicenseDto dto) { (await httpClient.PostAsJsonAsync("api/license", dto)).EnsureSuccessStatusCode(); }
    public async Task UpdateLicenseAsync(int id, CreateLicenseDto dto) { (await httpClient.PutAsJsonAsync($"api/license/{id}", dto)).EnsureSuccessStatusCode(); }
    public async Task DeleteLicenseAsync(int id) => await httpClient.DeleteAsync($"api/license/{id}");

    // Competency
    public async Task<List<CompetencyDto>> GetCompetenciesAsync() => await httpClient.GetFromJsonAsync<List<CompetencyDto>>("api/competency") ?? new();
    public async Task CreateCompetencyAsync(CreateCompetencyDto dto) { (await httpClient.PostAsJsonAsync("api/competency", dto)).EnsureSuccessStatusCode(); }
    public async Task UpdateCompetencyAsync(int id, CreateCompetencyDto dto) { (await httpClient.PutAsJsonAsync($"api/competency/{id}", dto)).EnsureSuccessStatusCode(); }
    public async Task DeleteCompetencyAsync(int id) => await httpClient.DeleteAsync($"api/competency/{id}");

    // Language
    public async Task<List<LanguageDto>> GetLanguagesAsync() => await httpClient.GetFromJsonAsync<List<LanguageDto>>("api/language") ?? new();
    public async Task CreateLanguageAsync(CreateLanguageDto dto) { (await httpClient.PostAsJsonAsync("api/language", dto)).EnsureSuccessStatusCode(); }
    public async Task UpdateLanguageAsync(int id, CreateLanguageDto dto) { (await httpClient.PutAsJsonAsync($"api/language/{id}", dto)).EnsureSuccessStatusCode(); }
    public async Task DeleteLanguageAsync(int id) => await httpClient.DeleteAsync($"api/language/{id}");

    // TechStack
    public async Task<List<TechStackDto>> GetTechStacksAsync() => await httpClient.GetFromJsonAsync<List<TechStackDto>>("api/techstack") ?? new();
    public async Task CreateTechStackAsync(CreateTechStackDto dto) { (await httpClient.PostAsJsonAsync("api/techstack", dto)).EnsureSuccessStatusCode(); }
    public async Task UpdateTechStackAsync(int id, CreateTechStackDto dto) { (await httpClient.PutAsJsonAsync($"api/techstack/{id}", dto)).EnsureSuccessStatusCode(); }
    public async Task DeleteTechStackAsync(int id) => await httpClient.DeleteAsync($"api/techstack/{id}");

    // Settings (AI & Preferences)
    public async Task<UserAISettingsDto?> GetAISettingsAsync() => await httpClient.GetFromJsonAsync<UserAISettingsDto>("api/settings/ai");
    public async Task UpdateAISettingsAsync(UserAISettingsDto dto) { (await httpClient.PutAsJsonAsync("api/settings/ai", dto)).EnsureSuccessStatusCode(); }
    public async Task<UserPreferenceDto?> GetPreferencesAsync() => await httpClient.GetFromJsonAsync<UserPreferenceDto>("api/settings/preferences");
    public async Task UpdatePreferencesAsync(UserPreferenceDto dto) { (await httpClient.PutAsJsonAsync("api/settings/preferences", dto)).EnsureSuccessStatusCode(); }

    // Contact Messages
    public async Task<List<AdminContactMessageDto>> GetContactMessagesAsync() => await httpClient.GetFromJsonAsync<List<AdminContactMessageDto>>("api/contact") ?? new();
    public async Task MarkContactMessageAsReadAsync(int id) { (await httpClient.PatchAsync($"api/contact/{id}/read", null)).EnsureSuccessStatusCode(); }
}