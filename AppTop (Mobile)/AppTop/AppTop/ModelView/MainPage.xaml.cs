using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using AppTop.Model;
using System.IO;

namespace AppTop
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private string caminho = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "remember_login.txt");


        public async void Loader(bool sts)
        {
            loader.IsRunning = sts;
        }

        private void ContentPage_Appearing(object sender, EventArgs e)
        {
            if (!File.Exists(caminho))
            {
                File.Create(caminho);
            }
            else
            {
                string text = File.ReadAllText(caminho);

                if(text != "")
                {
                    string[] credenciais = text.Split('/');

                    txtUsername.Text = credenciais[0];
                    txtSenha.Text = credenciais[1];
                    switchSalvarlogin.IsToggled = bool.Parse(credenciais[2]);
                }
            }
        }

        private void SwitchSalvarlogin_Toggled(object sender, ToggledEventArgs e)
        {
            if(switchSalvarlogin.IsToggled)
            {
                File.WriteAllText(caminho, txtUsername.Text + "/" + txtSenha.Text + "/" + switchSalvarlogin.IsToggled);
            } else
            {
                File.WriteAllText(caminho, "");
            }
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
                        await Task.Delay(4000);
                        await Navigation.PushAsync(new PagePrincipal(txtUser));

                        txtSenha.Text = "";
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
