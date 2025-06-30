using System.Collections.ObjectModel;
using application.BL.Facades;
using application.BL.Models.Details;
using CommunityToolkit.Mvvm.ComponentModel;

namespace application.App.Pages.ViewModel
{
    public partial class MainPageViewModel : ObservableObject
    {
        private readonly EmployeeFacade _employeeFacade;
        [ObservableProperty]
        public ICollection<EmployeeDetailModel> _employeeDetailModels = new ObservableCollection<EmployeeDetailModel>();
        [ObservableProperty]
        public List<string> _ahojList = new();
        [ObservableProperty]
        private string _ahojString = "Toto chcem";
        public MainPageViewModel(EmployeeFacade employeeFacade)
        {
            _employeeFacade = employeeFacade;
            loadData();
        }

        public async Task loadData()
        {
            EmployeeDetailModels = await _employeeFacade.GetAsync();
            AhojList.Add("ahoj");
            AhojList.Add("ako");
            AhojList.Add("je");
            return;
        }
    }
}
