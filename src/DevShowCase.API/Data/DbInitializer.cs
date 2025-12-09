using DevShowCase.API.Models;
namespace DevShowCase.API.Data;

public static class DbInitializer
{
    public static void Seed(IApplicationBuilder applicationBuilder)
    {
        using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
        {
            var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
            if (context == null)
            {
                throw new Exception("Could not retrieve DbContext");
            }
            // Kör migreringar automatiskt (valfritt, men smidigt)
            // context.Database.Migrate();
            // Om vi redan har teman, gör inget
            if (context.Themes.Any()) return;
            // Annars, lägg till standardteman
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
            context.SaveChanges();
        }
    }
}