using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace QLQuanCafe.ViewModels
{
    public class ForgotPasswordViewModel : BaseViewModel
    {
        public Command PrevPage { get; set; }

        public ForgotPasswordViewModel()
        {
            PrevPage = new Command(OnPrevPage);
        }

        private async void OnPrevPage(object obj)
        {
            await Shell.Current.Navigation.PopAsync();
        }
    }
}
