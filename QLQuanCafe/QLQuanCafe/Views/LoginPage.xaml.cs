using QLQuanCafe.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QLQuanCafe.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();

            txtUsername.Completed += TxtUsername_Completed;

            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += (s, e) =>
            {
                chkShowPassword.IsChecked = !chkShowPassword.IsChecked;
            };
            lblShowPassword.GestureRecognizers.Add(tapGestureRecognizer);

            BindingContext = new LoginViewModel(this);
        }

        protected override bool OnBackButtonPressed()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                bool result = await DisplayAlert("Xác nhận", "Bạn có muốn đóng ứng dụng?", "Đồng ý", "Hủy");

                if (result)
                {
                    System.Diagnostics.Process.GetCurrentProcess().Kill();
                }
            });

            return true;
        }

        private void TxtUsername_Completed(object sender, EventArgs e)
        {
            txtPassword.Focus();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Device.StartTimer(TimeSpan.FromMilliseconds(100), () =>
            {
                txtUsername.Focus();
                return false;
            });
        }

        private void chkShowPassword_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            bool isPass = chkShowPassword.IsChecked;
            txtPassword.IsPassword = !isPass;
        }

        private void txtUsername_Focused(object sender, FocusEventArgs e)
        {
            (BindingContext as LoginViewModel).UsernameError = string.Empty;
        }

        private void txtPassword_Focused(object sender, FocusEventArgs e)
        {
            (BindingContext as LoginViewModel).PasswordError = string.Empty;
        }
    }
}