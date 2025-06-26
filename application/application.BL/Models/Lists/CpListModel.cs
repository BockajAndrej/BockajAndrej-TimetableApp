namespace application.BL.Models.Lists;

public class CpListModel
{
    public int Id { get; set; }
    public DateOnly CreationDate { get; set; }
    public DateTimeOffset StartTime { get; set; }
    public DateTimeOffset EndTime { get; set; }
    public string CpState { get; set; } = null!;
    
    public string IdEmployee { get; set; } = null!;
    public EmployeeListModel IdEmployeeNav { get; set; } = null!;
    public int IdStartCity { get; set; }
    public CityListModel IdStartCityNav { get; set; }
    public int IdEndCity { get; set; }
    public CityListModel IdEndCityNav { get; set; } = null!;
}