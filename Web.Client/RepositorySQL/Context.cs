using Microsoft.EntityFrameworkCore;

namespace RepositorySQL;

public class Context : DbContext
{
    public DbSet<Client> Clients { get; init; }
    //protected override void OnConfiguring(DbContextOptionsBuilder builder)
    //{
    //    builder.UseSqlServer("Server=localhost;Database=model;User Id=sa;Password=LltF8Nx*yo;Encrypt=True;TrustServerCertificate=True");
    //}
    public Context():base()
    {

    }
    public Context(DbContextOptions options) : base(options)
    {

    }
}
