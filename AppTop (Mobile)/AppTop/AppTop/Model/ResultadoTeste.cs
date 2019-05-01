using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppTop.Model
{
    public class ResultadoTeste
    {
        [JsonProperty(PropertyName = "IdUsuario")]
        public int IdUsuario { get; set; }

        [JsonProperty(PropertyName = "IdTeste")]
        public int IdTeste { get; set; }

        [JsonProperty(PropertyName = "IdCurso")]
        public int IdCurso { get; set; }

        [JsonProperty(PropertyName = "PontosExatas")]
        public double PontosExatas { get; set; }

        [JsonProperty(PropertyName = "PontosHumanas")]
        public double PontosHumanas { get; set; }

        [JsonProperty(PropertyName = "PontosBiologicas")]
        public double PontosBiologicas { get; set; }


    }
}
