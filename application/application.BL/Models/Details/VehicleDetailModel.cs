using System.Collections.ObjectModel;
using application.BL.Models.Lists;

namespace application.BL.Models.Details;

public class VehicleDetailModel : VehicleListModel
{
    public ObservableCollection<CpListModel> Cps = new();
}