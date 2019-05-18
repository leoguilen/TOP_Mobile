using Android.App;
using Plugin.LocalNotifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppTop
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PageValidaCadastro : ContentPage
	{
        private string user_logado;
        private int Cod_Verifica;
		public PageValidaCadastro (string username, int cod)
		{
			InitializeComponent ();
            NavigationPage.SetHasNavigationBar(this, false);

            user_logado = username;
            Cod_Verifica = cod;

            Vibration.Vibrate(TimeSpan.FromMilliseconds(250));

        }

        protected override bool OnBackButtonPressed()
        {
            DisplayAlert("NOTIFICAÇÃO", "Use a notificação com o código para liberar seu acesso!", "OK");
            return true;
        }


        private async void Validar_Clicked(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(txtCodVerificar.Text))
            {
                int valorCod = int.Parse(txtCodVerificar.Text);

                if (valorCod == Cod_Verifica)
                {
                    await DisplayAlert("Validação", "Cadastro confirmado com sucesso", "Começar");
                    await Navigation.PushAsync(new PagePrincipal(user_logado));
                }
                else
                {
                    await DisplayAlert("Validação", "Seu código não esta correto", "Tentar novamente");
                }
            } else
            {
                await DisplayAlert("ALERTA", "Digite o código de verificação enviado para você!", "OK");
                return;
            }

            
                
        }
    }
}