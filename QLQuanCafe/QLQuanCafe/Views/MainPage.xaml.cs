using Acr.UserDialogs;
using QLQuanCafe.Data;
using QLQuanCafe.Helpers;
using QLQuanCafe.Models;
using QLQuanCafe.ViewModels;
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
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            BindingContext = new MainPageViewModel();
        }

        void AddTable(Grid grid, int columnIndex, int rowIndex, Ban ban)
        {
            //Tạo 1 label
            Label label = new Label
            {
                Text = $"Bàn số {ban.IDBan}",
                TextColor = Color.FromHex("#000011"),
                FontSize = 20,
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.CenterAndExpand

            };

            //Tạo 1 imgButton
            ImageButton imageButton = new ImageButton
            {
                Source = "cloud",
                Margin = new Thickness(20, 30, 0, 0),
                Command = new Command(() =>
                {
                    OnDetailePage(ban.IDBan);
                })
            };

            AbsoluteLayout.SetLayoutBounds(label, new Rectangle(0, 0, 120, 30));
            AbsoluteLayout.SetLayoutFlags(label, AbsoluteLayoutFlags.PositionProportional);

            // Tạo một Frame
            Frame frame = new Frame
            {
                
                Margin = new Thickness(5),
                BorderColor = Color.FromHex("#1D77D2"),
                WidthRequest = 100,
                HeightRequest = 100,
                CornerRadius = 20,
                Content = new AbsoluteLayout
                {
                    Children =
                    {
                        imageButton,
                        label
                    }
                }
            };

            frame.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command( () =>
                {
                    OnDetailePage(ban.IDBan);
                }),
            });

            grid.Children.Add(frame, columnIndex, rowIndex);
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            try
            {
                string username = await SecureStorage.GetAsync(Globals.KeyUsername);
                if (!string.IsNullOrEmpty(username))
                {
                    lblTenNhanVien.Text = username;
                }
                NguoiDung nguoiDung = await Database.NguoiDungDatabase.GetNguoiDungAsync(username);
                if(nguoiDung.IDNguoiDung == 1)
                {
                    btnXemThongKe.IsVisible = true;
                }
            }
            catch (Exception)
            {

            }

            List<Ban> bans = await Database.BanDatabase.GetBanAsync();

            int columnsCount = 2;
            int rowIndex = 0;
            int columnIndex = 0;

            foreach (Ban ban in bans)
            {
                AddTable(gridTable, columnIndex, rowIndex, ban);
                columnIndex++;
                if (columnIndex >= columnsCount)
                {
                    columnIndex = 0;
                    rowIndex++;
                }
            }
        }

        async void OnDetailePage(int id)
        {
            UserDialogs.Instance.ShowLoading("Xin chờ...");
            await Task.Delay(300);
            await Shell.Current.Navigation.PushAsync(new DetailMenuPage(id));
            UserDialogs.Instance.HideLoading();
        }
    }
}