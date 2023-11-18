using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace QLQuanCafe.ViewModels
{
    public class SignupViewModel : BaseViewModel
    {
        public Command PrevPage { get; set; }

        public SignupViewModel()
        {
            PrevPage = new Command(OnPrevPage);
        }

        private async void OnPrevPage(object obj)
        {
            await Shell.Current.Navigation.PopAsync();
        }
    }
}
