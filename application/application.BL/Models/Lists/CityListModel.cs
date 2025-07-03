using CommunityToolkit.Mvvm.ComponentModel;

namespace application.BL.Models.Lists;

public partial class CityListModel : ObservableObject
{
    [ObservableProperty]
    private int _id;
    [ObservableProperty]
    private decimal _latitude;
    [ObservableProperty]
    private decimal _longitude;
    [ObservableProperty]
    private string _cityName;
    [ObservableProperty]
    private string _stateName;


    [ObservableProperty]
    private bool _iSelectedFromFilter = false;
}