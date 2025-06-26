using application.DAL;
using application.DAL.Entities;
using application.DAL.Factories;

namespace application.BL.Facades;

public class EmployeeFacade(DbContexCpFactory factory) : Facade<Employee>(factory)
{
}