using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BibleApp.Services;


namespace BibleApp.Services.Responses
{
    public class BooksResponse
    {
        [JsonProperty("data")]
        public List<Books>? Data { get; set; }
    }
}
