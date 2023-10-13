using my_expense_manager.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace my_expense_manager.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TrasactionListPage : ContentPage
    {
        private FirebaseServices firebase = new FirebaseServices();
        public TrasactionListPage()
        {
            InitializeComponent();
        }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolValue)
            {
                return !boolValue;
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
        private void UpdateTrs(object sender, EventArgs e)
        {
            // Execute your UpdateCmd command here
           

        }
        private void DeleteTrs(object sender, EventArgs e)
        {
         //   firebase.DeleteTrans()

        }
    }
}