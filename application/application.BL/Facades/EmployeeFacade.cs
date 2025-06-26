using application.BL.Mappers;
using application.BL.Models;
using application.BL.Models.Details;
using application.DAL;
using application.DAL.Entities;
using application.DAL.Factories;
using application.DAL.Seeds;
using AutoMapper;

namespace application.BL.Facades;

public class EmployeeFacade(
    DbContexCpFactory factory, 
    IMapper mapperProfile
    ) : Facade<Employee, EmployeeDetailModel>(factory, mapperProfile)
{
}