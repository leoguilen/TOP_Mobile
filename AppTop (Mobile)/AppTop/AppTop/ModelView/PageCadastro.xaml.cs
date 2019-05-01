using AppTop.Model;
using Plugin.LocalNotifications;
using System;
using Xamarin.Essentials;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.Generic;

namespace AppTop
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PageCadastro : ContentPage
	{
		public PageCadastro ()
		{
			InitializeComponent ();

            NavigationPage.SetHasNavigationBar(this, false);
        }

        public async void Loader(bool sts)
        {
            loader.IsRunning = sts;
        }

        public async Task EnviarEmail(string assunto,string corpo, List<string> recebedores )
        {
            var message = new EmailMessage
            {
                BodyFormat = EmailBodyFormat.PlainText,
                Body = corpo,
                Subject = assunto,
                To = recebedores,
                
            };

            await Email.ComposeAsync(message);
        }

        public void ErroEntradaCadastro(Entry e,Label l,Label err,string erro_campo)
        {
            if (string.IsNullOrEmpty(e.Text))
            {
                l.TextColor = Color.Red;
                e.PlaceholderColor = Color.Red;
                err.Text = erro_campo;
                err.IsVisible = true;
                btnCadastrar.IsEnabled = false;
                btnCadastrar.BackgroundColor = Color.Gainsboro;
                btnCadastrar.Text = "Cadastro com erros";
            }
            else
            {
                l.TextColor = Color.Black;
                e.PlaceholderColor = Color.White;
                err.IsVisible = false;
                btnCadastrar.IsEnabled = true;
                btnCadastrar.BackgroundColor = Color.FromHex("#03324c");
                btnCadastrar.Text = "Cadastrar-me".ToUpper();
            }
        }
        public void LimparCampos()
        {
            txtNome.Text = "";
            txtCel.Text = "";
            txtCidade.Text = "";
            txtConfSenha.Text = "";
            txtEmail.Text = "";
            txtSenha.Text = "";
            txtUsername.Text = "";
            pckNivel.SelectedItem = "";
            pckSexo.SelectedItem = "";
            pckUF.SelectedItem = "";
        }

        public void ErroCad()
        {
            lblErro.IsVisible = true;

            lblCelular.TextColor = Color.Red;
            txtCel.PlaceholderColor = Color.Red;

            lblCidade.TextColor = Color.Red;
            txtCidade.PlaceholderColor = Color.Red;

            lblConfSenha.TextColor = Color.Red;
            txtConfSenha.PlaceholderColor = Color.Red;

            lblEmail.TextColor = Color.Red;
            txtEmail.PlaceholderColor = Color.Red;

            lblNome.TextColor = Color.Red;
            txtNome.PlaceholderColor = Color.Red;

            lblSenha.TextColor = Color.Red;
            txtSenha.PlaceholderColor = Color.Red;

            lblUsername.TextColor = Color.Red;
            txtUsername.PlaceholderColor = Color.Red;

            dtPicDtNasc.TextColor = Color.Red;

            pckNivel.TitleColor = Color.Red;
            pckNivel.TextColor = Color.Red;

            pckSexo.TitleColor = Color.Red;
            pckSexo.TextColor = Color.Red;

            pckUF.TextColor = Color.Red;
            pckUF.TitleColor = Color.Red;
        }

        private async void Cadastrar_Clicked(object sender, EventArgs e)
        {
            Loader(true);
            btnCadastrar.IsVisible = false;

            Usuario us = new Usuario();
            us.Nome = txtNome.Text;
            switch (pckSexo.SelectedItem.ToString())
            {
                case "Masculino":
                    us.Sexo = "M";
                    break;
                case "Feminino":
                    us.Sexo = "F";
                    break;
                default:
                    break;
            }

            us.DataNascimento = string.Format("{0:yyyy-MM-dd}", dtPicDtNasc.Date);
            us.Username = txtUsername.Text;
            us.Senha = txtSenha.Text;
            us.Cidade = txtCidade.Text;
            us.Uf = pckUF.SelectedItem.ToString();
            us.NivelAcademico = pckNivel.SelectedIndex;
            string cel = string.Format("{0: (##) #####-####}", txtCel.Text);

            var t = Task.Run(() => HttpClientUsuario.NewUser(us, txtEmail.Text, cel));
            bool valid = t.Result;

            if (valid)
            {
                await Task.Delay(4000);

                await DisplayAlert("Sucesso", "Seu cadastro foi efetuado com sucesso", "Ok, Vamos lá");
                Random rand = new Random(DateTime.Now.Millisecond);
                int codConfirma = rand.Next(1000, 9999);

                await Navigation.PushAsync(new PageValidaCadastro(txtUsername.Text, codConfirma));
                CrossLocalNotifications.Current.Show("Código de confirmação", codConfirma + " esse é seu código para confirmar o seu cadastro",0, DateTime.Now);
                
                Loader(false);
                btnCadastrar.IsVisible = true;
                LimparCampos();
            }
            else
            {
                await Task.Delay(2000);

                loader.IsRunning = false;
                btnCadastrar.IsVisible = true;

                await DisplayAlert("Erro", "Seu cadastro não foi efetuado!", "Tentar novamente");
                ErroCad();
            }
        }

        private void TxtNome_Unfocused(object sender, FocusEventArgs e)
        {
            ErroEntradaCadastro(txtNome, lblNome,lblErroNome,"Campo nome deve ser preenchido");
        }

        private void TxtEmail_Unfocused(object sender, FocusEventArgs e)
        {
            ErroEntradaCadastro(txtEmail, lblEmail,lblErroEmail, "Campo email deve ser preenchido");
        }

        private void PckSexo_Unfocused(object sender, FocusEventArgs e)
        {
            if(pckSexo.SelectedIndex < 0)
            {
                pckSexo.TitleColor = Color.Red;
                lblErrorSexo.Text = "Obrigatorio selecionar um gênero";
                btnCadastrar.IsEnabled = false;
                btnCadastrar.BackgroundColor = Color.Gainsboro;
                btnCadastrar.Text = "Cadastro com erros";
            } else
            {
                pckSexo.TitleColor = Color.Black;
                btnCadastrar.IsEnabled = true;
                btnCadastrar.BackgroundColor = Color.FromHex("#03324c");
                btnCadastrar.Text = "Cadastrar-me".ToUpper();
            }
        }
        
        private void TxtUsername_Unfocused(object sender, FocusEventArgs e)
        {
            ErroEntradaCadastro(txtUsername, lblUsername,lblErroUser, "Campo username deve ser preenchido");
        }

        private void TxtSenha_Unfocused(object sender, FocusEventArgs e)
        {
            ErroEntradaCadastro(txtSenha, lblSenha,lblErroSenha, "Campo senha deve ser preenchido");
        }

        private void TxtConfSenha_Unfocused(object sender, FocusEventArgs e)
        {
            if(string.IsNullOrEmpty(txtConfSenha.Text))
            {
                ErroEntradaCadastro(txtConfSenha, lblConfSenha,lblErroConfSenha, "Campo confirmar senha deve ser preenchido");
            } else
            {
                if(!txtConfSenha.Text.Equals(txtSenha.Text))
                {
                    lblConfSenha.TextColor = Color.Red;
                    txtConfSenha.PlaceholderColor = Color.Red;
                    lblErroConfSenha.Text = "Senhas digitadas não conferem";
                    btnCadastrar.IsEnabled = false;
                } else
                {
                    lblConfSenha.TextColor = Color.Black;
                    txtConfSenha.PlaceholderColor = Color.White;
                    lblErroConfSenha.IsVisible = false;
                    btnCadastrar.IsEnabled = true;
                }
            }
        }

        private void TxtCidade_Unfocused(object sender, FocusEventArgs e)
        {
            ErroEntradaCadastro(txtCidade, lblCidade,lblErroCidade, "Campo cidade deve ser preenchido");
        }

        private void PckUF_Unfocused(object sender, FocusEventArgs e)
        {
            if (pckUF.SelectedIndex < 0)
            {
                pckUF.TitleColor = Color.Red;
                lblErroCidade.Text = "Insira uma cidade e/ou selecione uma UF";
                btnCadastrar.IsEnabled = false;
                btnCadastrar.BackgroundColor = Color.Gainsboro;
                btnCadastrar.Text = "Cadastro com erros";
            }
            else
            {
                pckUF.TitleColor = Color.Black;
                lblErroCidade.IsVisible = false;
                btnCadastrar.IsEnabled = true;
                btnCadastrar.BackgroundColor = Color.FromHex("#03324c");
                btnCadastrar.Text = "Cadastrar-me".ToUpper();
            }
        }

        private void PckNivel_Unfocused(object sender, FocusEventArgs e)
        {
            if (pckNivel.SelectedIndex < 0)
            {
                pckNivel.TitleColor = Color.Red;
                lblErroNivel.Text = "Selecione um nivel acadêmico";
                btnCadastrar.IsEnabled = false;
                btnCadastrar.BackgroundColor = Color.Gainsboro;
                btnCadastrar.Text = "Cadastro com erros";
            }
            else
            {
                pckNivel.TitleColor = Color.Black;
                lblErroNivel.IsVisible = false;
                btnCadastrar.IsEnabled = true;
                btnCadastrar.BackgroundColor = Color.FromHex("#03324c");
                btnCadastrar.Text = "Cadastrar-me".ToUpper();
            }
        }

        private void TxtCel_Unfocused(object sender, FocusEventArgs e)
        {
            ErroEntradaCadastro(txtCel, lblCelular,lblErroCel, "Campo celular deve ser preenchido");
        }
    }
}