using System.Collections.ObjectModel;
using System.ComponentModel;
using application.BL.Models.Details;
using application.BL.Models.Lists;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;

namespace application.App.Pages.View.Popups;

public partial class EmployeeEditPopup : Popup<EmployeeDetailModel?>, INotifyPropertyChanged
{
    public EmployeeDetailModel? EmpDetail;
    private string _idLocal { get; set; }
    public string IdLocal
    {
        get { return _idLocal; }
        set
        {
            if (_idLocal != value)
            {
                _idLocal = value;
                OnPropertyChanged(nameof(IdLocal));
            }
        }
    }
    private string _firstName { get; set; }
    public string FirstName
    {
        get { return _firstName; }
        set
        {
            if (_firstName != value)
            {
                _firstName = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }
    }
    private string _lastName { get; set; }
    public string LastName
    {
        get { return _lastName; }
        set
        {
            if (_lastName != value)
            {
                _lastName = value;
                OnPropertyChanged(nameof(LastName));
            }
        }
    }
    private DateOnly _birthDay { get; set; }
    public DateOnly BirthDay
    {
        get { return _birthDay; }
        set
        {
            if (_birthDay != value)
            {
                _birthDay = value;
                OnPropertyChanged(nameof(BirthDay));
                OnPropertyChanged(nameof(BirthDayDateTime));
            }
        }
    }
    public DateTime BirthDayDateTime
    {
        get { return BirthDay.ToDateTime(TimeOnly.MinValue); }
        set
        {
            if (BirthDay != DateOnly.FromDateTime(value))
            {
                BirthDay = DateOnly.FromDateTime(value);
                OnPropertyChanged(nameof(BirthDay));
                OnPropertyChanged(nameof(BirthDayDateTime));
            }
        }
    }
    private string _birthNum { get; set; }
    public string BirthNum
    {
        get { return _birthNum; }
        set
        {
            if (_birthNum != value)
            {
                _birthNum = value;
                OnPropertyChanged(nameof(BirthNum));
            }
        }
    }


    public EmployeeEditPopup(EmployeeDetailModel? empDetail)
    {
        InitializeComponent();
        BindingContext = this;

        EmpDetail = empDetail;
        if (EmpDetail != null)
        {
            IdLocal = EmpDetail.Id;
            FirstName = EmpDetail.FirstName;
            LastName = EmpDetail.LastName;
            BirthDay = EmpDetail.BirthDay;
            BirthNum = EmpDetail.BirthNumber;
        }
        else
        {
            EmpDetail = new EmployeeDetailModel
            {
                Id = null,
                FirstName = null,
                LastName = null,
                BirthDay = default,
                BirthNumber = null,
                Trips = new ObservableCollection<CpListModel>()
            };

            IdLocal = "";
            FirstName = "";
            LastName = "";
            BirthDay = default;
            BirthNum = "";
        }
    }
    async void OnSaveClicked(object? sender, EventArgs e)
    {
        EmpDetail.Id = IdLocal;
        EmpDetail.FirstName = FirstName;
        EmpDetail.LastName = LastName;
        EmpDetail.BirthDay = BirthDay;
        EmpDetail.BirthNumber = BirthNum;

        await CloseAsync(EmpDetail);
    }

    async void OnCloseTapped(object? sender, EventArgs e)
    {
        await CloseAsync(null);
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}