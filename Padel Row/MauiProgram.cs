using Firebase.Auth;
using Firebase.Auth.Providers;
using Microsoft.Extensions.Logging;
using Padel_Row.Views;
using Padel_Row.ViewModel;
using Firebase.Auth.Repository;

namespace Padel_Row
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            builder.Services.AddSingleton(new FirebaseAuthClient(new FirebaseAuthConfig()
            {
                ApiKey = "AIzaSyBrYV44HwkT-F2Lj-_2atRTU36vYYhxctw",
                AuthDomain = "padel-row-e6028.firebaseapp.com",
                Providers = new FirebaseAuthProvider[]
                {
                    new EmailProvider()
                },
                UserRepository = new FileUserRepository("PadelRow")

            })); ;


            // Registrar UserRepository o cualquier otro servicio que necesites
            //builder.Services.AddSingleton<UserRepository>();

            builder.Services.AddSingleton<SignInView>();
            builder.Services.AddSingleton<SignInViewModel>();
            builder.Services.AddSingleton<SignUpView>();
            builder.Services.AddSingleton<SignUpViewModel>();

            return builder.Build();
        }
    }
}
