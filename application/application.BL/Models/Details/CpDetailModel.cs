using System.Collections.ObjectModel;
using application.BL.Models.Lists;

namespace application.BL.Models.Details;

public class CpDetailModel : CpListModel
{
    public ObservableCollection<VehicleListModel> VehicleList { get; set; } = new();
}