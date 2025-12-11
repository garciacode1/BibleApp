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
using System.Net.Http;

namespace BibleApp.Services
{
    internal class APIService
    {
        const string apiKey = "47455fdb12c865888b79736cf85c5515";
        const string baseURL = "https://api.scripture.api.bible/v1/bibles";
        const string defaultBibleId = "de4e12af7f28f599-01"; //King James (Authorised) Version

        public APIService() { }


        //Get Books Method

        public async Task<List<Books>> GetBooks()
        {

            try
            {
                //Get Http Client
                HttpClient client = new HttpClient();
                string url = $"{baseURL}/{defaultBibleId}/books";
                //Form a request
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url);
                //Headers
                request.Headers.Add("Accept", "application/json");
                request.Headers.Add("api-key", apiKey);
                //send request 
                HttpResponseMessage response = await client.SendAsync(request);
                //error checking
                if (response.StatusCode != System.Net.HttpStatusCode.OK || response.Content == null)
                {
                    await App.Current.MainPage.DisplayAlert("Error", $"Status code: {response.StatusCode}", "OK");
                    return new List<Books>();
                }
                //read json
                string json = await response.Content.ReadAsStringAsync();
                //convert json into c# object
                BooksResponse? result = JsonConvert.DeserializeObject<BooksResponse>(json);
                if (result != null && result.Data != null)
                {
                    return result.Data;
                }

                return new List<Books>();
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Exception", ex.Message, "OK");
                return new List<Books>(); //Prevent crashing
            }

        }

        //Get chapters method

        public async Task<List<Chapter>> GetChapters(string bookId)
        {
            try
            {
                HttpClient client = new HttpClient();

                string url = $"{baseURL}/{defaultBibleId}/books/{bookId}/chapters";
                // request with headers
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url);
                request.Headers.Add("Accept", "application/json");
                request.Headers.Add("api-key", apiKey);

                HttpResponseMessage response = await client.SendAsync(request);
                //error checking
                if (response.StatusCode != System.Net.HttpStatusCode.OK ||
                    response.Content == null)
                {
                    await App.Current.MainPage.DisplayAlert("Error",$"Status code: {response.StatusCode}","OK");

                    return new List<Chapter>();
                }

                //Read the JSON
                string json = await response.Content.ReadAsStringAsync();

                //Convert JSON into C# object
                ChaptersResponse? result = JsonConvert.DeserializeObject<ChaptersResponse>(json);

                //Always return a safe list
                if (result != null && result.Data != null)
                {
                    return result.Data;
                }

                return new List<Chapter>();
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Exception", ex.Message, "OK");
                return new List<Chapter>();
            }
        }

        //Get content method

        public async Task<ChapterContent> GetChapterText(string chapterId)
        {
            try
            {
                HttpClient client = new HttpClient();
                string url = $"{baseURL}/{defaultBibleId}/chapters/{chapterId}";

                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url);
                request.Headers.Add("Accept", "application/json");
                request.Headers.Add("api-key", apiKey);

                HttpResponseMessage response = await client.SendAsync(request);

                if (!response.IsSuccessStatusCode)
                {
                    await App.Current.MainPage.DisplayAlert("Error", "Could not load chapter text", "OK");
                    return null;
                }

                string json = await response.Content.ReadAsStringAsync();

                var result = JsonConvert.DeserializeObject<ChapterContentResponse>(json);

                return result?.Data;
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Exception", ex.Message, "OK");
                return null;
            }



        } 





    }
}   
