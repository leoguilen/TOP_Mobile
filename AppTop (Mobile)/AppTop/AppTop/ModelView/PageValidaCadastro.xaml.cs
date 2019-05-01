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
            user_logado = username;
            Cod_Verifica = cod;

            Vibration.Vibrate(TimeSpan.FromMilliseconds(250));

        }

        private async void Validar_Clicked(object sender, EventArgs e)
        {
            int valorCod = int.Parse(txtCodVerificar.Text);

            if (valorCod == Cod_Verifica)
            {
                await DisplayAlert("Validação", "Cadastro confirmado com sucesso", "Começar");
                await Navigation.PushAsync(new PagePrincipal(user_logado));
            }
            else
            {
                bool tentar = await DisplayAlert("Validação", "Seu código não esta correto", "Tentar novamente",null);
            }
                
        }
    }
}