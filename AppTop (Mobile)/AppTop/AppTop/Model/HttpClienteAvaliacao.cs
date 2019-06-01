using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace AppTop.Model
{
    public static class HttpClienteAvaliacao
    {
        private static string addressBase = App.Current.Resources["IPAddress"].ToString();

        public static HttpClient Configurar()
        {
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(addressBase); //Endereço da API
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json")); //Identifica que o resultado vai ser um arquivo json    

            return client;
        }

        public static void InsertEvaluation(int idUser,int numRating,string comentario)
        {

        }
    }
}
