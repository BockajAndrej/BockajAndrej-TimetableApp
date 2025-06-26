using AutoMapper;

namespace application.BL.Mappers;

public class MapperProfile
{
    public MapperConfiguration Config()
    {
        return new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<EmployeeMapperConfig>();
            cfg.AddProfile<CpMapperConfig>();
        });
    }
}