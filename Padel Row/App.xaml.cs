namespace Padel_Row
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();


            //if (IsUserLoggedIn())
            //{
            //    MainPage = new AppShell();
            //}
            //else
            //{
            //    MainPage = new NavigationPage(new LoginPage());

            //    //MainPage = new NavigationPage(new RegisterPage());
            //}

            InitializeComponent();
            MainPage = new AppShell();

        }

        protected override async void OnStart()
        {
            var token = await SecureStorage.GetAsync("firebase_token");
            if (string.IsNullOrEmpty(token))
            {
                // Redirige a LoginPage si no hay sesión activa
                //await Shell.Current.GoToAsync("//LoginPage");

                //MainPage = new NavigationPage(new LoginPage());
            }
        }

        //private bool IsUserLoggedIn()
        //{
        //    return true;
        //}



    }
}
