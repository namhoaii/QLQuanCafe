using Acr.UserDialogs;
using QLQuanCafe.Data;
using QLQuanCafe.Helpers;
using QLQuanCafe.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
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
            UserDialogs.Instance.ShowLoading("Xin chờ...");
            await Task.Delay(500);
            if (await isValidate())
            {
                //Xử lý gữi password về gmail;
                NguoiDung nguoiDung = await Database.NguoiDungDatabase.GetNguoiDungEmailAsync(Email);
                string passRandom = Globals.RandomPass();
                nguoiDung.MatKhau = BCrypt.Net.BCrypt.HashPassword(passRandom);

                _ = await Database.NguoiDungDatabase.SaveNguoiDungAsync(nguoiDung);

                string bodyMail = Globals.BodyEmail(Email, nguoiDung.TenNguoiDung, passRandom);

                bool isResult = Globals.SendEmail(Globals.Subject, bodyMail, Email);

                if(!isResult)
                {
                    await page.DisplayAlert("Thông báo", "Thất bại!\n\nCó lỗi xảy ra vui lòng thử lại sau vài phút!", "OK");
                    UserDialogs.Instance.HideLoading();
                    return;
                }

                UserDialogs.Instance.HideLoading();
                await page.DisplayAlert("Thông báo", "Thành công!\n\nMật khẩu mới đã được gửi qua email của bạn!", "OK");
                await Shell.Current.Navigation.PopAsync();
                return;
            }
            UserDialogs.Instance.HideLoading();
        }

        private async Task<bool> isValidate()
        {
            if(string.IsNullOrEmpty(_email))
            {
                EmailError = "Không được bỏ trống";
                return false;
            }

            string pattern = @"^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$";
            if(!Regex.IsMatch(_email, pattern))
            {
                EmailError = "Email không hợp lệ";
                return false;
            }

            NguoiDung emailNguoiDung = await Database.NguoiDungDatabase.GetNguoiDungEmailAsync(Email);
            //Kiểm tra trùng email
            if (emailNguoiDung == null)
            {
                EmailError = "Email không tồn tại";
                return false;
            }

            return true;
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
