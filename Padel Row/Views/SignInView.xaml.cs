using Padel_Row.ViewModel;

namespace Padel_Row.Views;

public partial class SignInView : ContentPage
{
    private readonly SignInViewModel _viewModel;

    public SignInView(SignInViewModel viewModel)
	{
		InitializeComponent();

        _viewModel = viewModel;

        BindingContext = viewModel;

        // Suscribirse al evento de inicio de sesi�n exitoso
        _viewModel.SignInSuccess += OnSignInSuccess;
    }

    private async void OnSignInSuccess()
    {
        await DisplayAlert("Inicio de Sesi�n", "�Sesi�n iniciada exitosamente!", "OK");
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        // Cancelar la suscripci�n para evitar fugas de memoria
        _viewModel.SignInSuccess -= OnSignInSuccess;
    }
}