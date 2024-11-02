namespace Padel_Row.Views;

public partial class PlayerPage : ContentPage
{
	public PlayerPage()
	{
		InitializeComponent();
	}

    private void NavigateAddPlayer(object sender, EventArgs e)
    {
        Navigation.PushAsync(new AddPlayerView());
    }
}