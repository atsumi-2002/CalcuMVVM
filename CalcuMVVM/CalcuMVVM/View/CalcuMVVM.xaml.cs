using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CalcuMVVM.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CalcuMVVM : ContentPage
	{
		public CalcuMVVM ()
		{
			InitializeComponent ();
            this.BindingContext = new ViewModel.CalcuVM();
		}
	}
}