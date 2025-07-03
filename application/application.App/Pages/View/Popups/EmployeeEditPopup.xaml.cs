using System.Collections.ObjectModel;
using System.ComponentModel;
using application.BL.Models.Details;
using application.BL.Models.Lists;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;

namespace application.App.Pages.View.Popups;

public partial class EmployeeEditPopup : Popup<int>
{
    public EmployeeDetailModel? EmployeeDetailMain;
    public EmployeeDetailModel? EmployeeDetailTmp;

    public EmployeeEditPopup(EmployeeDetailModel EmployeeDetail)
    {
        InitializeComponent();

        EmployeeDetailMain = EmployeeDetail;

        EmployeeDetailTmp = new EmployeeDetailModel
        {
            Id = EmployeeDetailMain.Id,
            FirstName = EmployeeDetailMain.FirstName,
            LastName = EmployeeDetailMain.LastName,
            BirthDay = EmployeeDetailMain.BirthDay,
            BirthNumber = EmployeeDetailMain.BirthNumber,
            Trips = new ObservableCollection<CpListModel>()
        };

        if (EmployeeDetailMain.Id == string.Empty)
            EmployeeDetailTmp.IdIsUsed = true;
        else
            EmployeeDetailTmp.IdIsUsed = false;

        BindingContext = EmployeeDetailTmp;
    }
    async void OnSaveClicked(object? sender, EventArgs e)
    {
        EmployeeDetailMain.Id = EmployeeDetailTmp.Id;
        EmployeeDetailMain.FirstName = EmployeeDetailTmp.FirstName;
        EmployeeDetailMain.LastName = EmployeeDetailTmp.LastName;
        EmployeeDetailMain.BirthDay = EmployeeDetailTmp.BirthDay;
        EmployeeDetailMain.BirthNumber = EmployeeDetailTmp.BirthNumber;

        await CloseAsync(1);
    }

    async void OnCloseTapped(object? sender, EventArgs e)
    {
        await CloseAsync(0);
    }

    async void OnDeleteTapped(object? sender, EventArgs e)
    {
        //You are not allowed to remove item with changed Id
        await CloseAsync(2);
    }
}