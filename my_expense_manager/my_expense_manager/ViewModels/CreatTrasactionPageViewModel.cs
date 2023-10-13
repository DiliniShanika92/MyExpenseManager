using MvvmHelpers.Commands;
using my_expense_manager.Models;
using my_expense_manager.Services;
using my_expense_manager.Views;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Input;
using System.Xml.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace my_expense_manager.ViewModels
{
    public class CreatTrasactionPageViewModel : ViewModelBase
    {
        private FirebaseServices firebase = new FirebaseServices();
        bool? Returntype;

        public ObservableCollection<string> categoryList { get; set; }
        public ObservableCollection<string> CategoryListtwo { get; set; }
        public ICommand CreateCmd { get; private set; }
        IEnumerable<Category> ca { get; set; }   
      
        bool connection;
        public CreatTrasactionPageViewModel(bool? type)
        {

            Returntype = type;

            connection = CheckInternetConnection().Result;

            CreateCmd = new AsyncCommand(Createtrans);
            categoryList = new ObservableCollection<string>();
            CategoryListtwo = new ObservableCollection<string>();
            Date = DateTime.Now.Date;
            Time = DateTime.Now.TimeOfDay;
            VisibleP = true;
            if (type != null)
            {
                if (type == true)
                {
                    TrsType = "Income";
                    VisibleP = false;
                }
                else
                {
                    TrsType = "Expenses";
                    VisibleP = false;
                }
            }
            _ = loadinga();

            _ = loading();
         
        }

        public double amount;
        public DateTime date;
        public TimeSpan time;
        public string descript;
        public string category;
        public string trsType;
        private Boolean _visibleP;

        public Boolean VisibleP
        {
            get { return _visibleP; }
            set
            {
                if (_visibleP != value)
                {
                    _visibleP = value;
                    OnPropertyChanged();
                }
            }
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
        public async Task loading()
        {
            Date = DateTime.Now;

            Task<IEnumerable<Category>> categories = App.sql.GetAllCategory();
            ca = await categories;
            categoryList.Clear();

            if (Returntype != null)
            {
                if (Returntype == true)
                {
                    foreach (var i in ca)
                    {
                        if (i.CategoryType == true)
                        {
                            categoryList.Add(i.Name);
                        }
                    }

                }
                else
                {
                    foreach (var i in ca)
                    {
                        if (i.CategoryType == false)
                        {
                            categoryList.Add(i.Name);
                        }

                    }

                }

            }
            else
            {
                foreach (var i in ca)
                {
                    categoryList.Add(i.Name);
                }
            }

        }
        public async Task loadinga()
        {

            IEnumerable<Category> cac  = await App.sql.GetAllCategory();
           List<Category> categories = new List<Category>();
            categories = cac.ToList();
            if (categories.Count == 0)
            {
                Category newc = new Category()
                {
                    CategoryType = true,
                    Name = "Salary",


                };
                await App.sql.CreateCategory(newc);

                Category newcc = new Category()
                {
                    CategoryType = false,
                    Name = "Food",


                };
                await App.sql.CreateCategory(newcc);


                _ = loading();


            }

        }
        public async Task Createtrans()
        {
            if (Amount != 0 && Amount != null && TrsType != null && Category != null)
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
                DateTime combinedDateTime;
                if (Date.TimeOfDay == TimeSpan.Zero)
                {
                    combinedDateTime = Date.Add(Time);
                }
                else
                {
                    combinedDateTime = Date;


                }




                transaction trans = new transaction()
                {
                    Amount = Amount,

                    Discription = descript,
                    TransactionType = type,
                    Category = Category,
                    DateAndTime = combinedDateTime



                };
                try
                {
                    var sql = await App.sql.Create(trans);
                    if (connection)
                    {

                        var fr = await firebase.CreateTrans(trans);

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
}
