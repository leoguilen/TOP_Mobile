using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppTop.Model
{
    public class Resultado
    {
        [JsonProperty(PropertyName = "IdUsuario")]
        public int IdUsuario { get; set; }

        [JsonProperty(PropertyName = "Username")]
        public string Username { get; set; }

        [JsonProperty(PropertyName = "IdTeste")]
        public int IdTeste { get; set; }

        [JsonProperty(PropertyName = "DataInicio")]
        public DateTime DataInicio { get; set; }

        [JsonProperty(PropertyName = "DataFim")]
        public DateTime? DataFim { get; set; }

        [JsonProperty(PropertyName = "TempoConclusao")]
        public int TempoConclusao { get; set; }

        [JsonProperty(PropertyName = "ResultadoExatas")]
        public double ResultadoExatas { get; set; }

        [JsonProperty(PropertyName = "ResultadoHumanas")]
        public double ResultadoHumanas { get; set; }

        [JsonProperty(PropertyName = "ResultadoBiologicas")]
        public double ResultadoBiologicas { get; set; }

        [JsonProperty(PropertyName = "NomeCurso")]
        public string NomeCurso { get; set; }

        [JsonProperty(PropertyName = "TipoCurso")]
        public string TipoCurso { get; set; }

        [JsonProperty(PropertyName = "DuracaoCurso")]
        public string DuracaoCurso { get; set; }

        [JsonProperty(PropertyName = "DescArea")]
        public string DescArea { get; set; }

        [JsonProperty(PropertyName = "ImagemCurso")]
        public string ImagemCurso { get; set; }

        [JsonProperty(PropertyName = "NomeFaculdade")]
        public string NomeFaculdade { get; set; }

        [JsonProperty(PropertyName = "EstadoFaculdade")]
        public string EstadoFaculdade { get; set; }

        [JsonProperty(PropertyName = "SiteFaculdade")]
        public string SiteFaculdade { get; set; }

        [JsonProperty(PropertyName = "NotaMEC")]
        public int NotaMEC { get; set; }

        [JsonProperty(PropertyName = "NovoTeste")]
        public byte NovoTeste { get; set; }

        public bool Isvisible { get; set; }

        public bool IsvisibleDown { get; set; }

        public bool IsvisibleUp { get; set; }
    }
}
