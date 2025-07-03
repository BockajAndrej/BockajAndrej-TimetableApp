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
        public ObservableCollection<CpDetailModel> _cpDetailModels;

        public ObservableCollection<CityDetailModel> _cityDetailModels;
        public ObservableCollection<CityDetailModel> CityDetailModels
        {
            get => _cityDetailModels;
            set
            {
                if (_cityDetailModels != null)
                {
                    _cityDetailModels.CollectionChanged -= CityDetailModels_CollectionChanged;
                    foreach (var item in _cityDetailModels)
                        item.PropertyChanged -= City_PropertyChanged;
                }

                SetProperty(ref _cityDetailModels, value);
                if (_cityDetailModels != null)
                {
                    _cityDetailModels.CollectionChanged += CityDetailModels_CollectionChanged;
                    foreach (var item in _cityDetailModels)
                        item.PropertyChanged += City_PropertyChanged;
                }
            }
        }
        public ObservableCollection<VehicleDetailModel> _vehicleDetailModels;
        public ObservableCollection<VehicleDetailModel> VehicleDetailModels
        {
            get => _vehicleDetailModels;
            set
            {
                if (_vehicleDetailModels != null)
                {
                    _vehicleDetailModels.CollectionChanged -= VehicleDetailModels_CollectionChanged;
                    foreach (var item in _vehicleDetailModels)
                        item.PropertyChanged -= Vehicle_PropertyChanged;
                }

                SetProperty(ref _vehicleDetailModels, value);
                if (_vehicleDetailModels != null)
                {
                    _vehicleDetailModels.CollectionChanged += VehicleDetailModels_CollectionChanged;
                    foreach (var item in _vehicleDetailModels)
                        item.PropertyChanged += Vehicle_PropertyChanged;
                }
            }
        }

        //SearchBars
        [ObservableProperty]
        private string _searchbarEmployee = string.Empty;
        [ObservableProperty]
        private string _searchbarCp = string.Empty;
        [ObservableProperty]
        private string _searchbarVehicle = string.Empty;
        [ObservableProperty]
        private string _searchbarCity = string.Empty;

        //Predicates
        private Expression<Func<Cp, bool>> _predicateEmployeeForCp = l => true;
        private Expression<Func<Cp, bool>> _predicateVehicleForCp = l => true;
        private Expression<Func<Cp, bool>> _predicateCityForCp = l => true;

        //Variables
        public EmployeeDetailModel IsClickedemployeeDetailModel;
        public CityDetailModel IsClickedCityDetailModel;
        public VehicleDetailModel IsClickedVehicleDetailModel;

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
            var emps = await _employeeFacade.GetAsync();
            EmployeeDetailModels = new ObservableCollection<EmployeeDetailModel>(emps);
            var cps = await _cpFacade.GetAsync();
            CpDetailModels = new ObservableCollection<CpDetailModel>(cps);
            var cities = await _cityFacade.GetAsync();
            CityDetailModels = new ObservableCollection<CityDetailModel>(cities);
            var vehicles = await _vehicleFacade.GetAsync();
            VehicleDetailModels = new ObservableCollection<VehicleDetailModel>(vehicles);
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
        async partial void OnSearchbarCityChanged(string? value)
        {
            CityDetailModels.Clear();
            string lowerSearchTerm = SearchbarCity.Trim().ToLower();
            Expression<Func<City, bool>> predicate = lamb =>
                lamb.CityName.ToLower().Contains(lowerSearchTerm) ||
                lamb.StateName.ToLower().Contains(lowerSearchTerm);

            if (SearchbarCity != string.Empty)
            {
                var result = await _cityFacade.GetAsync(predicate);
                CityDetailModels = new ObservableCollection<CityDetailModel>(result);
            }
            else
            {
                var result = await _cityFacade.GetAsync();
                CityDetailModels = new ObservableCollection<CityDetailModel>(result);
            }
        }
        async partial void OnSearchbarVehicleChanged(string? value)
        {
            VehicleDetailModels.Clear();
            string lowerSearchTerm = SearchbarVehicle.Trim().ToLower();
            Expression<Func<Vehicle, bool>> predicate = lamb => lamb.VehicleName.ToLower().Contains(lowerSearchTerm);

            if (SearchbarEmployee != string.Empty)
            {
                var result = await _vehicleFacade.GetAsync(predicate);
                VehicleDetailModels = new ObservableCollection<VehicleDetailModel>(result);
            }
            else
            {
                var result = await _vehicleFacade.GetAsync();
                VehicleDetailModels = new ObservableCollection<VehicleDetailModel>(result);
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
            Cppredicate = Combine(_predicateCityForCp, Expression.AndAlso, Cppredicate);

            CpDetailModels.Clear();
            var cps = await _cpFacade.GetAsync(filter: Cppredicate);
            CpDetailModels = new ObservableCollection<CpDetailModel>(cps);
        }

        //MainPageView aux methods
        public async Task<bool> SaveEmployeeAsync(EmployeeDetailModel model)
        {
            var result = await _employeeFacade.SaveAsync(model);
            if (result.Id == String.Empty)
                return false;
            return true;
        }
        public async Task<bool> SaveCityAsync(CityDetailModel model)
        {
            var result = await _cityFacade.SaveAsync(model);
            if (result.Id <= 0)
                return false;
            return true;
        }
        public async Task<bool> SaveVehicleAsync(VehicleDetailModel model)
        {
            var result = await _vehicleFacade.SaveAsync(model);
            if (result.Id <= 0)
                return false;
            return true;
        }

        public async Task RoveEmployeeAsync(EmployeeDetailModel model)
        {
            await _employeeFacade.DeleteAsync(model.Id);
        }
        public async Task RoveCityAsync(CityDetailModel model)
        {
            await _cityFacade.DeleteAsync(model.Id);
        }
        public async Task RoveVehicleAsync(VehicleDetailModel model)
        {
            await _vehicleFacade.DeleteAsync(model.Id);
        }

        //Collection changed
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
        private void CityDetailModels_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            //Added Items
            if (e.NewItems != null)
            {
                foreach (CityDetailModel newEmployee in e.NewItems)
                    newEmployee.PropertyChanged += Employee_PropertyChanged;
            }
            //Removed Items
            if (e.OldItems != null)
            {
                foreach (CityDetailModel oldEmployee in e.OldItems)
                    oldEmployee.PropertyChanged -= Employee_PropertyChanged;
            }
        }
        private void VehicleDetailModels_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            //Added Items
            if (e.NewItems != null)
            {
                foreach (VehicleDetailModel newEmployee in e.NewItems)
                    newEmployee.PropertyChanged += Employee_PropertyChanged;
            }
            //Removed Items
            if (e.OldItems != null)
            {
                foreach (VehicleDetailModel oldEmployee in e.OldItems)
                    oldEmployee.PropertyChanged -= Employee_PropertyChanged;
            }
        }

        //Property changed
        private void Employee_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(EmployeeDetailModel.ISelectedFromEmployeeFilter))
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

                LoadDataCpQuery();
            }
        }
        private void City_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(CityDetailModel.ISelectedFromFilter))
            {
                int cntOfSelectedItems = 0;
                _predicateCityForCp = l => false;
                foreach (var item in CityDetailModels)
                {
                    if (item.ISelectedFromFilter)
                    {
                        cntOfSelectedItems++;
                        Expression<Func<Cp, bool>> filter = lamb => lamb.IdStartCity == item.Id || lamb.IdEndCity == item.Id;
                        _predicateCityForCp = Combine(_predicateCityForCp, Expression.OrElse, filter);
                    }
                }
                if (cntOfSelectedItems == 0)
                    _predicateCityForCp = l => true;

                LoadDataCpQuery();
            }
        }
        private void Vehicle_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(VehicleDetailModel.ISelectedFromFilter))
            {
                int cntOfSelectedItems = 0;
                _predicateVehicleForCp = l => false;
                foreach (var item in VehicleDetailModels)
                {
                    if (item.ISelectedFromFilter)
                    {
                        cntOfSelectedItems++;
                        Expression<Func<Cp, bool>> filter = lamb => lamb.Transports == item.TransportList;
                        _predicateVehicleForCp = Combine(_predicateVehicleForCp, Expression.OrElse, filter);
                    }
                }
                if (cntOfSelectedItems == 0)
                    _predicateVehicleForCp = l => true;

                LoadDataCpQuery();
            }
        }


        //Popup
        [RelayCommand]
        public void BtnClickedEmployeeFromFilter(EmployeeDetailModel item)
        {
            IsClickedemployeeDetailModel = item;
        }
        [RelayCommand]
        public void BtnClickedCityFromFilter(CityDetailModel item)
        {
            IsClickedCityDetailModel = item;
        }
        [RelayCommand]
        public void BtnClickedVehicleFromFilter(VehicleDetailModel item)
        {
            IsClickedVehicleDetailModel = item;
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