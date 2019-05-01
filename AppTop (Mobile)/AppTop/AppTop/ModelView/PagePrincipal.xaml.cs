using AppTop.Model;
using AppTop.ModelView;
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

        private async void Home(object sender, System.EventArgs e)
        {
            Detail = new NavigationPage(new PagePrincipal(user_logado));
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

        private void MeuTeste_Tapped(object sender, EventArgs e)
        {
            if(HttpClientResultado.GetResult(user_logado).Count() == 0)
            {
                Detail = new NavigationPage(new PageSemTeste());
                IsPresented = false;
            }
            else
            {
                Detail = new NavigationPage(new PageMeusTestes(user_logado));
                IsPresented = false;
            }
            
        }
    }
}