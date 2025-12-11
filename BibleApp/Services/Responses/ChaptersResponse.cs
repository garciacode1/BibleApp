using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BibleApp.Services;


namespace BibleApp.Services.Responses
{
    internal class ChaptersResponse
    {
        [JsonProperty("data")]
        public List<Chapter>? Data { get; set; }
    }
}
