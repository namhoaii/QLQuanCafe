using QLQuanCafe.Data;
using QLQuanCafe.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace QLQuanCafe.ViewModels
{
    public class SignupViewModel : BaseViewModel
    {
        Page Page;

        private string _username;
        private string _email;
        private string _password;
        private string _rePassword;
        private string _passwordError;
        private string _usernameError;
        private string _emailError;
        private string _rePasswordError;

        public Command PrevPage { get; }
        public Command CreateAccount { get; }


        public SignupViewModel(Page page)
        {
            Page = page;

            PrevPage = new Command(OnPrevPage);
            CreateAccount = new Command(OnCreateAccount);
        }

        private async void OnCreateAccount(object obj)
        {
            if (await Validate())
            {
                //Tiến hành tạo user

                NguoiDung nguoiDung = new NguoiDung();
                nguoiDung.TenNguoiDung = Username;
                nguoiDung.MatKhau = Password;
                nguoiDung.Email = Email;
                nguoiDung.Quyen = 0;

                await Database.NguoiDungDatabase.SaveNguoiDungAsync(nguoiDung);

                /*-------------*/
                await Page.DisplayAlert("Thông báo", "Thành Công!\n\nChúc mừng bạn đã tạo tài khoản thành công. Hãy tiến hành Đăng nhập", "OK");
                await Shell.Current.Navigation.PopAsync();
            }
        }

        private async Task<bool> Validate()
        {
            bool isVal = true;

            #region Kiểm tra null

            if (string.IsNullOrEmpty(_username))
            {
                isVal = false;
                UsernameError = "Không được bỏ trống";
            }
            if (string.IsNullOrEmpty(_email))
            {
                isVal = false;
                EmailError = "Không được bỏ trống";
            }
            if (string.IsNullOrEmpty(_password))
            {
                isVal = false;
                PasswordError = "Không được bỏ trống";
            }
            if (string.IsNullOrEmpty(_rePassword))
            {
                isVal = false;
                RePasswordError = "Không được bỏ trống";
            }
            //nếu trống tiến hành báo lỗi ngược lại kiểm tra điều kiện
            if (!isVal)
            {
                return isVal;
            }

            #endregion

            #region Rule

            //Kiểm tra định dạng email
            string patternEmail = @"^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$";
            if (!Regex.IsMatch(_email, patternEmail))
            {
                isVal = false;
                EmailError = "Email không hợp lệ";
            }

            //Kiểm tra pass mạnh
            string patternPass = @"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$";
            if (!Regex.IsMatch(Password, patternPass))
            {
                isVal = false;
                PasswordError = "Mật khẩu bao gồm ít nhất 8 ký tự, chữ thường, chữ in hoa, ký tự đặc biệt và số";
            }

            //Kiểm tra rePass phải trùng pass
            if (RePassword != Password)
            {
                isVal = false;
                RePasswordError = "Mật khẩu không trùng khớp";
            }

            if (!isVal)
                return isVal;
            #endregion

            #region Duplicates

            //Kiểm tra trùng username db
            NguoiDung nguoiDung = await Database.NguoiDungDatabase.GetNguoiDungAsync(Username);
            if (nguoiDung != null)
            {
                isVal = false;
                UsernameError = "Tên Người Dùng đã tồn tại";
            }
            NguoiDung emailNguoiDung = await Database.NguoiDungDatabase.GetNguoiDungEmailAsync(Email);
            //Kiểm tra trùng email
            if (emailNguoiDung != null)
            {
                isVal = false;
                EmailError = "Email đã được sử dụng vui lòng nhập email khác";
            }

            #endregion

            return isVal;
        }

        private async void OnPrevPage(object obj)
        {
            await Shell.Current.Navigation.PopAsync();
        }

        #region Props

        public string Username { get => _username; set => SetProperty(ref _username, value); }
        public string Email { get => _email; set => SetProperty(ref _email, value); }
        public string Password { get => _password; set => SetProperty(ref _password, value); }
        public string RePassword { get => _rePassword; set => SetProperty(ref _rePassword, value); }
        public string PasswordError { get => _passwordError; set => SetProperty(ref _passwordError, value); }
        public string UsernameError { get => _usernameError; set => SetProperty(ref _usernameError, value); }
        public string EmailError { get => _emailError; set => SetProperty(ref _emailError, value); }
        public string RePasswordError { get => _rePasswordError; set => SetProperty(ref _rePasswordError, value); }

        #endregion
    }
}
