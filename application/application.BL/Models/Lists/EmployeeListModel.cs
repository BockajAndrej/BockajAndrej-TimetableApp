using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel;

namespace application.BL.Models.Lists;

public partial class EmployeeListModel : ObservableObject
{
    [ObservableProperty]
    private string _id;
    [ObservableProperty]
    private string _firstName;
    [ObservableProperty]
    private string _lastName;
    [ObservableProperty]
    private DateOnly _birthDay;
    [ObservableProperty]
    private string _birthNumber;

    //Just Auxiliary for UI
    public string FullName => $"{FirstName} {LastName}";

    [ObservableProperty]
    private bool _iSelectedFromEmployeeFilter = false;
    [ObservableProperty]
    private bool _isClickedFromEmployeeFilter = false;

    [ObservableProperty]
    private bool _idIsUsed = false;

    public bool IdIsUsedInverseValue => !IdIsUsed;

    //Used by Datepicker
    public DateTime BirthDayDateTime
    {
        get { return BirthDay.ToDateTime(TimeOnly.MinValue); }
        set
        {
            if (BirthDay != DateOnly.FromDateTime(value))
            {
                BirthDay = DateOnly.FromDateTime(value);
                OnPropertyChanged(nameof(BirthDayDateTime));
            }
        }
    }
}