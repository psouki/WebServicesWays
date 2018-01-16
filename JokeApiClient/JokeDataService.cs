using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using JokeApiClient.Helpers;
using JokeRestClient.Const;
using Newtonsoft.Json;

namespace JokeApiClient
{
    public class JokeDataService
    {
        private readonly HttpClient _client;

        public JokeDataService()
        {
            // Simple method to encapsulate the http client creation.
            _client = HttpClientHelper.GetClient();
        }

        public async Task<ICollection<Joke>> GetJokes()
        {
            HttpResponseMessage response = await _client.GetAsync("joke");
            if (!response.IsSuccessStatusCode) return null;
            string content = await response.Content.ReadAsStringAsync();
            ICollection<Joke> result = JsonConvert.DeserializeObject<ICollection<Joke>>(content);
            return result;
        }

        public async Task<Joke> GetJokeById(int id)
        {
            // the framework will concatenate the base address with path provided
            HttpResponseMessage response = await _client.GetAsync($"joke/{id}");

            if (!response.IsSuccessStatusCode) return null;

            string content = await response.Content.ReadAsStringAsync();
            Joke result = JsonConvert.DeserializeObject<Joke>(content);
            return result;
        }

        public async Task<Joke> AddJoke(Joke joke)
        {
            string data = JsonConvert.SerializeObject(joke);
            StringContent content = new StringContent(data, Encoding.Unicode, Format.Json);
            HttpResponseMessage response = await _client.PostAsync("joke", content);

            if (!response.IsSuccessStatusCode) return null;

            string created = await response.Content.ReadAsStringAsync();
            Joke result = JsonConvert.DeserializeObject<Joke>(created);
            return result;
        }

        public async Task<Joke> UpdateJoke(Joke joke)
        {
            string data = JsonConvert.SerializeObject(joke);
            StringContent content = new StringContent(data, Encoding.Unicode, Format.Json);
            HttpResponseMessage response = await _client.PutAsync("joke", content);

            if (!response.IsSuccessStatusCode) return null;

            string created = await response.Content.ReadAsStringAsync();
            Joke result = JsonConvert.DeserializeObject<Joke>(created);
            return result;
        }

        public async void DeleteJoke(int id)
        {
            HttpResponseMessage response = await _client.DeleteAsync($"joke/{id}");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("An error has occurred");
            };

        }
    }
}
