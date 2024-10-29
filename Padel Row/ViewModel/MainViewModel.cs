using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Padel_Row.ViewModel
{
    public partial class MainViewModel : ObservableObject
    {

        [RelayCommand]
        private async Task SignOutAsync()
        {
            //Application.Current.MainPage.DisplayAlert("LOGICA", "LOGICA MVVM", "OK");

            // Eliminar el token de autenticación
            SecureStorage.Default.Remove("auth_token");

            // Navegar a la página de inicio de sesión
            await Shell.Current.GoToAsync("//SignIn");

        }

    }
}
