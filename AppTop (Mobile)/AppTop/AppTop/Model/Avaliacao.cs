using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppTop.Model
{
    public class Avaliacao
    {
        [JsonProperty(PropertyName = "UserID")]
        public int UserID { get; set; }

        [JsonProperty(PropertyName = "QtdeRatings")]
        public int QtdeRatings { get; set; }

        [JsonProperty(PropertyName = "Comentario")]
        public string Comentario { get; set; }


    }
}
