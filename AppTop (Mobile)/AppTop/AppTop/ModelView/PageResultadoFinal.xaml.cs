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
	public partial class PageResultadoFinal : ContentPage
	{
        private readonly string user_logado;

		public PageResultadoFinal (string username)
		{
			InitializeComponent ();

            NavigationPage.SetHasNavigationBar(this, false);

            user_logado = username;

            Resultado result = HttpClientResultado.GetResult(user_logado).FirstOrDefault();

            lblResultado.Text = "O seu resultado é " + result.NomeCurso;
            lblResultado.FontSize = 35;
            lblResultado.FontAttributes = FontAttributes.Bold;
		}

        protected override bool OnBackButtonPressed()
        {
            return false;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PagePrincipal(user_logado));
        }
    }
}