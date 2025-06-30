using application.App.Pages.View;
using Microsoft.Extensions.DependencyInjection;

namespace application.App
{
    public partial class App : Application
    {
        private IServiceProvider _serviceProvider;
        public App(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            //MainPage = serviceProvider.GetRequiredService<AppShell>();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            //return new Window(new AppShell());
            return new Window(_serviceProvider.GetRequiredService<MainPageView>());
        }
    }
}