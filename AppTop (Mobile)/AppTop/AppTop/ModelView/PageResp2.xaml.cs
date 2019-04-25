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
	public partial class PageResp2 : ContentPage
	{
        private readonly string user_logado;
        private const int numPagina = 2;

        public PageResp2 (string username)
		{
			InitializeComponent ();

            user_logado = username;

            List<string> _listResps = new List<string>();

            NavigationPage.SetHasNavigationBar(this, false);
            
            //Dados inseridos na barra do usuario na pagina de teste
            lblUser.Text += username;
            lblSts.Text = "Status: Iniciado";
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

        private void Prox_Clicked(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new PageResp3(user_logado));
        }

        private async void Voltar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PageResp1(user_logado));
        }
    }
}