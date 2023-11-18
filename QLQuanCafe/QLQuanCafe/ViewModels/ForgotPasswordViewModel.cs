using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Text;
using System.Text.RegularExpressions;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace QLQuanCafe.ViewModels
{
    public class ForgotPasswordViewModel : BaseViewModel
    {
        Page page;

        private string _email;
        private string _emailError;

        public Command PrevPage { get; }
        public Command Send { get; }

        public ForgotPasswordViewModel(Page page)
        {
            this.page = page;
            PrevPage = new Command(OnPrevPage);
            Send = new Command(OnSend);
        }

        private async void OnSend(object obj)
        {
            if (isValidate())
            {
                //Xử lý gữi password về gmail
                /*DisplayAlert("Alert", "You have been alerted", "OK");*/
                await page.DisplayAlert("Thông báo", "Thành công!\n\nMật khẩu mới đã được gửi qua email của bạn!", "OK");
                await Shell.Current.Navigation.PopAsync();

            }
        }

        private bool isValidate()
        {
            bool isVal = true;

            if(string.IsNullOrEmpty(_email))
            {
                isVal = false;
                EmailError = "Không được bỏ trống";
                return isVal;
            }

            string pattern = @"^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$";
            if(!Regex.IsMatch(_email, pattern))
            {
                isVal = false;
                EmailError = "Email không hợp lệ";
                return isVal;
            }

            return isVal;
        }

        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }
        
        public string EmailError
        {
            get => _emailError;
            set => SetProperty(ref _emailError, value);
        }

        private async void OnPrevPage(object obj)
        {
            await Shell.Current.Navigation.PopAsync();
        }
    }
}
