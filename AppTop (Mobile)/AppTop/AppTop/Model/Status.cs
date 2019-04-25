using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppTop.Model
{
    public class Status
    {
        [JsonProperty(PropertyName = "StatusLogin")]
        public string StatusLogin { get; set; }

        [JsonProperty(PropertyName = "FezTeste")]
        public int FezTeste { get; set; }

        public Status(string sts)
        {
            StatusLogin = sts;
        }

    }
}
