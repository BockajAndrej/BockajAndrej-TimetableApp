using application.BL.Mappers;
using application.DAL.Factories;
using AutoMapper;
using Xunit.Abstractions;

namespace application.BL.Tests.FacadeTests;
[Collection("Sequential")]
public class FacadeTests : IDisposable
{
    protected readonly ITestOutputHelper TestOutputHelper;
    protected readonly DbContexCpFactory Factory;
    protected readonly IMapper Mapper;
    
    public FacadeTests(ITestOutputHelper testOutputHelper)
    {
        TestOutputHelper = testOutputHelper;
        Factory = new DbContexCpFactory(@"Server=(localdb)\MSSQLLocalDB;Database=applicationDb;Trusted_Connection=True;");
        
        var mapperProfile = new MapperProfile();
        var mapperConfig = mapperProfile.Config();
        Mapper = mapperConfig.CreateMapper();
    }
    
    public void Dispose()
    {
        Factory?.Dispose();
    }
}