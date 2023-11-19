using Acr.UserDialogs;
using QLQuanCafe.Data;
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
        private Page Page;

        private string _username;
        private string _password;
        private string _passwordError;
        private string _usernameError;

        public Command OpenPageForgotPass { get; }
        public Command OpenPageSignup { get; }
        public Command OpenMainPage { get; }

        public LoginViewModel(Page page)
        {
            Page = page;

            OpenPageForgotPass = new Command(OnForgotPassword);
            OpenPageSignup = new Command(OnSigUp);
            OpenMainPage = new Command(OnMainPage);
        }

        private async void OnMainPage(object obj)
        {
            UserDialogs.Instance.ShowLoading("Xin chờ...");
            if (ValidateLogin())
            {
                //tiến hành kiểm tra vào main page
                NguoiDung nguoiDung = await Database.NguoiDungDatabase.GetNguoiDungAsync(Username);
                if (nguoiDung == null)
                {
                    _ = Page.DisplayAlert("Thông báo", "Thông tin tài khoảng hoặc mật khẩu không chính xác", "OK");
                    UserDialogs.Instance.HideLoading();
                    return;
                }

                if(nguoiDung.MatKhau != Password)
                {
                    _ = Page.DisplayAlert("Thông báo", "Thông tin tài khoảng hoặc mật khẩu không chính xác", "OK");
                    UserDialogs.Instance.HideLoading();
                    return;
                }
                await Task.Delay(1000);
                UserDialogs.Instance.HideLoading();
                await Shell.Current.Navigation.PushAsync(new MainPage());
            }
            else
            {
                UserDialogs.Instance.HideLoading();
                return;
            }

        }

        private bool ValidateLogin()
        {
            bool isVal = true;
            if (string.IsNullOrEmpty(Username))
            {
                UsernameError = "Không được bỏ trống";
                isVal = false;
            }
            if (string.IsNullOrEmpty(Password))
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

        private async void OnSigUp(object obj)
        {
            await Shell.Current.Navigation.PushAsync(new SignupPage());
        }

        private async void OnForgotPassword(object obj)
        {
            await Shell.Current.Navigation.PushAsync(new ForgotPasswordPage());
        }
    }
}
