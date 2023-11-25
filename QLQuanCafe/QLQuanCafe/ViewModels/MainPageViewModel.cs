using Acr.UserDialogs;
using QLQuanCafe.Models;
using QLQuanCafe.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace QLQuanCafe.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        public ICommand FrameTappedCommand { get; }
        public ICommand Logout { get; }
        public ICommand OpenReportPage { get; }

        public MainPageViewModel()
        {
            FrameTappedCommand = new Command(OnFrameSelected);
            Logout = new Command(OnLogout);
            OpenReportPage = new Command(OnReportPage);
        }

        private async void OnReportPage(object obj)
        {
            UserDialogs.Instance.ShowLoading("Xin chờ...");
            await Task.Delay(300);
            await Shell.Current.Navigation.PushAsync(new Report());
            UserDialogs.Instance.HideLoading();
        }

        private async void OnLogout(object obj)
        {
            UserDialogs.Instance.ShowLoading("Xin chờ...");
            await Task.Delay(300);
            UserDialogs.Instance.HideLoading();
            await Shell.Current.Navigation.PushAsync(new LoginPage());
        }

        private async void OnFrameSelected(object obj)
        {
            UserDialogs.Instance.ShowLoading("Xin chờ...");
            await Task.Delay(500);
            int IDBan = Convert.ToInt32(obj);
            await Shell.Current.Navigation.PushAsync(new DetailMenuPage(IDBan));
            UserDialogs.Instance.HideLoading();
        }
    }
}
