using Padel_Row.ViewModel;

namespace Padel_Row.Views;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
        BindingContext = new MainViewModel();
    }

    //M�todo (Code-Behind)
    private void OnButtonClicked(object sender, EventArgs e)
    {
        // C�digo a ejecutar al presionar el bot�n
        DisplayAlert("Bot�n presionado", "�El bot�n ha sido presionado!", "OK");
    }


}