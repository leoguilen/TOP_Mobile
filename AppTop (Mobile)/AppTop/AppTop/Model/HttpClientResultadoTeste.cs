using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AppTop.Model
{
    public static class HttpClientResultadoTeste
    {
        //private readonly static string addressBase = "http://192.168.43.108/"; //REDE TIM
        private readonly static string addressBase = "http://192.168.0.5/"; //REDE CASA

        public static HttpClient Configurar()
        {
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(addressBase); //Endereço da API
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json")); //Identifica que o resultado vai ser um arquivo json    

            return client;
        }

        public static IEnumerable<ResultadoTeste> GetAllTestResult()
        {
            List<ResultadoTeste> _listResultTest = new List<ResultadoTeste>();

            using (HttpClient client = Configurar())
            {
                HttpResponseMessage resp = client.GetAsync("api/detalhesResultado/TodosDetalhesDosTestes").Result; //Aqui faz primeira consulta

                if (resp.IsSuccessStatusCode) //Verifica se a consulta é valida
                {
                    var resposta = client.GetStringAsync("api/detalhesResultado/TodosDetalhesDosTestes").Result;
                    ResultadoTeste[] result = JsonConvert.DeserializeObject<ResultadoTeste[]>(resposta); //Converte o resultado em classes usuario

                    foreach (var itemPerg in result)
                    {
                        _listResultTest.Add(itemPerg);
                    }
                }
            }

            return _listResultTest;
        }

        public static IEnumerable<ResultadoTeste> GetTestResult(int id_teste)
        {
            List<ResultadoTeste> _listResultTest = new List<ResultadoTeste>();

            using (HttpClient client = Configurar())
            {
                HttpResponseMessage resp = client.GetAsync("api/detalhesResultado/DetalhesDosTestesDoUsuario/"+id_teste).Result; //Aqui faz primeira consulta

                if (resp.IsSuccessStatusCode) //Verifica se a consulta é valida
                {
                    var resposta = client.GetStringAsync("api/detalhesResultado/DetalhesDosTestesDoUsuario/"+id_teste).Result;
                    ResultadoTeste result = JsonConvert.DeserializeObject<ResultadoTeste>(resposta); //Converte o resultado em classes usuario

                    _listResultTest.Add(result);
                }
            }

            return _listResultTest;
        }

        public static async Task CalculateCompatibility(string user_logado,double ve,double vh,double vb)
        {
            string content = addressBase + "api/detalhesResultado/CalcularResultadoDaCompatibilidade/" + user_logado + "/" + Math.Round(ve,0) + "/" + Math.Round(vh,0) + "/" + Math.Round(vb,0);
            HttpClient httpClient = Configurar();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, content);
            HttpResponseMessage response = await httpClient.SendAsync(request);
        }
    }
}
