using application.BL.Facades;
using application.BL.Models.Details;
using application.DAL.Entities;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq.Expressions;

namespace application.App.Pages.ViewModel
{
    public partial class MainPageViewModel : ObservableObject
    {
        //Facades
        private readonly EmployeeFacade _employeeFacade;
        private readonly CpFacade _cpFacade;
        private readonly CityFacade _cityFacade;
        private readonly VehicleFacade _vehicleFacade;

        //Collections
        private ObservableCollection<EmployeeDetailModel> _employeeDetailModels;
        public ObservableCollection<EmployeeDetailModel> EmployeeDetailModels
        {
            get => _employeeDetailModels;
            set
            {
                if (_employeeDetailModels != null)
                {
                    _employeeDetailModels.CollectionChanged -= EmployeeDetailModels_CollectionChanged;
                    foreach (var employee in _employeeDetailModels)
                        employee.PropertyChanged -= Employee_PropertyChanged;
                }

                SetProperty(ref _employeeDetailModels, value);
                if (_employeeDetailModels != null)
                {
                    _employeeDetailModels.CollectionChanged += EmployeeDetailModels_CollectionChanged;
                    foreach (var employee in _employeeDetailModels)
                        employee.PropertyChanged += Employee_PropertyChanged;
                }
            }
        }

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
        private Expression<Func<Cp, bool>> _predicateEmployeeForCp = l => true;
        private Expression<Func<Cp, bool>> _predicateVehicleForCp = l => true;
        private Expression<Func<Cp, bool>> _predicateCityForCp = l => true;

        //Variables
        public EmployeeDetailModel IsClickedemployeeDetailModel;

        public MainPageViewModel(EmployeeFacade employeeFacade, CpFacade cpFacade, CityFacade cityFacade,
            VehicleFacade vehicleFacade)
        {
            _employeeFacade = employeeFacade;
            _cpFacade = cpFacade;
            _cityFacade = cityFacade;
            _vehicleFacade = vehicleFacade;

            LoadData();
        }
        public async Task LoadData()
        {
            var result = await _employeeFacade.GetAsync();
            EmployeeDetailModels = new ObservableCollection<EmployeeDetailModel>(result);
            CpDetailModels = await _cpFacade.GetAsync();
            CityDetailModels = await _cityFacade.GetAsync();
            VehicleDetailModels = await _vehicleFacade.GetAsync();
        }

        async partial void OnSearchbarEmployeeChanged(string? value)
        {
            EmployeeDetailModels.Clear();

            string lowerSearchTerm = SearchbarEmployee.Trim().ToLower();
            Expression<Func<Employee, bool>> predicateEmployee = lamb =>
                lamb.FirstName.ToLower().Contains(lowerSearchTerm) || lamb.LastName.ToLower().Contains(lowerSearchTerm);

            if (SearchbarEmployee != string.Empty)
            {
                var result = await _employeeFacade.GetAsync(predicateEmployee);
                EmployeeDetailModels = new ObservableCollection<EmployeeDetailModel>(result);
            }
            else
            {
                var result = await _employeeFacade.GetAsync();
                EmployeeDetailModels = new ObservableCollection<EmployeeDetailModel>(result);
            }
        }

        async partial void OnSearchbarCpChanged(string? value)
        {
            await LoadDataCpQuery();
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

            Cppredicate = Combine(_predicateEmployeeForCp, Expression.AndAlso, filter);

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

        private void EmployeeDetailModels_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            //Added Items
            if (e.NewItems != null)
            {
                foreach (EmployeeDetailModel newEmployee in e.NewItems)
                    newEmployee.PropertyChanged += Employee_PropertyChanged;
            }

            //Removed Items
            if (e.OldItems != null)
            {
                foreach (EmployeeDetailModel oldEmployee in e.OldItems)
                    oldEmployee.PropertyChanged -= Employee_PropertyChanged;
            }
        }

        private void Employee_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(EmployeeDetailModel.ISelectedFromEmployeeFilter))
            {
                UpdateSelectedEmployees();
            }
        }

        [RelayCommand]
        public void BtnClickedEmployeeFromFilter(EmployeeDetailModel employee)
        {
            IsClickedemployeeDetailModel = employee;
        }

        public async Task UpdateSelectedEmployees()
        {
            int cntOfSelectedEmployees = 0;
            _predicateEmployeeForCp = l => false;
            foreach (var employee in EmployeeDetailModels)
            {
                if (employee.ISelectedFromEmployeeFilter)
                {
                    cntOfSelectedEmployees++;
                    Expression<Func<Cp, bool>> filter = lamb => lamb.IdEmployee == employee.Id;
                    _predicateEmployeeForCp = Combine(_predicateEmployeeForCp, Expression.OrElse, filter);
                }
            }
            if (cntOfSelectedEmployees == 0)
                _predicateEmployeeForCp = l => true;

            await LoadDataCpQuery();
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