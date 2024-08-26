using Microsoft.EntityFrameworkCore;

namespace Microservice.Register.Function.Data.Context;

public class UserDbContext(DbContextOptions<UserDbContext> options, DefaultData defaultData) : DbContext(options)
{
    DefaultData _defaultData = defaultData;

    public DbSet<Domain.User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Domain.User>().HasData(_defaultData.GetUserDefaultData());
    }
}