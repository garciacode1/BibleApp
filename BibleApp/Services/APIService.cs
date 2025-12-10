using BibleApp.Services;
using BibleApp.Services.Responses;
using Newtonsoft.Json;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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



        //get books method
        public async Task<List<Books>> GetBooks()   
        {

            try
            {
                
                HttpClient client = new HttpClient();
                string url = $"{baseURL}/{defaultBibleId}/books";
                
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url);
                request.Headers.Add("Accept", "application/json");
                request.Headers.Add("api-key", apiKey);
              
                HttpResponseMessage response = await client.SendAsync(request);
                if (response.StatusCode != System.Net.HttpStatusCode.OK ||
                    response.Content == null)
                {
                    await App.Current.MainPage.DisplayAlert(
                        "Error",
                        $"Status code: {response.StatusCode}",
                        "OK");
                    return new List<Books>();
                }
               
                string json = await response.Content.ReadAsStringAsync();
             
                BooksResponse? result = JsonConvert.DeserializeObject<BooksResponse>(json);
                if (result != null && result.Data != null)
                {
                    return result.Data;
                }

                return new List<Books>();
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Exception",ex.Message,"OK");
                return new List<Books>(); //Prevent crashing
            }

        }
        
        //Get chapters method

        public async Task<List<string>> GetChapters(string bookId)
        {
            HttpClient client = new HttpClient();

            string url = $"{baseURL}/{defaultBibleId}/books/{bookId}/chapters";

            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("api-key", apiKey);

            //send request 
            var response = await client.SendAsync(request);
            string json = await response.Content.ReadAsStringAsync();

            var jsonObject = JsonConvert.DeserializeObject<dynamic>(json);
            var chapters = new List<string>();

            foreach (var item in jsonObject.data)
            {
                chapters.Add((string)item.id);
            }

            
            return chapters;
        }







    }
}   
