using Newtonsoft.Json;

namespace AppTop.Model
{
    public class Resposta
    {
        [JsonProperty(PropertyName = "IdResposta")]
        public int IdResposta { get; set; }

        [JsonProperty(PropertyName = "IdPergunta")]
        public int IdPergunta { get; set; }

        [JsonProperty(PropertyName = "DescResposta")]
        public string DescResposta { get; set; }

        [JsonProperty(PropertyName = "ValorExatas")]
        public double ValorExatas { get; set; }

        [JsonProperty(PropertyName = "ValorHumanas")]
        public double ValorHumanas { get; set; }

        [JsonProperty(PropertyName = "ValorBiologicas")]
        public double ValorBiologicas { get; set; }


    }
}