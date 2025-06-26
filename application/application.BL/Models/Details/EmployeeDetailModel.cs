using System.Collections.ObjectModel;
using application.BL.Models.Lists;

namespace application.BL.Models.Details;

public class EmployeeDetailModel : EmployeeListModel
{
    public ObservableCollection<CpListModel> Trips { get; set; } = new();
    public int NumberOfTrips => Trips.Count;
}