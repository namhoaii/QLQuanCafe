using Acr.UserDialogs;
using QLQuanCafe.Data;
using QLQuanCafe.Helpers;
using QLQuanCafe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QLQuanCafe.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BillPage : ContentPage
    {
        int IDBan;

        List<ThongKeSL> ThongKeSL;

        public BillPage(int iDBan, List<ThongKeSL> thongKeSL)
        {
            InitializeComponent();
            IDBan = iDBan;
            ThongKeSL = thongKeSL;

            entTienKhachDua.TextChanged += EntTienKhachDua_TextChanged;
        }

        private void EntTienKhachDua_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(entTienKhachDua.Text))
                return;
            entTienKhachDua.Text = Convert.ToInt32(entTienKhachDua.Text.Replace(",", "").Replace(".", ""))
                                        .ToString("#,###");
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            int tongTien = 0;
            spnBan.Text = IDBan.ToString();
            spnTime.Text = DateTime.Now.ToString("dd/MM/yyyy");
            try
            {
                spnName.Text = await SecureStorage.GetAsync(Globals.KeyUsername);
            }
            catch (Exception) { }

            foreach (ThongKeSL item in ThongKeSL)
            {
                if (item.SoLuong == 0)
                    continue;
                AddDrinks(stkDSMon, item.TenSanPham, item.SoLuong, item.ThanhTien);
                tongTien += item.ThanhTien;
            }

            spnTongTien.Text = tongTien.ToString("#,###");
        }

        void AddDrinks(StackLayout mainStack, string name, int soLuong, int thanhToan)
        {
            StackLayout stackLayout = new StackLayout();

            Label labelCafe = new Label
            {
                Text = name,
                FontSize = 18,
                FontAttributes = FontAttributes.Bold,
                TextColor = Color.Black
            };

            stackLayout.Children.Add(labelCafe);

            Grid grid = new Grid();

            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

            Label labelSL = new Label
            {
                Text = $"SL: {soLuong}",
                FontSize = 16,
                FontAttributes = FontAttributes.Italic
            };

            Grid.SetRow(labelSL, 0);
            Grid.SetColumn(labelSL, 0);
            grid.Children.Add(labelSL);

            Label labelThanhTien = new Label
            {
                Text = $"Thành Tiền: {thanhToan.ToString("#,###")}",
                FontSize = 16,
                FontAttributes = FontAttributes.Bold
            };

            Grid.SetRow(labelThanhTien, 0);
            Grid.SetColumn(labelThanhTien, 1);
            grid.Children.Add(labelThanhTien);

            stackLayout.Children.Add(grid);

            mainStack.Children.Add(stackLayout);
        }

        private async void entTienKhachDua_Unfocused(object sender, FocusEventArgs e)
        {
            if (string.IsNullOrEmpty(entTienKhachDua.Text))
            {
                spnTienTralai.Text = string.Empty;
                return;
            }

            int tienKhachDua = Convert.ToInt32(entTienKhachDua.Text.Replace(",", "").Replace(".", ""));

            int tongTien = Convert.ToInt32(spnTongTien.Text.Replace(",", "").Replace(".", ""));

            if (tienKhachDua < tongTien)
            {
                await DisplayAlert("Thông báo", "Vui lòng nhập số tiền lớn hơn số tiền cần thanh toán.", "OK");
                entTienKhachDua.Focus();
                return;
            }

            //UserDialogs.Instance.

            spnTienTralai.Text = (tienKhachDua - tongTien).ToString("#,###");

        }

        private async void btnThanhToanTien_Clicked(object sender, EventArgs e)
        {
            bool result = await UserDialogs.Instance.ConfirmAsync(new ConfirmConfig
            {
                Title = "Xác nhận",
                Message = "Xác nhận Thanh toán?",
                OkText = "Đồng Ý",
                CancelText = "Thoát"
            });

            if (!result)
                return;

            string username = await SecureStorage.GetAsync(Globals.KeyUsername);
            NguoiDung nguoiDung = await Database.NguoiDungDatabase.GetNguoiDungAsync(username);

            HoaDon hoaDon = new HoaDon()
            {
                IDBan = IDBan,
                MaNV = nguoiDung.IDNguoiDung,
                NgayLap = DateTime.Now.ToString("dd/MM/yyyy")
            };

            await Database.HoaDonDatabase.SaveHoaDonAsync(hoaDon);
            int idHoaDon = await Database.HoaDonDatabase.GetMaxHoaDonIdAsync();

            foreach (ThongKeSL item in ThongKeSL)
            {
                if (item.SoLuong > 0)
                {
                    ChiTietHD chiTiet = new ChiTietHD()
                    {
                        IDHoaDon = idHoaDon,
                        IDSanPham = item.IDSanPham,
                        SoLuong = item.SoLuong,
                        ThanhTien = (item.SoLuong * item.DonGia)
                    };
                    await Database.ChiTietHDDatabase.SaveChiTietHDAsync(chiTiet);
                }
            }

            await DisplayAlert("Thông báo", "Thanh toán thành công!", "OK");
            await Shell.Current.Navigation.PushAsync(new MainPage());

        }

        private async void btnHuyDon_Clicked(object sender, EventArgs e)
        {
            bool result = await UserDialogs.Instance.ConfirmAsync(new ConfirmConfig
            {
                Title = "Xác nhận",
                Message = "Bạn có chắc chắn muốn HỦY đơn?",
                OkText = "Đồng Ý",
                CancelText = "Thoát"
            });

            if (result)
            {
                await Shell.Current.Navigation.PushAsync(new MainPage());
            }
            else
            {
                return;
            }
        }
    }
}