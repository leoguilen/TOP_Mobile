using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppTop.Model
{
    public class Teste
    {
        [JsonProperty(PropertyName = "IdTeste")]
        public int IdTeste { get; set; }

        [JsonProperty(PropertyName = "IdUsuario")]
        public int IdUsuario { get; set; }

        [JsonProperty(PropertyName = "DataInicio")]
        public DateTime DataInicio { get; set; }

        [JsonProperty(PropertyName = "DataFinal")]
        public DateTime? DataFinal { get; set; }

        [JsonProperty(PropertyName = "NovoTeste")]
        public byte NovoTeste { get; set; }



    }
}
