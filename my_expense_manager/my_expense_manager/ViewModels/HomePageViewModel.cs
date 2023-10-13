
using my_expense_manager.Models;
using my_expense_manager.Services;
using my_expense_manager.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Input;

using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;
using static SQLite.SQLite3;

namespace my_expense_manager.ViewModels
{
    public class HomePageViewModel : ViewModelBase
    {
        public ICommand Navi { get; set; }
        public ICommand NaviIncome { get; set; }
        public ICommand NaviCategory { get; set; }
        public ICommand QRcmd { get; set; }



        public ObservableCollection<transaction> TrnsList { get; set; }



        bool connection;
        IEnumerable<transaction> transactions { get; set; }
        public HomePageViewModel()
        {
            Navi = new MvvmHelpers.Commands.AsyncCommand(NavigateMethod);
            NaviIncome = new MvvmHelpers.Commands.AsyncCommand(NavigateNaviIncomeMethod);
            NaviCategory = new MvvmHelpers.Commands.AsyncCommand(NavigateCategoryMethod);
            QRcmd = new MvvmHelpers.Commands.AsyncCommand(ScanMethod);
            TrnsList = new ObservableCollection<transaction>();
            _ = loading();
        }

        async Task NavigateNaviIncomeMethod()
        {

            NaviIncome = null;

            await Application.Current.MainPage.Navigation.PushModalAsync(new CreatTrasactionPage(true));
        }

        public Double bal;
        public Double income;
        public Double expenses;
        public double Bal
        {
            get => bal;
            set => SetProperty(ref bal, value);
        }
        public double Income
        {
            get => income;
            set => SetProperty(ref income, value);
        }
        public double Expenses
        {
            get => expenses;
            set => SetProperty(ref expenses, value);
        }
        async Task NavigateMethod()
        {

            Navi = null;

            await Application.Current.MainPage.Navigation.PushModalAsync(new CreatTrasactionPage(false));

        }
        async Task ScanMethod()
        {

            var scan = new ZXingScannerPage();
            await Application.Current.MainPage.Navigation.PushModalAsync(scan);
            scan.OnScanResult += (result) =>
            {
                Device.BeginInvokeOnMainThread(async () =>
                {

                    await Application.Current.MainPage.Navigation.PopModalAsync();
                    await Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Item Price", "" + result.Text, "Ok");

                });

            };

        }


        async Task NavigateCategoryMethod()
        {

            Navi = null;

            await Application.Current.MainPage.Navigation.PushModalAsync(new CategoryPage());

        }
        public async Task loading()
        {



            //var b = await firebase.AllTransaction();

            Task<IEnumerable<transaction>> transactionTask = App.sql.GetAll();
            transactions = await transactionTask;
            transactions = transactions.Where(transaction => transaction.DateAndTime.Year == DateTime.Now.Year && transaction.DateAndTime.Month == DateTime.Now.Month);
            TrnsList.Clear();
            foreach (var i in transactions)
            {

                TrnsList.Add(i);


            }

            double amo = 0;
            double inc = 0;
            double exp = 0;

            foreach (var tr in TrnsList)
            {
                if (tr.DateAndTime.Year == DateTime.Now.Year && tr.DateAndTime.Month == DateTime.Now.Month)
                {
                    if (tr.TransactionType)
                    {
                        inc = inc + tr.Amount;

                    }
                    else
                    {
                        exp = exp + tr.Amount;

                    }
                }


            }

            amo = inc - exp;
            Bal = amo;
            Income = inc;
            Expenses = exp;



        }

    }
}
