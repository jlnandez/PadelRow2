using Padel_Row.ViewModel;

namespace Padel_Row.Views;

public partial class SignUpView : ContentPage
{
	public SignUpView(SignUpViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
	}
}