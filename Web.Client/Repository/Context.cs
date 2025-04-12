using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using MongoDB.EntityFrameworkCore.Extensions;

namespace Repository;

public class Context : DbContext
{
    public DbSet<Client> Clients { get; init; }

    public static Context Create(IMongoDatabase database) =>
        new(new DbContextOptionsBuilder<Context>()
            .UseMongoDB(database.Client, database.DatabaseNamespace.DatabaseName)
            .Options);

    public Context(DbContextOptions options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Client>().ToCollection("clients");
    }
}
