using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppTop
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PagePrincipal : MasterDetailPage
	{
        private string user_logado;

		public PagePrincipal (string username)
		{
			InitializeComponent ();

            user_logado = username;

            Detail = new NavigationPage(new PagePrincipalDetail(user_logado));
            NavigationPage.SetHasNavigationBar(this, false);

            lblTitleMenu.Text = "Olá, " + user_logado.ToUpper();
        }

        private void Home(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new PagePrincipal(user_logado));
            IsPresented = false;
        }

        private async void Sair(object sender, System.EventArgs e)
        {
            IsPresented = false;
            bool confSair = await DisplayAlert("Sair", "Deseja realmente sair?", "Sim", "Não");

            if(confSair)
            {
                Environment.Exit(0); 
            } else
            {
                return;
            }
            
        }
    }
}