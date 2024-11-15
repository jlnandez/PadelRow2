using Firebase.Auth;
using Firebase.Auth.Providers;
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
            //DependencyService.Register<IPlayerService, PlayerService>();
            DependencyService.Register<ITournamentService, TournamentService>();
        }

        private async void CheckUserAuthentication()
        {
            var token = await SecureStorage.Default.GetAsync("auth_token");

            var uuu = await SecureStorage.Default.GetAsync("uuu");

            var ppp = await SecureStorage.Default.GetAsync("ppp");

            if (!string.IsNullOrEmpty(token))
            {
                try
                {
                    // Intenta autenticar usando el token guardado
                    //await _authClient.SignInWithCredentialAsync(credential);

                    await _authClient.SignInWithEmailAndPasswordAsync(uuu, ppp);


                    await Shell.Current.GoToAsync("//MainPage");
                }
                catch (Exception ex)
                {

                    await Application.Current.MainPage.DisplayAlert("Error","Problemas con inicio de session intente de nuevo ", "OK");
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

            SecureStorage.Default.Remove("uuu");

            SecureStorage.Default.Remove("ppp");

            // Navegar a la página de inicio de sesión
            await Shell.Current.GoToAsync("//SignIn");
        }
    }
}
