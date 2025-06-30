using application.BL.Facades.Interface;
using application.BL.Models.Details;
using application.DAL;
using application.DAL.Entities;
using application.DAL.Factories;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace application.BL.Facades;

public class EmployeeFacade(
    IDbContextFactory<MyDbContext> factory,
    IMapper mapperProfile
    ) : Facade<Employee, EmployeeDetailModel, MyDbContext>(factory, mapperProfile)
{
}