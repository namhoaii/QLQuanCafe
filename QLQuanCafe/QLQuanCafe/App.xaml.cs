using QLQuanCafe.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QLQuanCafe
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            //DependencyService.Register<MockDataStore>();
            /*MainPage = new LoginPage();*/
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
