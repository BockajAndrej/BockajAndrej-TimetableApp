using System.Collections.ObjectModel;
using application.BL.Models.Lists;

namespace application.BL.Models.Details;

public class CityDetailModel : CityListModel
{
    public ObservableCollection<CpListModel> TripsStarted = new();
    public ObservableCollection<CpListModel> TripsEnded = new();
}