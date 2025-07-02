using CommunityToolkit.Mvvm.ComponentModel;

namespace application.BL.Models.Lists;

public partial class EmployeeListModel : ObservableObject
{
    public required string Id { get; set; } = null!;

    public required string FirstName { get; set; } = null!;
    public required string LastName { get; set; } = null!;

    public required DateOnly BirthDay { get; set; }
    public required string BirthNumber { get; set; } = null!;

    public string FullName => $"{FirstName} {LastName}";

    [ObservableProperty]
    private bool _iSelectedFromEmployeeFilter = false;
}