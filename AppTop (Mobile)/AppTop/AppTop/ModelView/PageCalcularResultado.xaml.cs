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
	public partial class PageCalcularResultado : ContentPage
	{
        private readonly string user_logado;
		public PageCalcularResultado (string username)
		{
			InitializeComponent ();
            NavigationPage.SetHasNavigationBar(this, false);

            user_logado = username;

            loading();
		}

        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        public async void loading()
        {
            loader.IsRunning = true;
            await Task.Delay(3500);
            await Navigation.PushAsync(new PageResultadoFinal(user_logado));

        }

	}
}