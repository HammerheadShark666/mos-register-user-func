using Microsoft.EntityFrameworkCore;

namespace Microservice.Register.Function.Data.Context;

public class UserDbContext(DbContextOptions<UserDbContext> options) : DbContext(options)
{
    public DbSet<Domain.User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Domain.User>().HasData(DefaultData.GetUserDefaultData());
    }
}