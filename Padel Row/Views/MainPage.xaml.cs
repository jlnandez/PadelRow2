using Padel_Row.ViewModel;

namespace Padel_Row.Views;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
        BindingContext = new MainViewModel();
    }

    //Método (Code-Behind)
    private void OnButtonClicked(object sender, EventArgs e)
    {
        // Código a ejecutar al presionar el botón
        DisplayAlert("Botón presionado", "¡El botón ha sido presionado!", "OK");
    }


}