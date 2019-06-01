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
	public partial class PageAvaliacao : ContentPage
	{
        private readonly string user_logado;

		public PageAvaliacao (string username)
		{
			InitializeComponent ();

            user_logado = username;
            this.BindingContext = new Ratings();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            int numRatings = int.Parse(lblNumRating.Text);
            string comentario = txtAvaliacao.Text;

            
            await Navigation.PushAsync(new PagePrincipal(user_logado));
        }

        private void ContentPage_Appearing(object sender, EventArgs e)
        {
            DisplayAlert("Avaliar", "Deixe aqui a sua avaliação sobre o que achou desse resultado", "OK");
        }
    }
}