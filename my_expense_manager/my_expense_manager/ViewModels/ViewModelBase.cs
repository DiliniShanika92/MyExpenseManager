using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace my_expense_manager.ViewModels
{
    public class ViewModelBase : BaseViewModel
    {
        public async Task<bool> CheckInternetConnection()
        {
            try
            {
                var current = Connectivity.NetworkAccess;

                if (current == NetworkAccess.Internet)
                {
                    // Internet connection is available
                    //           _ = Application.Current.MainPage.DisplayAlert("Internet Connection", "Internet connection is available.", "OK");
                    return true;
                }
                else
                {
                    // No internet connection

                    return false;
                }
            }
            catch (Exception ex)
            {
                _ = Application.Current.MainPage.DisplayAlert("InternetConnection Error !", ex.Message, "OK");
                return false;
            }
        }


    }
}
