using application.DAL.Entities;
using application.DAL.Factories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace application.DAL
{
    public static class DALInstaller
    {
        public static IServiceCollection AddDalServices(this IServiceCollection services)
        {
            //Todo: Change location of constring (config file)
            services.AddDbContextFactory<MyDbContext>(options =>
            {
                options.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=applicationDb;Trusted_Connection=True;");
                options.EnableSensitiveDataLogging();
                options.LogTo(Console.WriteLine);
            });

            //services.AddSingleton<City>();
            //services.AddSingleton<Cp>();
            //services.AddSingleton<Employee>();
            //services.AddSingleton<Transport>();
            //services.AddSingleton<Vehicle>();

            return services;
        }
    }
}
