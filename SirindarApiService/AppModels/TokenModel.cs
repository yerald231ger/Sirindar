using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SirindarApiService.AppModels
{
    sealed internal class TokenModel
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public int expires_in { get; set; }
        public string userName { get; set; }
        [JsonProperty(".expires")]
        public string _expires { get; set; }
        [JsonProperty(".issued")]
        public string _issued { get; set; }
    }
}
