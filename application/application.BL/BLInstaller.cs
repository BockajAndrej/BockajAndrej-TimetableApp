using Microsoft.Extensions.DependencyInjection;
using application.BL.Facades;
using application.BL.Facades.RelationSet;
using application.BL.Models.Details;
using application.BL.Models.Lists;
using application.BL.Models.RelationSet;

namespace application.BL
{
    public static class BLInstaller
    {
        public static IServiceCollection AddBlServices(this IServiceCollection services)
        {
            //Facades
            services.AddTransient<EmployeeFacade>();
            services.AddTransient<CityFacade>();
            services.AddTransient<CpFacade>();
            services.AddTransient<VehicleFacade>();
            //RelationSet
            services.AddTransient<TransportFacade>();

            //Models
            services.AddTransient<EmployeeListModel>();
            services.AddTransient<EmployeeDetailModel>();
            services.AddTransient<CityListModel>();
            services.AddTransient<CityDetailModel>();
            services.AddTransient<CpListModel>();
            services.AddTransient<CpDetailModel>();
            services.AddTransient<VehicleListModel>();
            services.AddTransient<VehicleDetailModel>();
            //RelationSet
            services.AddTransient<TransportListModel>();


            //Automapper
            services.AddAutoMapper(typeof(EmployeeListModel).Assembly);
            services.AddAutoMapper(typeof(EmployeeDetailModel).Assembly);
            services.AddAutoMapper(typeof(CityListModel).Assembly);
            services.AddAutoMapper(typeof(CityDetailModel).Assembly);
            services.AddAutoMapper(typeof(CpListModel).Assembly);
            services.AddAutoMapper(typeof(CpDetailModel).Assembly);
            services.AddAutoMapper(typeof(VehicleListModel).Assembly);
            services.AddAutoMapper(typeof(VehicleDetailModel).Assembly);
            //RelationSet
            services.AddAutoMapper(typeof(TransportListModel).Assembly);

            return services;
        }
    }
}
