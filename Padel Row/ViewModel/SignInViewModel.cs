using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Firebase.Auth;
using Firebase.Auth.Providers;
using System.Net;

namespace Padel_Row.ViewModel
{
    public partial class SignInViewModel : ObservableObject
    {
        private readonly FirebaseAuthClient _authClient;

        [ObservableProperty]
        private string _email;

        [ObservableProperty]
        private string _password;

        public event Action? SignInSuccess;
        public string? Username => _authClient.User?.Info?.DisplayName;

        public SignInViewModel(FirebaseAuthClient authClient)
        {
            _authClient = authClient;
        }

        [RelayCommand]
        private async Task SignIn()
        {
            try
            {
                await _authClient.SignInWithEmailAndPasswordAsync(Email, Password);

                // Guardar el IdToken y el UserId
                await SecureStorage.Default.SetAsync("auth_token", _authClient.User.Credential.IdToken);
                await SecureStorage.Default.SetAsync("user_id", _authClient.User.Uid); // Guarda el UserId en SecureStorage



                // Guardar email
                await SecureStorage.Default.SetAsync("uuu", Email);

                // Guardar pass
                await SecureStorage.Default.SetAsync("ppp", Password);

                // Redirigir a MainPage y quitar SignIn de la pila de navegación
                await Shell.Current.GoToAsync("//MainPage");


                // Si la autenticación fue exitosa, dispara el evento
                //SignInSuccess?.Invoke();

            }
            catch (Exception ex)
            {
                // Aquí puedes manejar errores de autenticación
                await Application.Current.MainPage.DisplayAlert("Error", "Inicio de sesión fallido. Verifica tus credenciales.", "OK");
            }
        }

        [RelayCommand]
        private async Task NavigateSignUp()
        {
            await Shell.Current.GoToAsync("//SignUp");

        }
        
    }
}
