using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppTop.Model
{
    public class Usuario
    {
        [JsonProperty(PropertyName = "ID")]
        public int ID { get; set; }

        [JsonProperty(PropertyName = "Nome")]
        public string Nome { get; set; }

        [JsonProperty(PropertyName = "Sexo")]
        public string Sexo { get; set; }

        [JsonProperty(PropertyName = "DataNascimento")]
        public string DataNascimento { get; set; }

        [JsonProperty(PropertyName = "Username")]
        public string Username { get; set; }

        [JsonProperty(PropertyName = "Senha")]
        public string Senha { get; set; }

        [JsonProperty(PropertyName = "Uf")]
        public string Uf { get; set; }

        [JsonProperty(PropertyName = "Cidade")]
        public string Cidade { get; set; }

        [JsonProperty(PropertyName = "NivelAcademico")]
        public int NivelAcademico { get; set; }

        [JsonProperty(PropertyName = "DataCadastro")]
        public DateTime DataCadastro { get; set; }

        [JsonProperty(PropertyName = "FezTeste")]
        public byte FezTeste { get; set; }

        [JsonProperty(PropertyName = "Avatar")]
        public string Avatar { get; set; }

        [JsonProperty(PropertyName = "Bio")]
        public string Bio { get; set; }

    }
}
