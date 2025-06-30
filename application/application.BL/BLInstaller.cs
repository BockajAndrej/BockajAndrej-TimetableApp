using Microsoft.Extensions.DependencyInjection;
using application.BL.Facades;
using application.BL.Facades.RelationSet;
using application.BL.Models.Details;
using application.BL.Models.Lists;

namespace application.BL
{
    public static class BLInstaller
    {
        public static IServiceCollection AddBlServices(this IServiceCollection services)
        {
            //Facades
            services.AddTransient<EmployeeFacade>();

            //Models
            services.AddTransient<EmployeeListModel>();
            services.AddTransient<EmployeeDetailModel>();

            //RelationSet
            services.AddTransient<TransportFacade>();

            //Automapper
            services.AddAutoMapper(typeof(EmployeeDetailModel).Assembly);
            services.AddAutoMapper(typeof(EmployeeListModel).Assembly);

            return services;
        }
    }
}
