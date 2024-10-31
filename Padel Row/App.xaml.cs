using Firebase.Auth;
using Padel_Row.Services.Implementations;
using Padel_Row.Services.Interfaces;
using Padel_Row.Views;

namespace Padel_Row
{
    public partial class App : Application
    {
        private readonly FirebaseAuthClient _authClient;

        public App(FirebaseAuthClient authClient)
        {
            InitializeComponent();

            _authClient = authClient;

            MainPage = new AppShell();

            CheckUserAuthentication();

            //Registro dependencias
            DependencyService.Register<IEmployeeService,EmployeeService>();
        }

        private async void CheckUserAuthentication()
        {
            var token = await SecureStorage.Default.GetAsync("auth_token");

            if (!string.IsNullOrEmpty(token))
            {
                try
                {
                    // Usar el token para restablecer la sesión
                    //await _authClient.SignInWithCustomTokenAsync(token);
                    await Shell.Current.GoToAsync("//MainPage");
                }
                catch
                {
                    // Si el token es inválido, redirige a la página de inicio de sesión
                    await Shell.Current.GoToAsync("//SignIn");

                    

                }
            }
            else
            {
                // No hay token, redirige a la página de inicio de sesión
                await Shell.Current.GoToAsync("//SignIn");
            }
        }

        public async Task SignOut()
        {
            // Eliminar el token de autenticación
            SecureStorage.Default.Remove("auth_token");

            // Navegar a la página de inicio de sesión
            await Shell.Current.GoToAsync("//SignIn");
        }
    }
}
