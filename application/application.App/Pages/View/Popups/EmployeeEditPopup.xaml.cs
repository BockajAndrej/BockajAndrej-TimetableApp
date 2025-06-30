using CommunityToolkit.Maui.Views;

namespace application.App.Pages.View.Popups;

public partial class EmployeeEditPopup : Popup
{
    public EmployeeEditPopup()
    {
        InitializeComponent();
    }
    async void OnYesButtonClicked(object? sender, EventArgs e)
    {
        await CloseAsync();
    }

    async void OnNoButtonClicked(object? sender, EventArgs e)
    {
        await CloseAsync();
    }
}