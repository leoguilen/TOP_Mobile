﻿using AppTop.Model;
using Plugin.InputKit.Shared.Controls;
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
	public partial class PageResp10 : ContentPage
	{
        private readonly string user_logado;
        private const int numPagina = 10;
        private List<Resposta> _listResps;
        private List<double> _listValoresExatas;
        private List<double> _listValoresHumanas;
        private List<double> _listValoresBiologicas;

        public PageResp10 (string username,List<double> _listE,List<double> _listH,List<double> _listB)
		{
			InitializeComponent ();

            user_logado = username;

            _listResps = new List<Resposta>();

            _listValoresExatas = _listE;
            _listValoresHumanas = _listH;
            _listValoresBiologicas = _listB;

            NavigationPage.SetHasNavigationBar(this, false);
            
            //Dados inseridos na barra do usuario na pagina de teste
            lblUser.Text += username;
            lblSts.Text = "Status: Iniciado";
            lblSts.TextColor = Color.Green;

            //Calculo para a conclusao em porcentagem
            int numTotalQuestoes = HttpClientPergunta.GetAllQuestions().Count() - 2;
            double percPorQuestao = (numPagina * 100) / numTotalQuestoes;
            lblPerc.Text = percPorQuestao + "%";

            //Total de perguntas e a qtde que faltam para terminar
            lblTotalPerg.Text = numPagina + "/" + numTotalQuestoes;

            Pergunta pergunta = HttpClientPergunta.GetQuestion();

            //Pegando a pergunta gerada e colocando na pagina
            lblPerg.Text = numPagina + ") " + pergunta.DescPergunta;

            foreach (var resp in HttpClientPergunta.GetAnswersForQuestion(pergunta.IdPergunta))
            {
                _listResps.Add(resp);
            }

            ckResp1.Text = _listResps[0].DescResposta;
            ckResp2.Text = _listResps[1].DescResposta;
            ckResp3.Text = _listResps[2].DescResposta;
            ckResp4.Text = _listResps[3].DescResposta;

        }

        public int ValidaResposta(List<CheckBox> list)
        {
            int valid = 0;
            int result = (from res in list.Where(l => l.IsChecked) select res).Count();

            // Erro 2 se o result for igual 0
            // Erro 1 se o result for maior que 1
            // 0 para result valido

            if (result == 0)
            {
                valid = 2;
            }
            else
            {
                if (result > 1)
                {
                    valid = 1;
                }
                else
                {
                    valid = 0;
                }
            }

            return valid;
        }

        public List<double> CalcularResultadosDasRespostas(List<double> valoresExatas, List<double> valoresHumanas, List<double> valoresBio)
        {
            List<double> _listResultadosFinal = new List<double>();
            double resultadoExatas = 0, resultadoHumanas = 0, resultadoBiologicas = 0;

            foreach (double valueE in valoresExatas)
            {
                resultadoExatas += valueE;
            }

            foreach (double valueH in valoresHumanas)
            {
                resultadoHumanas += valueH;
            }

            foreach (double valueB in valoresBio)
            {
                resultadoBiologicas += valueB;
            }

            _listResultadosFinal.Add((resultadoExatas / 10) * 100);
            _listResultadosFinal.Add((resultadoHumanas / 10) * 100);
            _listResultadosFinal.Add((resultadoBiologicas / 10) * 100);

            return _listResultadosFinal;

        }

        protected override bool OnBackButtonPressed()
        {
            string aviso = "Se voltar o seu progresso será perdido!".ToUpper();

            Device.BeginInvokeOnMainThread(async () => {
                var result = await DisplayAlert("Alerta", "Deseja mesmo encerrar o teste?\n" + aviso, "Sim", "Não");
                if (result)
                {
                    HttpClientTeste.CancelTest(user_logado);
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

            if (ValidaResposta(checkList) != 0)
            {
                switch (ValidaResposta(checkList))
                {
                    case 1:
                        {
                            await DisplayAlert("Mais de uma resposta selecionada", "Apenas uma resposta deve ser selecionada", "OK");
                            break;
                        }
                    case 2:
                        {
                            await DisplayAlert("Nenhuma resposta selecionada", "Você deve selecionar uma resposta!", "OK");
                            break;
                        }
                    default:
                        break;
                }

                return;
            }
            else
            {
                foreach (var itemChecked in from check in checkList.Where(c => c.IsChecked) select check)
                {
                    switch (itemChecked.ClassId)
                    {
                        case "1":
                            {
                                _listValoresExatas.Add(Math.Round(_listResps[0].ValorExatas, 2));
                                _listValoresHumanas.Add(Math.Round(_listResps[0].ValorHumanas, 2));
                                _listValoresBiologicas.Add(Math.Round(_listResps[0].ValorBiologicas, 2));
                                //await DisplayAlert("INFORMAÇÃO", string.Format("Exatas: {0} - Humanas: {1} - Biologicas: {2}", _listValoresExatas[1], _listValoresHumanas[1], _listValoresBiologicas[1]), "OK");
                                break;
                            }
                        case "2":
                            {
                                _listValoresExatas.Add(Math.Round(_listResps[1].ValorExatas, 2));
                                _listValoresHumanas.Add(Math.Round(_listResps[1].ValorHumanas, 2));
                                _listValoresBiologicas.Add(Math.Round(_listResps[1].ValorBiologicas, 2));
                                //await DisplayAlert("INFORMAÇÃO", string.Format("Exatas: {0} - Humanas: {1} - Biologicas: {2}", _listValoresExatas[1], _listValoresHumanas[1], _listValoresBiologicas[1]), "OK");
                                break;
                            }
                        case "3":
                            {
                                _listValoresExatas.Add(Math.Round(_listResps[2].ValorExatas, 2));
                                _listValoresHumanas.Add(Math.Round(_listResps[2].ValorHumanas, 2));
                                _listValoresBiologicas.Add(Math.Round(_listResps[2].ValorBiologicas, 2));
                                //await DisplayAlert("INFORMAÇÃO", string.Format("Exatas: {0} - Humanas: {1} - Biologicas: {2}", _listValoresExatas[1], _listValoresHumanas[1], _listValoresBiologicas[1]), "OK");
                                break;
                            }
                        case "4":
                            {
                                _listValoresExatas.Add(Math.Round(_listResps[3].ValorExatas, 2));
                                _listValoresHumanas.Add(Math.Round(_listResps[3].ValorHumanas, 2));
                                _listValoresBiologicas.Add(Math.Round(_listResps[3].ValorBiologicas, 2));
                                //await DisplayAlert("INFORMAÇÃO", string.Format("Exatas: {0} - Humanas: {1} - Biologicas: {2}", _listValoresExatas[1], _listValoresHumanas[1], _listValoresBiologicas[1]), "OK");
                                break;
                            }
                        default:
                            break;
                    }
                }

                 List<double> verResultado = CalcularResultadosDasRespostas(_listValoresExatas,_listValoresHumanas,_listValoresBiologicas);
                
                await HttpClientResultadoTeste.CalculateCompatibility(user_logado, verResultado[0], verResultado[1], verResultado[2]);
                await Navigation.PushAsync(new PageCalcularResultado(user_logado));
            }
        }
        
    }
}