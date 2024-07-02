using Microsoft.EntityFrameworkCore;

namespace Microservice.Register.Function.Data.Contexts;

public class UserDbContext : DbContext
{
    DefaultData _defaultData;

    public UserDbContext(DbContextOptions<UserDbContext> options, DefaultData defaultData) : base(options) 
    {
        _defaultData = defaultData;
    } 

    public DbSet<Domain.User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder); 

        modelBuilder.Entity<Domain.User>().HasData(_defaultData.GetUserDefaultData());
    }
}