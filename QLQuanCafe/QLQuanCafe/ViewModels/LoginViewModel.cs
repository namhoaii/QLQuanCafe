using QLQuanCafe.Models;
using QLQuanCafe.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace QLQuanCafe.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private User _user;

        private string _username;
        private string _password;
        private string _passwordError;
        private string _usernameError;

        public Command OpenPageForgotPass { get; }
        public Command OpenPageSignup { get; }
        public Command OpenMainPage { get; }

        public LoginViewModel()
        {
            OpenPageForgotPass = new Command(OnForgotPassword);
            OpenPageSignup = new Command(async () => await OnSigUp());
            OpenMainPage = new Command(OnMainPage);
        }

        private void OnMainPage(object obj)
        {
            if (ValidateLogin())
            {
                //tiến hành kiểm tra vào main page
            }
            else
            {
                return;
            }
            
        }

        private bool ValidateLogin()
        {
            bool isVal = true;
            if(string.IsNullOrEmpty(Username))
            {
                UsernameError = "Không được bỏ trống";
                isVal = false;
            }
            if(string.IsNullOrEmpty(Password))
            {
                PasswordError = "Không được bỏ trống";
                isVal = false;
            }

            return isVal;
        }

        public string Username
        {
            get => _username;
            set => SetProperty(ref _username, value);
        }
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        public string UsernameError
        {
            get => _usernameError;
            set => SetProperty(ref _usernameError, value);
        }        
        public string PasswordError
        {
            get => _passwordError;
            set => SetProperty(ref _passwordError, value);
        }

        private async Task OnSigUp()
        {
            await Shell.Current.Navigation.PushAsync(new SignupPage());
        }

        private async void OnForgotPassword(object obj)
        {
            await Shell.Current.Navigation.PushAsync(new ForgotPasswordPage());
        }
    }
}
