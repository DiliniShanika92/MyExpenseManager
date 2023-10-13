using my_expense_manager.Models;
using my_expense_manager.ViewModels;
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
	public partial class EditTrasactionPage : ContentPage
	{
		public EditTrasactionPage(transaction tr)
		{
			InitializeComponent();
            BindingContext = new EditTrasactionPageViewModel(tr);
        }
	}
}