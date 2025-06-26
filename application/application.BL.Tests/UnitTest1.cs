using application.BL.Facades;
using application.DAL.Factories;
using Xunit.Abstractions;

namespace application.BL.Tests;

public class UnitTest1
{
    private readonly ITestOutputHelper _testOutputHelper;

    public UnitTest1(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public async Task Test1()
    {
        var factory = new DbContexCpFactory(@"Server=(localdb)\MSSQLLocalDB;Database=applicationDb;Trusted_Connection=True;");
        EmployeeFacade empFacade = new EmployeeFacade(factory);

        var empList = await empFacade.GetAsync(filter: s => s.BirthDay >= new DateOnly(1980, 1, 1));

        foreach (var emp in empList)
        {
            _testOutputHelper.WriteLine(emp.FirstName + " " + emp.LastName);
        }
    }
}