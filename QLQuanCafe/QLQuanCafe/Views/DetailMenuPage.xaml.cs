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
            int slCafe = Convert.ToInt32(lblCafe.Text.Replace("SL: ", ""));
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

            if (soTien == 0)
            {
                await DisplayAlert("Thông báo", "Vui lòng chọn ít nhất 1 món", "OK");
                return;
            }

            List<ThongKeSL> thongKeSL = await Database.ThongKeSLDatabase.GetSP();

            foreach (ThongKeSL item in thongKeSL)
            {
                switch (item.TenSanPham)
                {
                    case "Cafe":
                        item.SoLuong = slCafe;
                        item.ThanhTien = slCafe * item.DonGia;
                        break;
                    case "Đá me":
                        item.SoLuong = slDaMe;
                        item.ThanhTien = slDaMe * item.DonGia;
                        break;
                    case "Rau má":
                        item.SoLuong = slRauMa;
                        item.ThanhTien = slRauMa * item.DonGia;
                        break;
                    case "Trà đào":
                        item.SoLuong = slTraDao;
                        item.ThanhTien = slTraDao * item.DonGia;
                        break;
                    case "Trà sữa":
                        item.SoLuong = slTraSua;
                        item.ThanhTien = slTraSua * item.DonGia;
                        break;
                    case "Nước ngọt":
                        item.SoLuong = slNuocNgot;
                        item.ThanhTien = slNuocNgot * item.DonGia;
                        break;
                    default:
                        break;

                }
            }

            await Shell.Current.Navigation.PushAsync(new BillPage(IDBan, thongKeSL));
        }
    }
}