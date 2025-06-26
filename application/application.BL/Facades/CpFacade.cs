using application.DAL.Entities;
using application.DAL.Factories;

namespace application.BL.Facades;

public class CpFacade(DbContexCpFactory factory) : Facade<Cp>(factory)
{
    
}