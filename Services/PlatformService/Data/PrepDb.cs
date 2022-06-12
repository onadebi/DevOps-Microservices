using Microsoft.EntityFrameworkCore;
using PlatformService.Models;

namespace PlatformService.Data;

public static class PrebDb
{
    public static void PrepPopulation(IApplicationBuilder app, bool IsProd)
    {
        using (var serviceScope = app.ApplicationServices.CreateScope())
        {
            SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>(), IsProd);
        }
    }

    private static void SeedData(AppDbContext context, bool IsProd)
    {
        if (IsProd)
        {
            System.Console.WriteLine("-->Attempting to apply migrations");
            try
            {
                context.Database.Migrate();
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine($"-->Migration run failed with error: {ex.Message}");
            }
        }
        try
        {
            if (!context.Platforms.Any())
            {
                Console.WriteLine("--> Seeding Data...");
                context.Platforms.AddRange(
                    new Platform() { Name = "DOt Net", Publisher = "Microsoft", Cost = "Free" },
                    new Platform() { Name = "SQL Server Express", Publisher = "Microsoft", Cost = "Free" },
                    new Platform() { Name = "Kubernetes", Publisher = "Cloud Native computing Foundation", Cost = "Free" }
                );
                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("--> We already have data");
            }
        }
        catch (System.Exception ex)
        {
            System.Console.WriteLine($"--> Failed seeding with error: {ex.Message}");
        }
    }
}