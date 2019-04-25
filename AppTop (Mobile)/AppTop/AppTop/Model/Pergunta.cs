using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppTop.Model
{
    public class Pergunta
    {
        [JsonProperty(PropertyName = "IdPergunta")]
        public int IdPergunta { get; set; }

        [JsonProperty(PropertyName = "DescPergunta")]
        public string DescPergunta { get; set; }
        
    }
}
