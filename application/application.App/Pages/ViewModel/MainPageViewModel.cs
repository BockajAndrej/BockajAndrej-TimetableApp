using application.App.Pages.View.Popups;
using application.BL.Facades;
using application.BL.Models.Details;
using application.DAL.Entities;
using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq.Expressions;
using System.Text;

namespace application.App.Pages.ViewModel
{
    public partial class MainPageViewModel : ObservableObject
    {
        private readonly EmployeeFacade _employeeFacade;
        private readonly CpFacade _cpFacade;
        private readonly CityFacade _cityFacade;
        private readonly VehicleFacade _vehicleFacade;

        [ObservableProperty]
        public ICollection<EmployeeDetailModel> _employeeDetailModels = new ObservableCollection<EmployeeDetailModel>();
        [ObservableProperty]
        public ICollection<CpDetailModel> _cpDetailModels = new ObservableCollection<CpDetailModel>();
        [ObservableProperty]
        public ICollection<CityDetailModel> _cityDetailModels = new ObservableCollection<CityDetailModel>();
        [ObservableProperty]
        public ICollection<VehicleDetailModel> _vehicleDetailModels = new ObservableCollection<VehicleDetailModel>();

        //SearchBars
        [ObservableProperty]
        private string _searchbarEmployee = string.Empty;
        [ObservableProperty]
        private string _searchbarCp = string.Empty;

        //Predicates
        private Expression<Func<Cp, bool>> predicateEmployeeForCp;
        private Expression<Func<Cp, bool>> predicateVehicleForCp;
        private Expression<Func<Cp, bool>> predicateCityForCp;

        //Selections
        [ObservableProperty]
        private EmployeeDetailModel? _selectedEmployee;

        [ObservableProperty]
        public ObservableCollection<object> _selectedEmployeeList = new ObservableCollection<object>();
        private int _lastSelectedEmployeeCount = 0;
        private int _actualSelectedEmployeeIndex = 0;


        //(EmployeeFacade employeeFacade, CpFacade cpFacade, CityFacade cityFacade, VehicleFacade vehicleFacade)
        public MainPageViewModel(EmployeeFacade employeeFacade, CpFacade cpFacade, CityFacade cityFacade,
            VehicleFacade vehicleFacade)
        {
            _employeeFacade = employeeFacade;
            _cpFacade = cpFacade;
            _cityFacade = cityFacade;
            _vehicleFacade = vehicleFacade;

            SelectedEmployeeList.CollectionChanged += SelectedEmployeeListOnCollectionChanged;

            LoadData();
        }

        public async Task LoadData()
        {
            EmployeeDetailModels = await _employeeFacade.GetAsync();
            CpDetailModels = await _cpFacade.GetAsync();
            CityDetailModels = await _cityFacade.GetAsync();
            VehicleDetailModels = await _vehicleFacade.GetAsync();
        }

        [RelayCommand]
        private async Task LoadDataEmployeeQuery()
        {
            EmployeeDetailModels.Clear();

            string lowerSearchTerm = SearchbarEmployee.Trim().ToLower();
            Expression<Func<Employee, bool>> predicateEmployee = lamb =>
                lamb.FirstName.ToLower().Contains(lowerSearchTerm) || lamb.LastName.ToLower().Contains(lowerSearchTerm);

            if (SearchbarEmployee != string.Empty)
                EmployeeDetailModels = await _employeeFacade.GetAsync(filter: predicateEmployee);
            else
                EmployeeDetailModels = await _employeeFacade.GetAsync();
        }

        [RelayCommand]
        public async Task LoadDataCpQuery()
        {
            Expression<Func<Cp, bool>> Cppredicate;

            string lowerSearchTerm = SearchbarCp.Trim().ToLower();
            Expression<Func<Cp, bool>> filter = lamb => lamb.IdEmployeeNavigation.FirstName.ToLower().Contains(lowerSearchTerm)
                                                 || lamb.IdEmployeeNavigation.LastName.ToLower()
                                                     .Contains(lowerSearchTerm)
                                                 || lamb.CpState.ToLower().Contains(lowerSearchTerm);

            Cppredicate = Combine(predicateEmployeeForCp, Expression.AndAlso, filter);

            CpDetailModels.Clear();
            CpDetailModels = await _cpFacade.GetAsync(filter: Cppredicate);
        }

        public async Task<bool> SaveEmployeeAsync(EmployeeDetailModel employeeDetail)
        {
            var result = await _employeeFacade.SaveAsync(employeeDetail);
            if (result.Id == String.Empty)
            {
                return false;
            }

            return true;
        }

        partial void OnSelectedEmployeeChanged(EmployeeDetailModel? oldValue, EmployeeDetailModel? newValue)
        {
            if (newValue != null)
                predicateEmployeeForCp = l => l.IdEmployee == newValue.Id;
            else
                predicateEmployeeForCp = l => true;
            LoadDataCpQuery();
        }

        private void SelectedEmployeeListOnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    _actualSelectedEmployeeIndex++;
                    break;
                case NotifyCollectionChangedAction.Reset:
                    _actualSelectedEmployeeIndex = 0;
                    break;
                default:
                    break;
            }

            if (_actualSelectedEmployeeIndex + 1 >= _lastSelectedEmployeeCount && _actualSelectedEmployeeIndex != _lastSelectedEmployeeCount)
            {
                _lastSelectedEmployeeCount = _actualSelectedEmployeeIndex;

                if (_lastSelectedEmployeeCount == 0)
                    predicateEmployeeForCp = l => true;
                else
                    predicateEmployeeForCp = l => false;
                foreach (var employeeDetail in SelectedEmployeeList.OfType<EmployeeDetailModel>().ToList())
                {
                    Expression<Func<Cp, bool>> filter = lamb => lamb.IdEmployee == employeeDetail.Id;
                    predicateEmployeeForCp = Combine(predicateEmployeeForCp, Expression.OrElse, filter);
                }
            }
        }

        //Auxiliary Functions
        private Expression<Func<T, bool>> Combine<T>(Expression<Func<T, bool>> predicate, Func<Expression, Expression, BinaryExpression> combination, Expression<Func<T, bool>> withPredicate)
        {
            var invocation = Expression.Invoke(withPredicate, predicate.Parameters);
            var combined = combination(predicate.Body, invocation);
            return Expression.Lambda<Func<T, bool>>(combined, predicate.Parameters);
        }
    }
}