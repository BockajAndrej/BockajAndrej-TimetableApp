using System.Runtime.CompilerServices;
using application.DAL.Entities;
using application.DAL.Factories;
using Xunit.Abstractions;

namespace application.DAL.Tests;

public class UnitTest1 : IDisposable
{
    private readonly ITestOutputHelper _testOutputHelper;
    private DbContexCpFactory _contextFactory;

    public UnitTest1(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        _contextFactory = new DbContexCpFactory(@"Server=(localdb)\MSSQLLocalDB;Database=applicationDb;Trusted_Connection=True;");
    }

    [Fact]
    public void Test1()
    {
        var context = _contextFactory.CreateDbContext();
        
        //Arranges
        var newEmployee = new Employee
        {
            Id = "EMP004",
            FirstName = "Test",
            LastName = "Zamestnanec",
            BirthNumber = "000000/0000",
            BirthDay = new DateOnly(2000, 1, 1)
        };
        
        //Act
        context.Employees.Add(newEmployee);
        context.SaveChanges();
        var context2 = _contextFactory.CreateDbContext();
        
        //Assert
        var empList2 = context2.Employees.ToList();
        foreach (var emp1 in empList2)
            _testOutputHelper.WriteLine(emp1.Id + " " + emp1.FirstName  + " " + emp1.LastName);
        
        _testOutputHelper.WriteLine("------------------------------");
        
        var context3 = _contextFactory.CreateDbContext();
        context3.Employees.Remove(newEmployee);
        context3.SaveChanges();
        
        //Assert
        var empList3 = context3.Employees.ToList();
        foreach (var emp1 in empList3)
            _testOutputHelper.WriteLine(emp1.Id + " " + emp1.FirstName  + " " + emp1.LastName);
    }

    public void Dispose()
    {
    }
}

