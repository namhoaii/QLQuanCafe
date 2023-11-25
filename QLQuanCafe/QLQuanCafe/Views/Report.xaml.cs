using Acr.UserDialogs;
using Microcharts;
using QLQuanCafe.Data;
using QLQuanCafe.Models;
using SkiaSharp;
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
    public partial class Report : ContentPage
    {
        private ChartEntry[] entries;


        public Report()
        {
            InitializeComponent();

        }

        private async void DisplayNumbersAsync(int time, long number)
        {

            long from = 0;
            long step = number / time;
            while (from < number)
            {
                from += step;
                lblTongDoanhThu.Text = from.ToString("#,###") + " VNĐ";
                await Task.Delay(1);
            }
            string s = number.ToString("#,###") == "" ? "0" : number.ToString("#,###");
            lblTongDoanhThu.Text = s + " VNĐ";
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            List<ThongKeSL> thongKeSL = await Database.ThongKeSLDatabase.GetSLSP();

            entries = new ChartEntry[thongKeSL.Count];

            long doanhThu = 0;

            string[] colors = new string[] { "#3498db", "#77d065", "#e74c3c", "#b455b6", "#f1c40f", "#e67e22" };
            int index = 0;

            foreach (ThongKeSL item in thongKeSL)
            {
                var temp = new ChartEntry(item.SoLuong)
                {
                    Label = item.TenSanPham,
                    ValueLabel = item.SoLuong.ToString("#,###") == "" ? "0" : item.SoLuong.ToString("#,###"),
                    Color = SKColor.Parse(colors[index])
                };
                entries[index] = temp;
                index++;
                doanhThu += item.ThanhTien;
            }

            DisplayNumbersAsync(96, doanhThu);

            chartView.Chart = new BarChart
            {
                Entries = entries,
                ValueLabelOrientation = Orientation.Horizontal,
                LabelTextSize = 40
            };

        }
    }
}