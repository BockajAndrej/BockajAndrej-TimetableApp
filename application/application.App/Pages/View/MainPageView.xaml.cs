using application.App.Pages.View.Popups;
using application.App.Pages.ViewModel;
using application.BL.Models.Details;
using application.BL.Models.Lists;
using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Extensions;
using Microsoft.Maui.Controls.Shapes;
using System.Collections.ObjectModel;

namespace application.App.Pages.View;

public partial class MainPageView : ContentPage
{
    private MainPageViewModel _viewModel;
    public MainPageView(MainPageViewModel vm)
    {
        InitializeComponent();
        BindingContext = _viewModel = vm;
    }


    private async void NewEmployeeButton_Clicked(object sender, EventArgs e)
    {
        var createdEmployeeDetail = new EmployeeDetailModel
        {
            Id = "",
            FirstName = "",
            LastName = "",
            BirthDay = default,
            BirthNumber = "",
            Trips = new ObservableCollection<CpListModel>()
        };

        var popup = new EmployeeEditPopup(createdEmployeeDetail);

        IPopupResult<int> popupResult = await this.ShowPopupAsync<int>(popup, new PopupOptions
        {
            Shape = new RoundRectangle
            {
                CornerRadius = new CornerRadius(2),
                Opacity = 0.8f,
                Stroke = Colors.Black,
                BackgroundColor = Colors.Black,
                StrokeThickness = 2
            }
        });

        if (popupResult.WasDismissedByTappingOutsideOfPopup)
            return;

        //Yes was clicked
        if (popupResult.Result == 1)
        {
            await _viewModel.SaveEmployeeAsync(createdEmployeeDetail);
            await _viewModel.LoadData();
        }
    }

    private async void EditEmployeeButton_Clicked(object sender, EventArgs e)
    {
        var popup = new EmployeeEditPopup(_viewModel.IsClickedemployeeDetailModel);

        IPopupResult<int> popupResult = await this.ShowPopupAsync<int>(popup, new PopupOptions
        {
            Shape = new RoundRectangle
            {
                CornerRadius = new CornerRadius(2),
                Opacity = 0.8f,
                Stroke = Colors.Black,
                BackgroundColor = Colors.Black,
                StrokeThickness = 2
            }
        });

        if (popupResult.WasDismissedByTappingOutsideOfPopup)
            return;

        //Yes was clicked
        if (popupResult.Result == 1)
        {
            await _viewModel.SaveEmployeeAsync(_viewModel.IsClickedemployeeDetailModel);
            await _viewModel.LoadData();
        }
        else if (popupResult.Result == 2)
        {
            await _viewModel.RoveEmployeeAsync(_viewModel.IsClickedemployeeDetailModel);
            await _viewModel.LoadData();
        }
    }

    // ExpandedChanged="OnExpanderIsExpandedChanged"
    private void OnExpanderIsExpandedChanged(object sender, CommunityToolkit.Maui.Core.ExpandedChangedEventArgs e)
    {
        //Expanded
        if (e.IsExpanded)
        {
        }
        //Collapsed
        else
        {
            //_viewModel.LoadDataCpQuery();
        }
    }
}