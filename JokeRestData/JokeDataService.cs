using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Hosting;
using Newtonsoft.Json;

namespace JokeRestData
{
    public class JokeDataService
    {
        public string SavePath = HostingEnvironment.MapPath("~/jokeData.json");
        public ICollection<Joke> Jokes { get; set; }

        public JokeDataService()
        {
            if (File.Exists(SavePath))
            {
                using (StreamReader sr = new StreamReader(SavePath))
                {
                    Jokes = JsonConvert.DeserializeObject<ICollection<Joke>>(sr.ReadToEnd());
                }
            }
            else
            {
                SeedJoke();
                Save();
            }
        }

        public ICollection<Joke> GetJokes()
        {
            return Jokes;
        }

        public Joke GetJokeById(int jokeId)
        {
            return Jokes.FirstOrDefault(j => j.Id == jokeId);
        }

        private void SeedJoke()
        {
            Jokes = new List<Joke>();
            Joke joke1 = new Joke
            {
                Id = 1,
                Title = "Bar 1",
                Question = "An amnesiac walks into a bar",
                Answer = "He goes up to a beautiful blond and says: So, do I come here often?",
                Grade = 2
            };

            Joke joke2 = new Joke
            {
                Id = 2,
                Title = "Bar 2",
                Question = "A neutron walks into a bar.",
                Answer = "How much for a beer? the neutron asks. For you no charge",
                Grade = 3
            };
            Jokes.Add(joke1);
            Jokes.Add(joke2);
        }

        private void Save()
        {
            JsonSerializer serializer = new JsonSerializer
            {
                NullValueHandling = NullValueHandling.Ignore,
                Formatting = Formatting.Indented
            };

            using (StreamWriter sw = new StreamWriter(SavePath))
            {
                using (JsonWriter jw = new JsonTextWriter(sw))
                {
                    serializer.Serialize(jw, Jokes);
                }
            }
        }

        public Joke UpdateJoke(Joke joke)
        {
            Joke foundJoke = Jokes.FirstOrDefault(j => j.Id == joke.Id);
            if (foundJoke == null) return null;
            Jokes.Remove(foundJoke);
            Jokes.Add(joke);
            Save();
            return joke;
        }

        public Joke AddJoke(Joke joke)
        {
            int id = Jokes.Max(j => j.Id);
            joke.Id = id + 1;
            Jokes.Add(joke);
            Save();
            return joke;
        }

        public void DeleteJoke(int jokeId)
        {
            Joke foundJoke = Jokes.FirstOrDefault(j => j.Id == jokeId);
            if (foundJoke == null) return;
            Jokes.Remove(foundJoke);
            Save();
        }
    }
}
