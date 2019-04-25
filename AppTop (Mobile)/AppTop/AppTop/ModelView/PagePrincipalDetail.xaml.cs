using AppTop.Model;
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
	public partial class PagePrincipalDetail : ContentPage
	{
        private readonly string user_logado;
		public PagePrincipalDetail (string username)
		{
			InitializeComponent ();
            user_logado = username;
            
            lblUserLogon.Text += user_logado;

            if (HttpClientUsuario.CheckTest(user_logado) == 0)
            {
                btnStartTest.IsVisible = true;
                lblStatus.Text += "Pendente";
            }
            else
            {
                btnStartTest.IsVisible = false;
                btnIrSite.IsVisible = true;
                btnOutroTeste.IsVisible = true;
                lblStatus.Text += "Feito";
                
                lblUltimoRes.Text += HttpClientResultado.GetResult(user_logado).NomeCurso;
                lblUltimoData.Text += string.Format("{0:dd-MM-yyyy}", HttpClientResultado.GetResult(user_logado).DataFim);
            }
        }

        private void BtnStartTest_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PageResp1(user_logado));
        }

        protected override bool OnBackButtonPressed()
        {
            Device.BeginInvokeOnMainThread(async () => {
                var result = await this.DisplayAlert("Alerta", "Você realmente quer sair?", "Sim", "Não");
                if (result) Environment.Exit(1);
            });

            return true;
        }

        private void BtnIrSite_Clicked(object sender, EventArgs e)
        {
            //Ir para o site
            Device.OpenUri(new Uri("http://testetop.localhost:8080/top/"));
        }

        private void BtnOutroTeste_Clicked(object sender, EventArgs e)
        {
            //Iniciar novo teste
        }
    }
}