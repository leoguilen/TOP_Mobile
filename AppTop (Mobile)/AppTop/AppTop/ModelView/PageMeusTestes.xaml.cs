using AppTop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppTop.ModelView
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PageMeusTestes : ContentPage
	{
        private readonly string user_logado; 

		public PageMeusTestes (string user)
		{
            user_logado = user;
            TestesListView.Username = user_logado;

			InitializeComponent ();
		}
        
        private void ListViewItem_Tabbed(object sender, ItemTappedEventArgs e)
        {
            var res = e.Item as Resultado;
            var vm = BindingContext as TestesListView;
            vm?.ShowOrHiddenResults(res);
        }
    }
}