using Padel_Row.ViewModel;

namespace Padel_Row.Views;

public partial class TorneoPage : ContentPage
{
	public TorneoPage()
	{
		InitializeComponent();
		this.BindingContext = new TournamentViewModel();
		
	}


    private void GoToAddTorneo(object sender, EventArgs e)
    {
        Navigation.PushAsync(new TorneoAddPage());
    }
}