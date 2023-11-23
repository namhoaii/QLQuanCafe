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

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            try
            {
                string username = await SecureStorage.GetAsync(Globals.KeyUsername);
                if(!string.IsNullOrEmpty(username))
                {
                    lblTenNhanVien.Text = username;
                }
            }
            catch (Exception)
            {

            }
        }
    }
}