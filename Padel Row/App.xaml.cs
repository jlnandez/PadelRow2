namespace Padel_Row
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();


            if (IsUserLoggedIn())
            {

                MainPage = new AppShell();
            }
            else
            {
                MainPage = new NavigationPage(new LoginPage());

                //MainPage = new NavigationPage(new RegisterPage());
            }

        }

        private bool IsUserLoggedIn()
        {
            return true;
        }



    }
}
