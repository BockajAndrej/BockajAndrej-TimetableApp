using System.Collections.ObjectModel;
using application.BL.Models.Lists;

namespace application.BL.Models.Details;

public class CityDetailModel : CityListModel
{
    public ObservableCollection<CpListModel> TripsStarted { get; set; }
    public ObservableCollection<CpListModel> TripsEnded { get; set; }
}