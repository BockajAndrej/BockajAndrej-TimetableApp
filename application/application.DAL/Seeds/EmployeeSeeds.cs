using application.DAL.Entities;

namespace application.DAL.Seeds;

public static class EmployeeSeeds
{
    public static readonly Employee Chetta = new()
    {
        Id = "EMP004",
        FirstName = "Chetta",
        LastName = "Test Employee",
        BirthNumber = "123456/7890",
        BirthDay = new DateOnly(1950, 2, 1),
    };
    
    public static readonly Employee Ramirez = new()
    {
        Id = "EMP005",
        FirstName = "Ramirez",
        LastName = "Employee last name",
        BirthNumber = "123456/7890",
        BirthDay = new DateOnly(1980, 6, 15),
    };
    
    public static readonly Employee TameImpala = new()
    {
        Id = "EMP006",
        FirstName = "TameImpala",
        LastName = "Employee last name",
        BirthNumber = "123456/7890",
        BirthDay = new DateOnly(2000, 7, 24),
    };
    
    public static MyDbContext Seed(MyDbContext db)
    {
        db.Set<Employee>().AddRange(
            Chetta,
            Ramirez,
            TameImpala
        );
        return db;
    }
}