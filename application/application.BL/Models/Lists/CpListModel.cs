using CommunityToolkit.Mvvm.ComponentModel;

namespace application.BL.Models.Lists;

public partial class CpListModel : ObservableObject
{
    public int Id { get; set; }
    public required DateOnly CreationDate { get; set; }
    public required DateTimeOffset StartTime { get; set; }
    public required DateTimeOffset EndTime { get; set; }
    public required string CpState { get; set; } = null!;

    public required string IdEmployee { get; set; } = null!;
    public EmployeeListModel IdEmployeeNav { get; set; } = null!;
    public required int IdStartCity { get; set; }
    public CityListModel IdStartCityNav { get; set; }
    public required int IdEndCity { get; set; }
    public CityListModel IdEndCityNav { get; set; } = null!;


    //Auxiliary
    public DateTime StartDateTimeOnly => StartTime.DateTime;
    public DateTime EndDateTimeOnly => EndTime.DateTime;
}