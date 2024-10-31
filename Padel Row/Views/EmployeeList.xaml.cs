using Padel_Row.ViewModel;

namespace Padel_Row.Views;

public partial class EmployeeList : ContentPage
{
	public EmployeeList()
	{
        InitializeComponent();
        this.BindingContext = new EmployeeListPageViewModel();
    }
}