
using my_expense_manager.Models;
using my_expense_manager.Services;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: ExportFont("Poppins-Regular.ttf", Alias = "R")]
[assembly: ExportFont("Poppins-ExtraLight.ttf", Alias = "EL")]
[assembly: ExportFont("Poppins-Bold", Alias = "B")]
[assembly: ExportFont("", Alias = "")]
[assembly: ExportFont("", Alias = "")]

namespace my_expense_manager
{
    public partial class App : Application
    {
        private static SqHelperService db;

        public static SqHelperService sql
        {
            get
            {
                if (db == null)
                {

                    db = new SqHelperService(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "EMApp.db3"));

                    
                }

                return db;

            }

        }
        public App()
        {
            InitializeComponent();

           
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
