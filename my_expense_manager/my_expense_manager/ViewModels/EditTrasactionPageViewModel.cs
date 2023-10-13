using MvvmHelpers.Commands;
using my_expense_manager.Models;
using my_expense_manager.Services;
using my_expense_manager.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace my_expense_manager.ViewModels
{
    public class EditTrasactionPageViewModel : ViewModelBase
    {
        private FirebaseServices firebase = new FirebaseServices();
        IEnumerable<Category> ca { get; set; }
        public List<string> CategoryList { get; set; }
        public ObservableCollection<string> categoryList { get; set; }
        public ICommand UpdateCmd { get; private set; }
        bool connection;
        public EditTrasactionPageViewModel(transaction TR)
        {
            connection = CheckInternetConnection().Result;
            categoryList = new ObservableCollection<string>();
            _ = Loading(TR);
            CategoryList = new List<string>();
            UpdateCmd = new AsyncCommand(Updatetrans);
           

        }
        public double amount;
        public DateTime date;
        public TimeSpan time;
        public string descript;
        public string category;
        public string trsType;
        public int id;

        public int Id
        {
            get => id;
            set => SetProperty(ref id, value);
        }
        public double Amount
        {
            get => amount;
            set => SetProperty(ref amount, value);
        }

        public DateTime Date
        {
            get => date;
            set => SetProperty(ref date, value);
        }
        public TimeSpan Time
        {
            get => time;
            set => SetProperty(ref time, value);
        }

        public string Descript
        {
            get => descript;
            set => SetProperty(ref descript, value);
        }
        public string Category
        {
            get => category;
            set => SetProperty(ref category, value);
        }
        public string TrsType
        {
            get => trsType;
            set => SetProperty(ref trsType, value);
        }

        public async Task Loading(transaction tr)
        {
            Category = tr.Category;
           
           
           
            
                if (tr.TransactionType == true)
                {
                    TrsType = "Income";
                  
                }
                else
                {
                    TrsType = "Expenses";
                   
                }

            Id = tr.Id;
            Amount = tr.Amount;
            Date = tr.DateAndTime;
            Time = tr.DateAndTime.TimeOfDay;
            Descript = tr.Discription;
            Date = DateTime.Now;

            Category = tr.Category;



            //var b = await firebase.AllTransaction();
            Task<IEnumerable<Category>> categories = App.sql.GetAllCategory();
            ca = await categories;
            categoryList.Clear();
           
            foreach (var i in ca)
            {

                categoryList.Add(i.Name);


            }

            Category =  tr.Category;
        }
        public async Task Updatetrans()
        {
            connection = CheckInternetConnection().Result;
            bool type = false;

            if (TrsType == "Income")
            {
                type = true;
            }
            else
            {
                type = false;
            }

            DateTime combinedDateTime = Date.Add(Time);



            transaction trans = new transaction()
            {
                Amount = Amount,
                Id = Id,
                Discription = descript,
                TransactionType = type,
                Category = Category,
                DateAndTime = combinedDateTime



            };
            try
            {
                var res = await App.sql.Update(trans);
                if (connection)
                {

                    var resFr = await firebase.UpdateTrans(trans.Id, trans);

                }

                await Application.Current.MainPage.Navigation.PushModalAsync(new MainPage());

            }
            catch (Exception ex)
            {

                _ = Application.Current.MainPage.DisplayAlert(" Error !", ex.Message, "OK");
            }



        }

    }
}
