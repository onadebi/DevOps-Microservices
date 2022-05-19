using PlatformService.Models;

namespace PlatformService.Data;

public static class PrebDb
{
    public static void PrepPopulation(IApplicationBuilder app)
    {
        using (var serviceScope = app.ApplicationServices.CreateScope())
        {
            SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>());
        }
    }

    private async static void SeedData(AppDbContext context)
    {
        if (!context.Platforms.Any())
        {
            Console.WriteLine("--> Seeding Data...");
            context.Platforms.AddRange(
                new Platform(){Name="DOt Net", Publisher="Microsoft", Cost="Free"},
                new Platform(){Name="SQL Server Express", Publisher="Microsoft", Cost="Free"},
                new Platform(){Name="Kubernetes", Publisher="Cloud Native computing Foundation", Cost="Free"}
            );
            await context.SaveChangesAsync();
        }else{
            Console.WriteLine("--> We already have data");
        }
    }
}