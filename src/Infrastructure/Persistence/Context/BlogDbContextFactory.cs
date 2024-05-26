using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Infrastructure;

public class BlogDbContextFactory : IDesignTimeDbContextFactory<BlogDbContext>
{
    public BlogDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<BlogDbContext>();
        optionsBuilder.UseSqlServer("Server=localhost;Database=AngularBlogYTDB;User Id=SA; Password=Admin@123;TrustServerCertificate=True;");

        return new BlogDbContext(optionsBuilder.Options);
    }
}
