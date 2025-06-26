using application.BL.Facades;
using application.BL.Mappers;
using application.BL.Models;
using application.BL.Models.Details;
using application.DAL;
using application.DAL.Entities;
using application.DAL.Factories;
using AutoMapper;
using Xunit.Abstractions;

namespace application.BL.Tests;

public class FacadeTests
{
    private readonly ITestOutputHelper _testOutputHelper;
    private DbContexCpFactory _factory;
    private IMapper _mapper;
    
    public FacadeTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        _factory = new DbContexCpFactory(@"Server=(localdb)\MSSQLLocalDB;Database=applicationDb;Trusted_Connection=True;");
        var mapperProfile = new MapperProfile();
        var mapperConfig = mapperProfile.Config();
        _mapper = mapperConfig.CreateMapper();
    }

    [Fact]
    public async Task TestGetAsync()
    {
        
        EmployeeFacade empFacade = new EmployeeFacade(_factory, _mapper);

        var empList =  await empFacade.GetAsync(filter: s => s.BirthDay >= new DateOnly(1980, 1, 1));

        foreach (var emp in empList)
        {
            _testOutputHelper.WriteLine(emp.FirstName + " " + emp.LastName);
            foreach (var trip in emp.Trips)
            {
                _testOutputHelper.WriteLine("CP = " + trip.Id);
            }
        }
    }
    
    [Fact]
    public async Task TestSaveAsync()
    {
        EmployeeFacade empFacade = new EmployeeFacade(_factory, _mapper);
        
        var newEmployee = new EmployeeDetailModel()
        {
            Id = "EMP004",
            FirstName = "Tesing",
            LastName = "Employee",
            BirthNumber = "123456/7890",
            BirthDay = new DateOnly(2000, 1, 1)
        };
    
        await empFacade.SaveAsync(newEmployee);
        
        var empList = await empFacade.GetAsync();
        foreach (var emp in empList)
        {
            _testOutputHelper.WriteLine(emp.FirstName + " " + emp.LastName);
        }
    }
    
    [Fact]
    public async Task TestDeleteAsync()
    {
        EmployeeFacade empFacade = new EmployeeFacade(_factory, _mapper);
        
        var newEmployee = new EmployeeDetailModel()
        {
            Id = "EMP004",
        };
        
        // await empFacade.SaveAsync(newEmployee);
        await empFacade.DeleteAsync(newEmployee);
        
        var empList = await empFacade.GetAsync();
        foreach (var emp in empList)
        {
            _testOutputHelper.WriteLine(emp.FirstName + " " + emp.LastName);
        }
    }
    
    [Fact(Skip = "Skipping test because there is unresolved automapper configs")]
    public async Task TestSaveeAsyncWithConnection()
    {
        EmployeeFacade empFacade = new EmployeeFacade(_factory, _mapper);
        CpFacade cpFacade = new CpFacade(_factory, _mapper);
        
        var newEmployee = new EmployeeDetailModel()
        {
            Id = "EMP004",
            FirstName = "Tesing",
            LastName = "Employee",
            BirthNumber = "123456/7890",
            BirthDay = new DateOnly(2000, 1, 1),
        };
        
        //('EMP003', 3, 4, '2023-10-27', '2023-11-03 07:30:00 +01:00', '2023-11-03 11:00:00 +01:00', 'Completed');
        var trip = new CpDetailModel()
        {
            IdStartCity = 3,
            IdEndCity = 4,
            CreationDate = DateOnly.FromDateTime(DateTime.Now),
            StartTime = DateTimeOffset.Now,
            EndTime = DateTimeOffset.Now.AddHours(8),
            CpState = "OK"
        };
        
        trip.IdEmployee = newEmployee.Id;
        newEmployee.Trips.Add(trip);
        
        await empFacade.SaveAsync(newEmployee);
        await cpFacade.SaveAsync(trip);
        
        var cpList = await cpFacade.GetAsync();
        foreach (var cp in cpList)
        {
            var employee = await empFacade.GetAsync(filter: s => s.Id == cp.IdEmployee);
            _testOutputHelper.WriteLine($"{cp.Id} {cp.StartTime} and Employee {employee.First().FirstName} {employee.First().LastName}");
        }
    }
}