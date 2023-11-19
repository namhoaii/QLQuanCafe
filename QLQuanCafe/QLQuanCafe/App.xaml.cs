using QLQuanCafe.Data;
using QLQuanCafe.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using QLQuanCafe.Helpers;

namespace QLQuanCafe
{
    public partial class App : Application
    {
        /*static Database database;

        public static Database Database
        {
            get
            {
                if (database == null)
                {
                    database = new Database(Globals.PathDB);
                }
                return database;
            }
        }*/


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
