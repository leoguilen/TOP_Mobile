﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace AppTop.Model
{
    public static class HttpClientResultado
    {
        //private readonly static string addressBase = "http://192.168.43.108/"; //REDE TIM
        //private readonly static string addressBase = "http://192.168.88.239/"; //REDE AUDITORIO
        //private readonly static string addressBase = "http://192.168.0.4/"; //REDE CASA

        private static string addressBase = App.Current.Resources["IPAddress"].ToString();

        public static HttpClient Configurar()
        {
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(addressBase); //Endereço da API
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json")); //Identifica que o resultado vai ser um arquivo json    

            return client;
        }

        public static IEnumerable<Resultado> GetAllResults()
        {
            List<Resultado> _listResult = new List<Resultado>();

            using (HttpClient client = Configurar())
            {
                HttpResponseMessage resp = client.GetAsync("/api/resultado/TodosResultados").Result; //Aqui faz primeira consulta

                if (resp.IsSuccessStatusCode) //Verifica se a consulta é valida
                {
                    var resposta = client.GetStringAsync("/api/resultado/TodosResultados").Result;
                    Resultado[] re = JsonConvert.DeserializeObject<Resultado[]>(resposta); //Converte o resultado em classes usuario

                    foreach (var itemResp in re)
                    {
                        _listResult.Add(itemResp);
                    }

                }
            }

            return _listResult;
        }

        public static IEnumerable<Resultado> GetResult(string username)
        {
            List<Resultado> _listResult = new List<Resultado>();

            using (HttpClient client = Configurar())
            {
                HttpResponseMessage resp = client.GetAsync("/api/resultado/TrazerResultado/"+username).Result; //Aqui faz primeira consulta

                if (resp.IsSuccessStatusCode) //Verifica se a consulta é valida
                {
                    var resposta = client.GetStringAsync("/api/resultado/TrazerResultado/"+username).Result;
                    Resultado[] re = JsonConvert.DeserializeObject<Resultado[]>(resposta); //Converte o resultado em classes usuario

                    foreach (var itemResp in re)
                    {
                        _listResult.Add(itemResp);
                    }

                }
            }

            return _listResult;
        }
    }
}
