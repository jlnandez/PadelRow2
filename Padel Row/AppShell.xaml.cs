using Padel_Row.Views;

namespace Padel_Row
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            // Aquí registras las rutas
            Routing.RegisterRoute(nameof(SignInView), typeof(SignInView));
            Routing.RegisterRoute(nameof(SignUpView), typeof(SignUpView));

            Routing.RegisterRoute(nameof(PlayerPage), typeof(PlayerPage));
            Routing.RegisterRoute(nameof(ScorePage), typeof(ScorePage));

        }

    }
    
}
