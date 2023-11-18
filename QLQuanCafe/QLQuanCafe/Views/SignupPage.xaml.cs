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

            BindingContext = new SignupViewModel();

            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += (s, e) =>
            {
                chkShowPassword_Signup.IsChecked = !chkShowPassword_Signup.IsChecked;
            };
            lblShowPassword_Signup.GestureRecognizers.Add(tapGestureRecognizer);
        }

        private void chkShowPassword_Signup_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            bool isPass = chkShowPassword_Signup.IsChecked;
            txtRepassword.IsPassword = !isPass;
        }
    }
}