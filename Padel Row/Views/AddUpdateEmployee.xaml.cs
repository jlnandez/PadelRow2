using Padel_Row.Model;
using Padel_Row.ViewModel;


namespace Padel_Row.Views;

public partial class AddUpdateEmployee : ContentPage
{
	public AddUpdateEmployee()
	{
		InitializeComponent();
        this.BindingContext = new AddUpdateEmployeePageViewModel();
    }

    public AddUpdateEmployee(EmployeeModel employee)
    {
        InitializeComponent();
        this.BindingContext = new AddUpdateEmployeePageViewModel(employee);
    }
}