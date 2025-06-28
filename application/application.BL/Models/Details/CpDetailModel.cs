using System.Collections.ObjectModel;
using application.BL.Models.Lists;
using application.BL.Models.RelationSet;

namespace application.BL.Models.Details;

public class CpDetailModel : CpListModel
{
    public ObservableCollection<VehicleListModel> VehicleList { get; set; } = new();
    public ObservableCollection<TransportListModel> TransportList { get; set; } = new();
}