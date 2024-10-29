using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Firebase.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Padel_Row.ViewModel
{
    public partial class SignUpViewModel : ObservableObject
    {
        private readonly FirebaseAuthClient _authClient;

        public SignUpViewModel(FirebaseAuthClient authClient)
        {
            _authClient = authClient;
        }


        [ObservableProperty]
        private string _email;

        [ObservableProperty]
        private string _username;

        [ObservableProperty]
        private string _password;

        [RelayCommand]
        private async Task SignUp()
        {
            await _authClient.CreateUserWithEmailAndPasswordAsync(Email, Password, Username);
        }

        [RelayCommand]
        private async Task NavigateSignIn()
        {
            await Shell.Current.GoToAsync("//SignIn");

        }

    }
}
