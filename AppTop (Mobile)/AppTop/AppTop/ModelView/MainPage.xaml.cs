using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using AppTop.Model;
using System.ComponentModel;
using System.Threading;

namespace AppTop
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }
        
        public async void Loader(bool sts)
        {
            loader.IsRunning = sts;
        }

        private async void BtnEntrar_Clicked(object sender, EventArgs e)
        {
            Loader(true);
            btnCadastro.IsVisible = false;
            btnEntrar.IsVisible = false;
            
            string txtUser = txtUsername.Text;
            string txtPass = txtSenha.Text;
            string erro;

            bool retorno = HttpClientUsuario.LoginValidate(txtUser, txtPass, out erro);
            
            if (string.IsNullOrEmpty(txtUser) || string.IsNullOrEmpty(txtPass))
            {
                await Task.Delay(1000);

                Loader(false);
                btnCadastro.IsVisible = true;
                btnEntrar.IsVisible = true;

                await DisplayAlert("Login", "Identificação para login é obrigatorio", "OK");
            }
            else
            {
                if (string.IsNullOrEmpty(erro))
                {
                    if (retorno)
                    {
                        //await DisplayAlert("Validação de login", "Logado com sucesso! Bem vindo " + txtUser.ToUpper(), "OK");
                        await Task.Delay(4000);

                        txtSenha.Text = "";
                        await Navigation.PushAsync(new PagePrincipal(txtUser));

                        Loader(false);
                        btnCadastro.IsVisible = true;
                        btnEntrar.IsVisible = true;
                    }
                    else
                    {
                        await Task.Delay(2000);

                        loader.IsRunning = false;
                        btnCadastro.IsVisible = true;
                        btnEntrar.IsVisible = true;

                        await DisplayAlert("Validação de login", "Erro ao validar o login", "OK");

                        lblErro.TextColor = Color.Red;
                        lblErro.Text = "Erro! Verifique se o usuario ou a senha estão corretos";
                        lblSenha.TextColor = Color.Red;
                        lblUser.TextColor = Color.Red;
                        txtSenha.PlaceholderColor = Color.Red;
                        txtUsername.PlaceholderColor = Color.Red;
                        txtUsername.Text = "";
                        txtSenha.Text = "";
                    }
                }
                else
                {
                    await DisplayAlert("Erro", erro, "OK");
                }
            }
        }

        private async void BtnCadastro_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PageCadastro());
        }
    }
}
