using System.Collections.ObjectModel;
using application.BL.Models.Lists;
using application.BL.Models.RelationSet;

namespace application.BL.Models.Details;

public class VehicleDetailModel : VehicleListModel
{
    public ObservableCollection<CpListModel> CpList = new();
    public ObservableCollection<TransportListModel> TransportList = new();
}