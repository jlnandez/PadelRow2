using Padel_Row.Model;
using Padel_Row.ViewModel;

namespace Padel_Row.Views;

public partial class AddPlayerView : ContentPage
{
	public AddPlayerView()
	{
		InitializeComponent();
        this.BindingContext = new AddUpdatePlayerViewModel();
    }

    public AddPlayerView(PlayerModel player)
    {
        InitializeComponent();
        this.BindingContext = new AddUpdatePlayerViewModel(player);
    }


}