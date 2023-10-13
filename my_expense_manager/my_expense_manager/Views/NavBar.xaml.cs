using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace my_expense_manager.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NavBar : ContentView
    {
        public static readonly BindableProperty IsButtonVisibleProperty = BindableProperty.Create(
            nameof(IsButtonVisible),
            typeof(bool),
            typeof(NavBar),
            true);

        public bool IsButtonVisible
        {
            get { return (bool)GetValue(IsButtonVisibleProperty); }
            set { SetValue(IsButtonVisibleProperty, value); }
        }
        public NavBar()
        {
            InitializeComponent();
            if (IsButtonVisible == true)
            {
                IsEnabled = true;
            }
            else 
            {
               
                IsEnabled = false;

            }
            SetBinding(IsEnabledProperty, new Binding(nameof(IsButtonVisible), source: this));
            
            
            

        }
      

        private void NavigateMethod(object sender, EventArgs e)
        {

           

             Application.Current.MainPage.Navigation.PushModalAsync(new CreatTrasactionPage(null));

        }

    }
}