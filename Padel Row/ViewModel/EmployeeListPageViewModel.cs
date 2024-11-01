using Padel_Row.Model;
using Padel_Row.Services.Interfaces;
using Padel_Row.Views;
using System.Collections.ObjectModel;
using System.Windows.Input;


namespace Padel_Row.ViewModel
{
    public class EmployeeListPageViewModel : BaseViewModel
    {
        #region Properties
        private bool _isRefreshing;
        public bool IsRefreshing
        {
            get => _isRefreshing;
            set => SetProperty(ref _isRefreshing, value);
        }

        private readonly IEmployeeService _employeeService;

        public ObservableCollection<EmployeeModel> Employees { get; set; } = new ObservableCollection<EmployeeModel>();
        #endregion

        #region Constructor
        public EmployeeListPageViewModel()
        {
            _employeeService = DependencyService.Resolve<IEmployeeService>();
            GetAllEmployee();
        }
        #endregion

        #region Methods
        private void GetAllEmployee()
        {
            IsBusy = true;
            Task.Run(async () =>
            {
                var employeeLIst = await _employeeService.GetAllEmployee();

                Device.BeginInvokeOnMainThread(() =>
                {

                    Employees.Clear();
                    if (employeeLIst?.Count > 0)
                    {
                        foreach (var employee in employeeLIst)
                        {
                            Employees.Add(employee);
                        }
                    }
                    IsBusy = IsRefreshing = false;
                });

            });
        }
        #endregion

        #region Commands

        public ICommand RefreshCommand => new Command(() =>
        {
            IsRefreshing = true;
            GetAllEmployee();
        });

        public ICommand SelectedEmployeeCommand => new Command<EmployeeModel>(async (employee) =>
        {
            if (employee != null)
            {
                var response = await App.Current.MainPage.DisplayActionSheet("Opciones", "Cerrar", null, "Editar Empleado", "Borrar Empleado");

                if (response == "Editar Empleado")
                {
                    await App.Current.MainPage.Navigation.PushAsync(new AddUpdateEmployee(employee));
                }
                else if (response == "Borrar Empleado")
                {
                    IsBusy = true;
                    bool deleteResponse = await _employeeService.DeleteEmployee(employee.Key);
                    if (deleteResponse)
                    {
                        GetAllEmployee();
                    }
                }
            }
        });
        #endregion
    }
}
