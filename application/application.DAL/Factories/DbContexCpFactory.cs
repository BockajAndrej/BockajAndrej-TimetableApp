using Microsoft.EntityFrameworkCore;

namespace application.DAL.Factories;

public class DbContexCpFactory(string connectionString) : DbContext
{
    public MyDbContext CreateDbContext()
    {
        var optionsBuilder = new DbContextOptionsBuilder<MyDbContext>();
        optionsBuilder.UseSqlServer(connectionString);
        optionsBuilder.EnableSensitiveDataLogging();
        optionsBuilder.LogTo(Console.WriteLine);
        
        return new MyDbContext(optionsBuilder.Options);
    }
}