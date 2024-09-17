using Padel_Row.Views;

namespace Padel_Row
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            // Aquí registras las rutas
            Routing.RegisterRoute(nameof(PlayerPage), typeof(PlayerPage));
            Routing.RegisterRoute(nameof(ScorePage), typeof(ScorePage));

            // Redirigir a login si el usuario no está autenticado
            if (!IsUserLoggedIn())
            {
                GoToLoginPage();
            }

            // Cuando el usuario haya iniciado sesión
            //Application.Current.MainPage = new AppShell();
        }

        private bool IsUserLoggedIn()
        {
            // Verifica si el usuario está autenticado (por ejemplo, token guardado)
            return false;
        }

        private async void GoToLoginPage()
        {
            // Redirige al login si no está autenticado
            //await Shell.Current.GoToAsync("LoginPage");
        }
    }
    
}
