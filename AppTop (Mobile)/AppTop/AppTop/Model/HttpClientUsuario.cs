using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AppTop.Model
{
    public class HttpClientUsuario
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

        public static IEnumerable<Usuario> GetAllUsers()
        {
            List<Usuario> _listUsers = new List<Usuario>();

            using (var client = Configurar())
            {
                HttpResponseMessage resp = client.GetAsync("api/usuario/TodosUsuario").Result; //Aqui faz primeira consulta

                if (resp.IsSuccessStatusCode) //Verifica se a consulta é valida
                {
                    var resposta = client.GetStringAsync("api/usuario/TodosUsuario").Result;
                    Usuario[] us = JsonConvert.DeserializeObject<Usuario[]>(resposta); //Converte o resultado em classes usuario

                    foreach (var users in us)
                    {
                        _listUsers.Add(users);
                    }
                }
            }

            return _listUsers;
        }

        public static bool LoginValidate(string user,string pwd,out string erro)
        {
            bool result = false;

            HttpClient client = Configurar();

            HttpResponseMessage resp = client.GetAsync("api/usuario/ValidarLogin/" + user + "/" + pwd).Result;
            if (resp.IsSuccessStatusCode)
            {
                var resposta = client.GetStringAsync("api/usuario/ValidarLogin/" + user + "/" + pwd).Result;
                Status sts = JsonConvert.DeserializeObject<Status>(resposta);
                erro = "";

                if (sts.StatusLogin.Equals("Valido"))
                    result = true;
                else
                    result = false;
            }
            else
            {
                erro = "Servidor IIS desconectado";
            }

            return result;
        }

        public static int CheckTest(string user)
        {
            int status = 0;

            HttpClient client = Configurar();

            HttpResponseMessage resp = client.GetAsync("api/usuario/FezTeste/"+user).Result;
            if (resp.IsSuccessStatusCode)
            {
                var resposta = client.GetStringAsync("api/usuario/FezTeste/"+user).Result;
                Status sts = JsonConvert.DeserializeObject<Status>(resposta);

                if (sts.FezTeste == 1)
                    status = 1;
                else
                    status = 0;
            }

            return status;
        }

        public static async Task<bool> NewUser(Usuario u,string email,string cel)
        { 
            string content = addressBase + "api/usuario/NovoUsuario?nome="+u.Nome+"&sexo="+u.Sexo+"&date="+u.DataNascimento+"&user="+u.Username+"&senha="+u.Senha+"&uf="+u.Uf+"&cidade="+u.Cidade+"&nivel="+u.NivelAcademico+"&email="+email+"&cel="+cel;
            HttpClient httpClient = Configurar();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, content);
            HttpResponseMessage response = await httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
                return true;
            else
                return false;
        }

        public static int ReturnUserId(string user_logado)
        {
            int id_user = 0;

            using (HttpClient client = Configurar())
            {
                HttpResponseMessage resp = client.GetAsync("api/usuario/PegarIDUsuario/"+user_logado).Result; //Aqui faz primeira consulta

                if (resp.IsSuccessStatusCode) //Verifica se a consulta é valida
                {
                    var resposta = client.GetStringAsync("api/usuario/PegarIDUsuario/"+user_logado).Result;
                    Usuario us = JsonConvert.DeserializeObject<Usuario>(resposta); //Converte o resultado em classes usuario
                    id_user = us.ID;
                }
            }

            return id_user;
        }


    }
}
