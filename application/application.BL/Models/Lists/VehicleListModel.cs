using CommunityToolkit.Mvvm.ComponentModel;

namespace application.BL.Models.Lists;

public partial class VehicleListModel : ObservableObject
{
    public int Id { get; set; }
    public required string VehicleName { get; set; }

    [ObservableProperty]
    private bool _iSelectedFromFilter = false;
}