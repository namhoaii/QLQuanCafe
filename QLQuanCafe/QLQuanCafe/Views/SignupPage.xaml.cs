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
	public partial class SignupPage : ContentPage
	{
		public SignupPage ()
		{
			InitializeComponent ();

            txtUsername.Completed += TxtUsername_Completed;
            txtEmail.Completed += TxtEmail_Completed;
            txtPassword.Completed += TxtPassword_Completed;

            BindingContext = new SignupViewModel(this);

            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += (s, e) =>
            {
                chkShowPassword_Signup.IsChecked = !chkShowPassword_Signup.IsChecked;
            };
            lblShowPassword_Signup.GestureRecognizers.Add(tapGestureRecognizer);
        }

        #region Events Press enter to continue

        private void TxtPassword_Completed(object sender, EventArgs e)
        {
            txtRepassword.Focus();
        }

        private void TxtEmail_Completed(object sender, EventArgs e)
        {
            txtPassword.Focus();
        }

        private void TxtUsername_Completed(object sender, EventArgs e)
        {
            txtEmail.Focus();
        }

        #endregion


        private void chkShowPassword_Signup_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            bool isPass = chkShowPassword_Signup.IsChecked;
            txtRepassword.IsPassword = txtPassword.IsPassword = !isPass;
        }

        #region Events Focus

        private void txtUsername_Focused(object sender, FocusEventArgs e)
        {
            (BindingContext as SignupViewModel).UsernameError = string.Empty;
        }

        private void txtPassword_Focused(object sender, FocusEventArgs e)
        {
            (BindingContext as SignupViewModel).PasswordError = string.Empty;
        }

        private void txtRepassword_Focused(object sender, FocusEventArgs e)
        {
            (BindingContext as SignupViewModel).RePasswordError = string.Empty;
        }

        private void txtEmail_Focused(object sender, FocusEventArgs e)
        {
            (BindingContext as SignupViewModel).EmailError = string.Empty;
        }

        #endregion
    }
}