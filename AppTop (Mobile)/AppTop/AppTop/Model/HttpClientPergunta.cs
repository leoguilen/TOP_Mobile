using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace AppTop.Model
{
    public static class HttpClientPergunta
    {
        private readonly static string addressBase = "http://192.168.0.5/";

        public static HttpClient Configurar()
        {
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(addressBase); //Endereço da API
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json")); //Identifica que o resultado vai ser um arquivo json    

            return client;
        }

        public static IEnumerable<Pergunta> GetAllQuestions()
        {
            List<Pergunta> _listPerg = new List<Pergunta>();

            using (HttpClient client = Configurar())
            {
                HttpResponseMessage resp = client.GetAsync("api/questao/TodasPerguntas").Result; //Aqui faz primeira consulta

                if (resp.IsSuccessStatusCode) //Verifica se a consulta é valida
                {
                    var resposta = client.GetStringAsync("api/questao/TodasPerguntas").Result;
                    Pergunta[] pe = JsonConvert.DeserializeObject<Pergunta[]>(resposta); //Converte o resultado em classes usuario

                    foreach (var itemPerg in pe)
                    {
                        _listPerg.Add(itemPerg);
                    }

                }
            }

            return _listPerg;
        }
        
        public static Pergunta GetQuestion()
        {
            Pergunta perg = null;

            using (HttpClient client = Configurar())
            {
                HttpResponseMessage resp = client.GetAsync("api/questao/GerarPergunta").Result; //Aqui faz primeira consulta

                if (resp.IsSuccessStatusCode) //Verifica se a consulta é valida
                {
                    var resposta = client.GetStringAsync("api/questao/GerarPergunta").Result;
                    Pergunta pe = JsonConvert.DeserializeObject<Pergunta>(resposta); //Converte o resultado em classes usuario
                    perg = pe;
                }
            }

            return perg;
        }

        public static IEnumerable<Resposta> GetAllAnswers()
        {
            List<Resposta> _listResp = new List<Resposta>();

            using (HttpClient client = Configurar())
            {
                HttpResponseMessage resp = client.GetAsync("api/questao/TodasRespostas").Result; //Aqui faz primeira consulta

                if (resp.IsSuccessStatusCode) //Verifica se a consulta é valida
                {
                    var resposta = client.GetStringAsync("api/questao/TodasRespostas").Result;
                    Resposta[] res = JsonConvert.DeserializeObject<Resposta[]>(resposta); //Converte o resultado em classes usuario

                    foreach (var itemPerg in res)
                    {
                        _listResp.Add(itemPerg);
                    }

                }
            }
            return _listResp;
        }

        public static IEnumerable<Resposta> GetAnswersForQuestion(int id_perg)
        {
            List<Resposta> _listResp = new List<Resposta>();

            using (HttpClient client = Configurar())
            {
                HttpResponseMessage resp = client.GetAsync("api/questao/PegarRespostaDaPergunta/"+id_perg).Result; //Aqui faz primeira consulta

                if (resp.IsSuccessStatusCode) //Verifica se a consulta é valida
                {
                    var resposta = client.GetStringAsync("api/questao/PegarRespostaDaPergunta/" + id_perg).Result;
                    Resposta[] res = JsonConvert.DeserializeObject<Resposta[]>(resposta); //Converte o resultado em classes usuario

                    foreach (var itemPerg in res)
                    {
                        _listResp.Add(itemPerg);
                    }

                }
            }
            return _listResp;
        }
    }
}
