using QLQuanCafe.Data;
using QLQuanCafe.Helpers;
using QLQuanCafe.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QLQuanCafe.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailMenuPage : ContentPage
    {
        int IDBan;

        public DetailMenuPage(int iDBan)
        {
            InitializeComponent();
            IDBan = iDBan;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            lblTenBan.Text = $"Bàn số {IDBan}";
            try
            {
                string username = await SecureStorage.GetAsync(Globals.KeyUsername);
                Title = username;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        void TongTien()
        {
            int slCafe = Convert.ToInt32(lblCafe.Text.Replace("SL: ",""));
            int slDaMe = Convert.ToInt32(lblDaMe.Text.Replace("SL: ", ""));
            int slRauMa = Convert.ToInt32(lblRauMa.Text.Replace("SL: ", ""));
            int slTraDao = Convert.ToInt32(lblTraDao.Text.Replace("SL: ", ""));
            int slTraSua = Convert.ToInt32(lblTraSua.Text.Replace("SL: ", ""));
            int slNuocNgot = Convert.ToInt32(lblNuocNgot.Text.Replace("SL: ", ""));

            int soTien = slCafe * 10000 + slDaMe * 12000 + slRauMa * 15000 + slTraSua * 20000 + slTraDao * 20000 + slNuocNgot * 10000;

            lblTongTien.Text = "Tổng tiền: " + soTien.ToString("#,###");
        }

        private void sppCafe_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            lblCafe.Text = $"SL: {(sender as Stepper).Value}";
            TongTien();
        }

        private void sppDame_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            lblDaMe.Text = $"SL: {(sender as Stepper).Value}";
            TongTien();
        }

        private void sppRauMa_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            lblRauMa.Text = $"SL: {(sender as Stepper).Value}";
            TongTien();
        }

        private void sppTraDao_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            lblTraDao.Text = $"SL: {(sender as Stepper).Value}";
            TongTien();
        }

        private void sppTraSua_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            lblTraSua.Text = $"SL: {(sender as Stepper).Value}";
            TongTien();
        }

        private void sppNuocNgot_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            lblNuocNgot.Text = $"SL: {(sender as Stepper).Value}";
            TongTien();
        }

        private async void btnHoanThanh_Clicked(object sender, EventArgs e)
        {
            int slCafe = Convert.ToInt32(lblCafe.Text.Replace("SL: ", ""));
            int slDaMe = Convert.ToInt32(lblDaMe.Text.Replace("SL: ", ""));
            int slRauMa = Convert.ToInt32(lblRauMa.Text.Replace("SL: ", ""));
            int slTraDao = Convert.ToInt32(lblTraDao.Text.Replace("SL: ", ""));
            int slTraSua = Convert.ToInt32(lblTraSua.Text.Replace("SL: ", ""));
            int slNuocNgot = Convert.ToInt32(lblNuocNgot.Text.Replace("SL: ", ""));

            int soTien = slCafe * 10000 + slDaMe * 12000 + slRauMa * 15000 + slTraSua * 20000 + slTraDao * 20000 + slNuocNgot * 10000;

            if(soTien == 0)
            {
                await DisplayAlert("Thông báo", "Vui lòng chọn ít nhất 1 món", "OK");
                return;
            }

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

            if(slCafe > 0)
            {
                ChiTietHD chiTiet = new ChiTietHD()
                {
                    IDHoaDon = idHoaDon,
                    IDSanPham = 1,
                    SoLuong = slCafe,
                    ThanhTien = slCafe * 10000
                };
                await Database.ChiTietHDDatabase.SaveChiTietHDAsync(chiTiet);
            }
            if (slDaMe > 0)
            {
                ChiTietHD chiTiet = new ChiTietHD()
                {
                    IDHoaDon = idHoaDon,
                    IDSanPham = 2,
                    SoLuong = slDaMe,
                    ThanhTien = slDaMe * 12000
                };
                await Database.ChiTietHDDatabase.SaveChiTietHDAsync(chiTiet);
            }
            if (slRauMa > 0)
            {
                ChiTietHD chiTiet = new ChiTietHD()
                {
                    IDHoaDon = idHoaDon,
                    IDSanPham = 3,
                    SoLuong = slRauMa,
                    ThanhTien = slRauMa * 15000
                };
                await Database.ChiTietHDDatabase.SaveChiTietHDAsync(chiTiet);
            }
            if (slTraDao > 0)
            {
                ChiTietHD chiTiet = new ChiTietHD()
                {
                    IDHoaDon = idHoaDon,
                    IDSanPham = 4,
                    SoLuong = slTraDao,
                    ThanhTien = slTraDao * 20000
                };
                await Database.ChiTietHDDatabase.SaveChiTietHDAsync(chiTiet);
            }
            if (slTraSua > 0)
            {
                ChiTietHD chiTiet = new ChiTietHD()
                {
                    IDHoaDon = idHoaDon,
                    IDSanPham = 5,
                    SoLuong = slTraSua,
                    ThanhTien = slTraSua * 20000
                };
                await Database.ChiTietHDDatabase.SaveChiTietHDAsync(chiTiet);
            }
            if (slNuocNgot > 0)
            {
                ChiTietHD chiTiet = new ChiTietHD()
                {
                    IDHoaDon = idHoaDon,
                    IDSanPham = 6,
                    SoLuong = slNuocNgot,
                    ThanhTien = slNuocNgot * 10000
                };
                await Database.ChiTietHDDatabase.SaveChiTietHDAsync(chiTiet);
            }

            await DisplayAlert("Thông báo", "Thanh toán thành công!", "OK");
            await Shell.Current.Navigation.PopAsync();
        }
    }
}