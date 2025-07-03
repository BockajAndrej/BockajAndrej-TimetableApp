using application.BL.Mappers;
using application.DAL;
using application.DAL.Factories;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;
using Xunit.Abstractions;

namespace application.BL.Tests.FacadeTests;

[CollectionDefinition("Facade Test Collection")]
public class FacadeTestCollection : ICollectionFixture<FacadeTests> { }

public class FacadeTests : IDisposable
{
    public ServiceProvider ServiceProvider { get; private set; }

    protected IDbContextFactory<MyDbContext> Factory;
    protected readonly IMapper Mapper;

    public FacadeTests()
    {
        var services = new ServiceCollection();

        services.AddDalServices();

        ServiceProvider = services.BuildServiceProvider();

        var mapperProfile = new MapperProfile();
        var mapperConfig = mapperProfile.Config();
        Mapper = mapperConfig.CreateMapper();
    }

    public void Dispose()
    {
        ServiceProvider?.Dispose();
    }
}