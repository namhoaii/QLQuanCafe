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

        public MainPageViewModel()
        {
            FrameTappedCommand = new Command(OnFrameSelected);
        }

        private async void OnFrameSelected(object obj)
        {
            UserDialogs.Instance.ShowLoading("Xin chờ...");
            await Task.Delay(500);
            int IDBan = Convert.ToInt32(obj);
            UserDialogs.Instance.HideLoading();
            await Shell.Current.Navigation.PushAsync(new DetailMenuPage(IDBan));
        }
    }
}
