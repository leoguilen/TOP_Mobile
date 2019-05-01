using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace AppTop.Model
{
    public static class HttpClientTeste
    {
        //private readonly static string addressBase = "http://192.168.43.108/"; REDE TIM
        private readonly static string addressBase = "http://192.168.0.5/"; //REDE CASA

        public static HttpClient Configurar()
        {
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(addressBase); //Endereço da API
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json")); //Identifica que o resultado vai ser um arquivo json    

            return client;
        }

        public static IEnumerable<Teste> GetAllTest()
        {
            List<Teste> _listTeste = new List<Teste>();

            using (HttpClient client = Configurar())
            {
                HttpResponseMessage resp = client.GetAsync("api/teste/TodosTestes").Result; //Aqui faz primeira consulta

                if (resp.IsSuccessStatusCode) //Verifica se a consulta é valida
                {
                    var resposta = client.GetStringAsync("api/teste/TodosTestes").Result;
                    Teste[] pe = JsonConvert.DeserializeObject<Teste[]>(resposta); //Converte o resultado em classes usuario

                    foreach (var itemPerg in pe)
                    {
                        _listTeste.Add(itemPerg);
                    }

                }
            }

            return _listTeste;
        }

        public static IEnumerable<Teste> GetTest(int id_user)
        {
            List<Teste> _listTeste = new List<Teste>();

            using (HttpClient client = Configurar())
            {
                HttpResponseMessage resp = client.GetAsync("api/teste/PegarTestesDoUsuario/"+id_user).Result; //Aqui faz primeira consulta

                if (resp.IsSuccessStatusCode) //Verifica se a consulta é valida
                {
                    var resposta = client.GetStringAsync("api/teste/PegarTestesDoUsuario/"+id_user).Result;
                    Teste[] pe = JsonConvert.DeserializeObject<Teste[]>(resposta); //Converte o resultado em classes usuario

                    foreach (var itemPerg in pe)
                    {
                        _listTeste.Add(itemPerg);
                    }

                }
            }

            return _listTeste;
        }

        public static async void StartNewTest(string user_logado)
        {
            string content = addressBase + "api/teste/IniciarNovoTeste/" + HttpClientUsuario.ReturnUserId(user_logado);
            HttpClient httpClient = Configurar();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, content);
            HttpResponseMessage response = await httpClient.SendAsync(request);
        }

        public static async void CancelTest(string user_logado)
        {
            string content = addressBase + "api/teste/CancelarTeste/" + HttpClientUsuario.ReturnUserId(user_logado);
            HttpClient httpClient = Configurar();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, content);
            HttpResponseMessage response = await httpClient.SendAsync(request);
        }
    }
}
