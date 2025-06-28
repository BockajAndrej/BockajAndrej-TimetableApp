using application.BL.Mappers.RelationSet;
using application.BL.Models.Details;
using application.BL.Models.Lists;
using AutoMapper;

namespace application.BL.Mappers;

public class MapperProfile
{
    public MapperConfiguration Config()
    {
        return new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<CityMapperConfig>();
            cfg.AddProfile<CpMapperConfig>();
            cfg.AddProfile<EmployeeMapperConfig>();
            cfg.AddProfile<VehicleMapperConfigv>();
            
            //RelationSet
            cfg.AddProfile<TransportListMapper>();
        });
    }
}