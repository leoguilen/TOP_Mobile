using AppTop.ModelView;
using Plugin.Connectivity;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace AppTop
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            if (CrossConnectivity.Current.IsConnected)
                MainPage = new NavigationPage(new MainPage());
            else
                MainPage = new PageErroNet();

            //MainPage = new NavigationPage(new MainPage());  //Inicial MainPage()
        }

        protected override void OnStart()
        {
            
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
