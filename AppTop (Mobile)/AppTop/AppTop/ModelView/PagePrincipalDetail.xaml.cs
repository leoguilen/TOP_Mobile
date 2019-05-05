using AppTop.Model;
using System;
using System.Linq;
using Xamarin.Essentials;
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
            
            if(HttpClientResultado.GetAllResults().Count() > 0)
            {
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

                    lblUltimoRes.Text += HttpClientResultado.GetResult(user_logado).Where(us => us.NovoTeste == 1).FirstOrDefault().NomeCurso;
                    lblUltimoData.Text += string.Format("{0:dd-MM-yyyy}", HttpClientResultado.GetResult(user_logado).Where(us=>us.NovoTeste == 1).FirstOrDefault().DataFim);
                }
            }
            else
            {
                btnStartTest.IsVisible = true;
                lblStatus.Text += "Pendente";
            }
            
        }

        private void BtnStartTest_Clicked(object sender, EventArgs e)
        {
            HttpClientTeste.StartNewTest(user_logado);
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
            //Device.OpenUri(new Uri("http://192.168.43.108:8080/Top/login.php"));
            Device.OpenUri(new Uri("http://192.168.0.5:8080/Top/login.php"));
            
        }

        private async void BtnOutroTeste_Clicked(object sender, EventArgs e)
        {
            bool newTest = await DisplayAlert("Alerta de novo teste", "Iniciar um novo teste agora?", "Sim", "Não");

            if(newTest)
            {
                HttpClientTeste.StartNewTest(user_logado);
                await Navigation.PushAsync(new PageResp1(user_logado));

            } else
            {
                return;
            }
        }
        
    }
}