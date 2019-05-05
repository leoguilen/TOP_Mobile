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

            lblResultado.Text += result.NomeCurso;
            lblExa.Text += result.ResultadoExatas + "%";
            lblBio.Text += result.ResultadoBiologicas + "%";
            lblHum.Text += result.ResultadoHumanas + "%";
            imgCurso.Source = result.ImagemCurso;
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