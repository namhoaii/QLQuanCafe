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
    public partial class ForgotPasswordPage : ContentPage
    {
        public ForgotPasswordPage()
        {
            InitializeComponent();

            BindingContext = new ForgotPasswordViewModel(this);
        }

        private void txtEmail_Focused(object sender, FocusEventArgs e)
        {
            (BindingContext as ForgotPasswordViewModel).EmailError = string.Empty;
        }
    }
}