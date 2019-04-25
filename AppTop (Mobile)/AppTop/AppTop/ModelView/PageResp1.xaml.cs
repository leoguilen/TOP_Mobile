using AppTop.Model;
using AppTop.ModelView;
using Plugin.InputKit.Shared.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppTop
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PageResp1 : ContentPage
	{
        private readonly string user_logado;
        private const int numPagina = 1;
		public PageResp1 (string username)
		{
			InitializeComponent ();
            user_logado = username;

            List<string> _listResps = new List<string>();

            NavigationPage.SetHasNavigationBar(this, false);

            //Dados inseridos na barra do usuario na pagina de teste
            lblUser.Text += username;
            lblSts.Text = "Status: Em Andamento";
            lblSts.TextColor = Color.Green;

            //Calculo para a conclusao em porcentagem
            int numTotalQuestoes = HttpClientPergunta.GetAllQuestions().Count();
            double percPorQuestao = (numPagina * 100) / numTotalQuestoes;
            lblPerc.Text = percPorQuestao + "%";

            //Total de perguntas e a qtde que faltam para terminar
            lblTotalPerg.Text = numPagina + "/" + numTotalQuestoes;

            Pergunta pergunta = HttpClientPergunta.GetQuestion();

            //Pegando a pergunta gerada e colocando na pagina
            lblPerg.Text = numPagina + ") " + pergunta.DescPergunta;

            foreach (var resp in HttpClientPergunta.GetAnswersForQuestion(pergunta.IdPergunta))
            {
                _listResps.Add(resp.DescResposta);
            }

            ckResp1.Text = _listResps[0];
            ckResp2.Text = _listResps[1];
            ckResp3.Text = _listResps[2];
            ckResp4.Text = _listResps[3];
        }
        
        public bool ValidaResposta(List<CheckBox> list)
        {
            int result = (from res in list.Where(l => l.IsChecked) select res).Count();

            if(result>1)
            {
                return false;
            } else
            {
                return true;
            }
        }

        protected override bool OnBackButtonPressed()
        {
            string aviso = "Se voltar o seu progresso será perdido!".ToUpper();
            
            Device.BeginInvokeOnMainThread(async () => {
                var result = await DisplayAlert("Alerta", "Deseja mesmo encerrar o teste?\n" + aviso, "Sim", "Não");
                if (result)
                {
                    await Navigation.PushAsync(new PagePrincipalDetail(user_logado));
                }
                else
                {
                    lblSts.Text = "Status: Em Andamento";
                    lblSts.TextColor = Color.Green;
                }
            });

            return true;
        }

        private async void Prox_Clicked(object sender, EventArgs e)
        {
            List<CheckBox> checkList = new List<CheckBox>
            {
                ckResp1,
                ckResp2,
                ckResp3,
                ckResp4
            };

            if (!ValidaResposta(checkList))
            {
                await DisplayAlert("Mais de uma resposta selecionada","Apenas uma resposta deve ser selecionada","OK");
                return;
            }
            else
            {
                await Navigation.PushAsync(new PageResp2(user_logado));
            }
        }

        private async void Voltar_Clicked(object sender, EventArgs e)
        {
            lblSts.Text = "Status: Parado";
            lblSts.TextColor = Color.Red;

            string aviso = "Se voltar o seu progresso será perdido!".ToUpper();
            bool voltar = await DisplayAlert("Alerta", "Deseja mesmo encerrar o teste?\n"+aviso, "Sim", "Não");

            if (voltar)
            {
                await Navigation.PushAsync(new PagePrincipal(user_logado));
            }
            else
            {
                lblSts.Text = "Status: Em Andamento";
                lblSts.TextColor = Color.Green;
                return;
            }
            
                
        }
    }
}