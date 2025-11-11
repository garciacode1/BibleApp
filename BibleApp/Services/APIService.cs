using BibleApp.Services;
using BibleApp.Services.Responses;
using Newtonsoft.Json;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace BibleApp.Services
{
    internal class APIService
    {
        const string apiKey = "47455fdb12c865888b79736cf85c5515";
        const string baseURL = "https://api.scripture.api.bible/v1/bibles";
        const string defaultBibleId = "de4e12af7f28f599-01"; // King James (Authorised) Version

        public APIService() { }

        public async Task<List<Books>> GetBooks()
        {

            //Get an HTTP Client

            HttpClient client = new HttpClient();

            //full URL

            string url = $"{baseURL}/{defaultBibleId}/books";

            //Form a request 
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("api-key", apiKey);

            var response = await client.SendAsync(request);
            string json = await response.Content.ReadAsStringAsync();

            var booksResponse = JsonConvert.DeserializeObject<BooksResponse>(json);

            return booksResponse?.Data ?? new List<Books>();









        }







    }
}   
