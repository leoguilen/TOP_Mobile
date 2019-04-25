using Plugin.Connectivity;
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
	public partial class PageErroNet : ContentPage
	{
		public PageErroNet ()
		{
			InitializeComponent ();
		}

        public bool CheckConnection()
        {
            if (CrossConnectivity.Current.IsConnected)
                return true;
            else
                return false;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            lblCheckNet.IsVisible = true;

            await Task.Delay(2000);
            
            if (CheckConnection())
            {
                Application.Current.MainPage = new NavigationPage(new MainPage());
            }
            else
            {
                lblCheckNet.IsVisible = false;
                await DisplayAlert("Erro de conexão", "Ainda não identificamos o acesso a internet", "OK");
                return;
            }
        }
    }
}