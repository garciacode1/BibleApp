using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using BibleApp.Services;

namespace BibleApp.Services.Responses
{
    public class ChapterContentResponse
    {
        [JsonProperty("data")]
        public ChapterContent Data { get; set; }
        
    }

    public class ChapterContent 
    {
        [JsonProperty ("id")]
        public string Id { get; set; }

        [JsonProperty ("reference")]
        public string Reference { get; set; }
        //content contains verses
        [JsonProperty ("content")]
        public string Content { get; set; }
     
    
    
    }

}   
