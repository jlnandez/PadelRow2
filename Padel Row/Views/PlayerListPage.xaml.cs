using Padel_Row.ViewModel;

namespace Padel_Row.Views;

public partial class PlayerListPage : ContentPage
{
	public PlayerListPage()
	{
		InitializeComponent();
        this.BindingContext = new PlayerListViewModel();
    }
}