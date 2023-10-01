
using my_expense_manager.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace my_expense_manager.ViewModels
{
    public class HomePageViewModel : ViewModelBase
    {
        public ICommand Navigation { get; set; }
      
        public HomePageViewModel()
        {
            Navigation = new MvvmHelpers.Commands.AsyncCommand(NavigateMethod);
       
        }
       async Task  NavigateMethod() 
        {
            Navigation = null;
           
            await Application.Current.MainPage.Navigation.PushModalAsync(new CreateTrasactionPage());

        }
    }
}
