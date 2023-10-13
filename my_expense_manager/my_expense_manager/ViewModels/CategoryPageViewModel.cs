using MvvmHelpers.Commands;
using my_expense_manager.Models;
using my_expense_manager.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace my_expense_manager.ViewModels
{
    public class CategoryPageViewModel : ViewModelBase
    {
        public ObservableCollection<Category> CategoryList { get; set; }
        public ObservableCollection<Category> CategoryListtwo { get; set; }
        public ObservableCollection<string> categoryList { get; set; }
        public ICommand CreateCmd { get; private set; }
        public ICommand DeleteCmd { get; set; }

        bool connection;
        IEnumerable<Category> ca { get; set; }
        public CategoryPageViewModel()
        {
            Nav = false;
            DeleteCmd = new AsyncCommand<Category>(DeleteCa);
            connection = CheckInternetConnection().Result;
            CategoryList = new ObservableCollection<Category>();
            CategoryListtwo = new ObservableCollection<Category>();
            CreateCmd = new AsyncCommand(Createtrans);
            _ = loading();
            CType = "Income";

        }

        public string cName;
        public string cType;

        public string Cname
        {
            get => cName;
            set => SetProperty(ref cName, value);
        }
        public bool nav;
        public bool Nav
        {
            get => nav;
            set => SetProperty(ref nav, value);
        }
        public string CType
        {
            get => cType;
            set => SetProperty(ref cType, value);
        }
        public async Task DeleteCa(Category tr)
        {
            bool result = await Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Delete Confirmation", "Are you sure you want to delete this item?", "OK", "Cancel");
            if (result)
            {
                try
                {
                    var sql = await App.sql.DeleteCategory(tr.Id);
                }
                catch (Exception ex)
                {

                    throw;
                }

                _ = loading();
            }

        }
        public async Task Createtrans()
        {

            if (Cname != "" && Cname != null)
            {

                bool type = false;

                if (CType == "Income")
                {
                    type = true;
                }
                else
                {
                    type = false;
                }


                Category cc = new Category()
                {
                    Name = Cname,
                    CategoryType = type




                };
                try
                {
                    var sql = await App.sql.CreateCategory(cc);


                    _ = loading();
                    //  await Application.Current.MainPage.Navigation.PushModalAsync(new MainPage());



                }
                catch (Exception ex)
                {

                    _ = Application.Current.MainPage.DisplayAlert(" Error !", ex.Message, "OK");
                }
                cName = null;
            }

        }
        public async Task loading()
        {



            //var b = await firebase.AllTransaction();

            Task<IEnumerable<Category>> categories = App.sql.GetAllCategory();
            ca = await categories;
            CategoryList.Clear();
            CategoryListtwo.Clear();
            foreach (var i in ca)
            {
                if (i.CategoryType == true)
                {
                    CategoryList.Add(i);
                }
                else
                {
                    CategoryListtwo.Add(i);
                }



            }







        }
    }
}
