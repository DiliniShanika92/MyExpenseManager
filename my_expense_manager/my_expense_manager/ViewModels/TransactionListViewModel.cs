using MvvmHelpers.Commands;
using my_expense_manager.Models;
using my_expense_manager.Services;
using my_expense_manager.Views;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Input;
using Xamarin.Forms;

namespace my_expense_manager.ViewModels
{
    class TransactionListViewModel : ViewModelBase
    {
        private FirebaseServices firebase = new FirebaseServices();

        public ICommand UpdateCmd { get; set; }
        public ICommand DeleteCmd { get; set; }
        public ICommand FilterCmd { get; set; }

        public ICommand Refresh { get; set; }

        public ObservableCollection<transaction> TrnsList { get; set; }
        public ObservableCollection<transaction> TrnsListFr { get; set; }
        public List<transaction> ComparerList { get; set; }
        public List<transaction> ComparerList2 { get; set; }

        bool connection;
        IEnumerable<transaction> transactions { get; set; }
        public TransactionListViewModel()
        {
            connection = CheckInternetConnection().Result;
            UpdateCmd = new AsyncCommand<transaction>(UpdateTrns);
            DeleteCmd = new AsyncCommand<transaction>(DeleteTrns);
            FilterCmd = new MvvmHelpers.Commands.Command(Filter);

            Refresh = new MvvmHelpers.Commands.AsyncCommand(refresh);

            TrnsList = new ObservableCollection<transaction>();
            TrnsListFr = new ObservableCollection<transaction>();
            ComparerList = new List<transaction>();
            _ = loading();

        }
        public DateTime date;
        public Double bal;
        public Double income;
        public Double expenses;
        public DateTime Date
        {
            get => date;
            set => SetProperty(ref date, value);
        }

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
        async Task refresh()
        {
            bool a = await CheckInternetConnection();

            if (a == true)
            {
                TrnsList.Clear();
                TrnsListFr.Clear();
                try
                {
                    await App.sql.Clear();

                    var bc = await firebase.AllTransaction();
                    _ = await App.sql.CreateAll(bc);
                    _ = await firebase.DeleteTransAll();
                    var res = await App.sql.GetAll();
                    var ba = await firebase.CreateTransList(res.ToList());
                }
                catch (Exception ex)
                {

                    throw;
                }

                Task<IEnumerable<transaction>> transactionTask = App.sql.GetAll();
                transactions = await transactionTask;

                foreach (var i in transactions.Reverse())
                {

                    TrnsList.Add(i);


                }

                var b = await firebase.AllTransaction();
                foreach (var i in b)
                {

                    TrnsListFr.Add(i);


                }

                Cal(TrnsList.ToList());

                await Task.Delay(2000);
                IsBusy = false;
            }
            else
            {
                await Task.Delay(2000);
                IsBusy = false;
            }
        }
        public void Filter()
        {
            TrnsList.Clear();
            var filteredList = transactions.Where(item => item.DateAndTime.Year == Date.Year && item.DateAndTime.Month == Date.Month).Reverse(); ;
            foreach (var i in filteredList)
            {

                TrnsList.Add(i);


            }
            Cal(TrnsList.ToList());
        }
        public void Cal(List<transaction> List)
        {

            double amo = 0;
            double inc = 0;
            double exp = 0;
            Bal = 0;
            Income = 0;
            Expenses = 0;
            foreach (var tr in List)
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

            amo = inc - exp;
            Bal = amo;
            Income = inc;
            Expenses = exp;

        }
        public async Task Comparer()
        {
            ComparerList = TrnsList.Except(TrnsListFr, new TransactionComparer()).ToList();
            if (ComparerList.Count > 0)
            {


                try
                {
                    await firebase.CreateTransList(ComparerList);
                }
                catch (Exception ex)
                {

                    throw;
                }

            }
            //ComparerList2 = TrnsListFr.Except(TrnsList, new TransactionComparer()).ToList();
            //if (ComparerList2.Count > 0)
            //{


            //    try
            //    {
            //        await App.sql.Clear();
            //        TrnsListFr.Clear();
            //        var b = await firebase.AllTransaction();
            //        _ =await  App.sql.CreateAll(b);
            //       _ = await firebase.DeleteTransAll();
            //       var res= await App.sql.GetAll();
            //        var ba = await firebase.CreateTransList(res.ToList());
            //    }
            //    catch (Exception ex)
            //    {

            //        throw;
            //    }
            //    TrnsList.Clear();
            //    Task<IEnumerable<transaction>> transactionTask = App.sql.GetAll();
            //    transactions = await transactionTask;
            //    TrnsList.Clear();
            //    foreach (var i in transactions)
            //    {

            //        TrnsList.Add(i);


            //    }

            //}


        }
        public async Task loading()
        {

            Date = DateTime.Now;

            //var b = await firebase.AllTransaction();

            Task<IEnumerable<transaction>> transactionTask = App.sql.GetAll();
            transactions = await transactionTask;
            TrnsList.Clear();
            foreach (var i in transactions.Reverse())
            {

                TrnsList.Add(i);


            }
            if (connection)
            {
                var b = await firebase.AllTransaction();
                foreach (var i in b)
                {

                    TrnsListFr.Add(i);


                }

                _ = CheckRecycle();
                _ = Comparer();

            }

            Cal(TrnsList.ToList());





        }
        public async Task CheckRecycle()
        {
            try
            {
                // await App.sql.DeleteRecycleAll();
                Task<IEnumerable<RecycleTransaction>> transactionTaska = App.sql.GetAllRecycle();
                IEnumerable<RecycleTransaction> transactions = await transactionTaska;
                List<RecycleTransaction> b = transactions.ToList();
                if (b.Count > 0)
                {
                    foreach (var i in transactions)
                    {

                        await firebase.DeleteTrans(i.RId);
                        await App.sql.DeleteRecycle(i.Id);

                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        public async Task DeleteTrns(transaction tr)
        {
            connection = CheckInternetConnection().Result;
            bool result = await Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Delete Confirmation", "Are you sure you want to delete this item?", "OK", "Cancel");
            if (result)
            {
                try
                {
                    var res = await App.sql.Delete(tr);

                    if (connection)
                    {
                        var resFr = await firebase.DeleteTrans(tr.Id);


                    }
                    else
                    {
                        RecycleTransaction nn = new RecycleTransaction()
                        {
                            RId = tr.Id,

                        };
                        var ac = App.sql.CreateRecycle(nn);



                    }


                    _ = loading();


                }
                catch (Exception ex)
                {

                    _ = Application.Current.MainPage.DisplayAlert(" Error !", ex.Message, "OK");
                }


            }

        }
        public async Task UpdateTrns(transaction tr)
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new EditTrasactionPage(tr));
        }


    }
}
