using DevShowCase.API.Models;
using Microsoft.AspNetCore.Identity;

namespace DevShowCase.API.Data;

public static class DbInitializer
{
    public static async Task SeedAsync(IApplicationBuilder applicationBuilder)
    {
        using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
        {
            var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
            var userManager = serviceScope.ServiceProvider.GetService<UserManager<User>>();

            if (context == null || userManager == null)
            {
                throw new Exception("Could not retrieve DbContext or UserManager");
            }
            
            // Seed Themes if empty
            if (!context.Themes.Any())
            {
                var themes = new List<Theme>()
                {
                    new Theme
                    {
                        Name = "DevShowcase Minimal",
                        Description = "Clean, professional and timeless design suitable for all developers.",
                        CssFileName = "theme-minimal.css",
                        IsActive = true,
                        TargetAudience = "General",
                        SupportsDarkMode = true,
                        PreviewImageUrl = "/images/themes/minimal.png"
                    },
                    new Theme
                    {
                        Name = "Frontend Creative",
                        Description = "Vibrant colors, gradients and playful animations.",
                        CssFileName = "theme-frontend.css",
                        IsActive = true,
                        TargetAudience = "Frontend",
                        SupportsDarkMode = true,
                        PreviewImageUrl = "/images/themes/frontend.png"
                    },
                    new Theme
                    {
                        Name = "Backend Terminal",
                        Description = "Monospace fonts, terminal esthetics and structured layout.",
                        CssFileName = "theme-backend.css",
                        IsActive = true,
                        TargetAudience = "Backend",
                        SupportsDarkMode = true,
                        PreviewImageUrl = "/images/themes/backend.png"
                    },
                    new Theme
                    {
                        Name = "Fullstack Modern",
                        Description = "Balanced design combining visual flair with technical structure.",
                        CssFileName = "theme-fullstack.css",
                        IsActive = true,
                        TargetAudience = "Fullstack",
                        SupportsDarkMode = true,
                        PreviewImageUrl = "/images/themes/fullstack.png"
                    },
                    new Theme
                    {
                        Name = "Enterprise System",
                        Description = "Robust, corporate and clean design for system architects.",
                        CssFileName = "theme-enterprise.css",
                        IsActive = true,
                        TargetAudience = "System",
                        SupportsDarkMode = true,
                        PreviewImageUrl = "/images/themes/enterprise.png"
                    },
                    new Theme
                    {
                        Name = "Classic Software",
                        Description = "Traditional, book-inspired typography and layout.",
                        CssFileName = "theme-classic.css",
                        IsActive = true,
                        TargetAudience = "Software",
                        SupportsDarkMode = true,
                        PreviewImageUrl = "/images/themes/classic.png"
                    }
                };
                context.Themes.AddRange(themes);
                await context.SaveChangesAsync();
            }

            // Seed John Doe User
            var email = "john.doe@example.com";
            var user = await userManager.FindByEmailAsync(email);
            if (user != null && user.UserName != "johndoe")
            {
                user.UserName = "johndoe";
                user.NormalizedUserName = "JOHNDOE";
                await userManager.UpdateAsync(user);
            }
            
            if (user == null)
            {
                user = new User
                {
                    UserName = "johndoe",
                    Email = email,
                    FirstName = "John",
                    LastName = "Doe",
                    Bio = "Fullstack Developer with a focus on C#/.NET and frontend web security. Experienced in both backend and frontend development.|Certified in Azure AI Fundamentals.",
                    Location = "Gothenburg, Sweden",
                    ProfileImageUrl = "https://raw.githubusercontent.com/Nowaxial/KaffePartyBootstrapFrontendExercise8Lexicon/main/images/HR.png",
                    GithubUrl = "https://github.com/Nowaxial",
                    LinkedInUrl = "https://linkedin.com/in/johndoe",
                    EmailConfirmed = true,
                    PrefersDarkMode = false
                };
                
                var result = await userManager.CreateAsync(user, "Password123!");
                if (result.Succeeded)
                {
                    // Add Experiences
                    context.Experiences.AddRange(
                        new Experience
                        {
                            UserId = user.Id,
                            Company = "Partille Kommun",
                            Position = "Fullstack Developer",
                            Location = "Partille, Västra Götalands län, Sweden",
                            Description = "Working as a fullstack developer intern at Partille kommun's library, focusing on the development and customization of Koha, an open-source library system. Responsibilities include programming and fine-tuning the OPAC interface with CSS, JavaScript, and jQuery.",
                            StartDate = new DateTime(2025, 10, 1),
                            IsCurrent = true,
                            EmploymentType = "Praktik"
                        },
                        new Experience
                        {
                            UserId = user.Id,
                            Company = "Biltema",
                            Position = "Bistro/Café",
                            Location = "Göteborg, Västra Götalands län, Sweden",
                            Description = "Worked in a bistro/café, providing customer service and support. Responsibilities included handling cash registers, serving customers, and maintaining a clean and organized environment.",
                            StartDate = new DateTime(2023, 9, 1),
                            EndDate = new DateTime(2023, 12, 1)
                        },
                        new Experience
                        {
                            UserId = user.Id,
                            Company = "Swedcon18 Ab",
                            Position = "Praktikant in Digital Healthcare Development",
                            Location = "Göteborg, Västra Götalands län, Sweden",
                            Description = "Participated in the development of a digital platform for remote healthcare services. Worked with modern tech-stack including React, Next.js, and Mantine UI.",
                            StartDate = new DateTime(2023, 2, 1),
                            EndDate = new DateTime(2023, 5, 1),
                            EmploymentType = "Praktik"
                        },
                        new Experience
                        {
                            UserId = user.Id,
                            Company = "Webwin Ab",
                            Position = "Fullstack Developer",
                            Location = "Mölndal, Västra Götalands län, Sweden",
                            Description = "Developed and maintained responsive administration interfaces using Bootstrap 5.2. Optimized database communication for performance and security.",
                            StartDate = new DateTime(2022, 11, 1),
                            EndDate = new DateTime(2023, 2, 1),
                            EmploymentType ="Praktik"
                        }
                    );

                    // Add Projects
                    context.Projects.AddRange(
                        new Project
                        {
                            UserId = user.Id,
                            Title = "Library Management System (Koha)",
                            Description = "Customizing and extending open-source library software.",
                            Technologies = "[\"Perl\", \"jQuery\", \"CSS\", \"MySQL\"]",
                            StartDate = new DateTime(2025, 10, 1)
                        },
                        new Project
                        {
                            UserId = user.Id,
                            Title = "Remote Health Platform",
                            Description = "Digital healthcare services dashboard.",
                            Technologies = "[\"React\", \"Next.js\", \"Mantine UI\"]",
                            StartDate = new DateTime(2023, 2, 1)
                        }
                    );

                    // Add Skills
                    context.Skills.AddRange(
                        new Skill
                        {
                            UserId = user.Id,
                            Name = "C#/.NET",
                            
                        },
                        new Skill
                        {
                            UserId = user.Id,
                            Name = "JavaScript",
                            Category = "Frontend"
                        },
                        new Skill
                        {
                            UserId = user.Id,
                            Name = "React",
                            Category = "Frontend"
                        },
                        new Skill
                        {
                            UserId = user.Id,
                            Name = "Bootstrap",
                            Category = "Frontend"
                        },
                        new Skill
                        {
                            UserId = user.Id,
                            Name = "MySQL",
                            Category = "Backend"
                        },
                        new Skill
                        {
                            UserId = user.Id,
                            Name = "Azure",
                            Category = "Fullstack"
                        }
                    );

                    //Add Education
                    context.Education.AddRange(
                        new Education
                        {
                            UserId = user.Id,
                            Institution = "Gymnasium",
                            Degree = "High School",
                            Field = "Computer Science",
                            StartDate = new DateTime(2020, 9, 1),
                            EndDate = new DateTime(2023, 6, 1),
                            Location = "Gothenburg, Sweden"
                        }
                    );

                    // Add Languages
                    context.Languages.AddRange(
                        new Language { UserId = user.Id, Name = "Swedish", ProficiencyLevel = "Native" },
                        new Language { UserId = user.Id, Name = "English", ProficiencyLevel = "Professional" }
                    );

                    // Add TechStacks
                    context.TechStacks.AddRange(
                        new TechStack { UserId = user.Id, Name = ".NET 9", Category = "Backend" },
                        new TechStack { UserId = user.Id, Name = "Blazor WASM", Category = "Frontend" },
                        new TechStack { UserId = user.Id, Name = "Entity Framework Core", Category = "Database" },
                        new TechStack { UserId = user.Id, Name = "Azure", Category = "Cloud" }
                    );

                    // Add Competencies
                    context.Competencies.AddRange(
                        new Competency { UserId = user.Id, Name = "Agile / Scrum", Category = "Methodology" },
                        new Competency { UserId = user.Id, Name = "System Architecture", Category = "Technical" },
                        new Competency { UserId = user.Id, Name = "Unit Testing", Category = "Technical" }
                    );

                    // Add Licenses
                    context.Licenses.AddRange(
                        new License 
                        { 
                            UserId = user.Id, 
                            Name = "Azure AI Fundamentals", 
                            Issuer = "Microsoft", 
                            IssueDate = new DateTime(2024, 5, 12),
                            CredentialId = "AI-900"
                        }
                    );
                    
                    await context.SaveChangesAsync();

                }
            }
        }
    }
}