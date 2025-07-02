using System.Collections.ObjectModel;
using application.BL.Models.Details;
using application.BL.Models.Lists;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;

namespace application.App.Pages.View.Popups;

public partial class EmployeeEditPopup : Popup<EmployeeDetailModel?>
{
    public EmployeeDetailModel? EmpDetail;

    public string IdLocal { get; set; } = "";
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public DateOnly BirthDay { get; set; } = default;
    public string BirthNum { get; set; } = "";
    public EmployeeEditPopup(EmployeeDetailModel? empDetail)
    {
        InitializeComponent();
        BindingContext = this;

        EmpDetail = empDetail;
    }
    async void OnSaveClicked(object? sender, EventArgs e)
    {
        if (EmpDetail == null)
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
        }

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
}