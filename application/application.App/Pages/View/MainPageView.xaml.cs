using application.App.Pages.ViewModel;

namespace application.App.Pages.View;

public partial class MainPageView : ContentPage
{
    public MainPageView(MainPageViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}