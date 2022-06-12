using CommandsService.Models;
using Microsoft.EntityFrameworkCore;

namespace CommandsService.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt): base(opt)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Platform>().HasMany(m=> m.Commands)
                .WithOne(p=> p.Platform)
                .HasForeignKey(p =>p.PlatformId!);
            modelBuilder.Entity<Command>().HasOne(m=> m.Platform)
                .WithMany(p=> p.Commands)
                .HasForeignKey(p => p.PlatformId);
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Command> Commands { get; set; } =default!;
        public DbSet<Platform> Platforms { get; set; } =default!;
    }
}