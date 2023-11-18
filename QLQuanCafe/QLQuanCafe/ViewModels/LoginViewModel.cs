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

        public Command OpenPageForgotPass { get; }
        public Command OpenPageSignup { get; }

        public LoginViewModel()
        {
            OpenPageForgotPass = new Command(OnForgotPassword);
            OpenPageSignup = new Command(async () => await OnSigUp());
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
