using Padel_Row.ViewModel;

namespace Padel_Row.Views;

public partial class TorneoPage : ContentPage
{
	private TournamentViewModel _viewModel;

    public TorneoPage()
	{
		InitializeComponent();
        _viewModel = new TournamentViewModel();
        this.BindingContext = _viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        // Llama al método para actualizar la lista al regresar a la pantalla
        _viewModel.GetAllTorneos();
    }


    private void GoToAddTorneo(object sender, EventArgs e)
    {
        Navigation.PushAsync(new TorneoAddPage());
    }
}