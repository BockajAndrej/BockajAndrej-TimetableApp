using System.Collections.ObjectModel;
using application.BL.Models.Lists;

namespace application.BL.Models.RelationSet;

public class TransportListModel
{
    public int Id { get; set; }
    public required CpListModel Cp { get; set; }
    public required VehicleListModel Vehicle { get; set; }
}