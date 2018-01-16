using System.Collections.Generic;
using JokeWcfClient.JokeService;

namespace JokeWcfClient
{
    public class JokeDataService
    {
        private readonly JokeServiceClient _serviceClient;

        public JokeDataService()
        {
            _serviceClient = new JokeServiceClient();
        }

        public ICollection<Joke> GetJokes()
        {
            return _serviceClient.GetJokes();
        }

        public Joke GetJokeById(int jokeId)
        {
            return _serviceClient.GetJokeById(jokeId);
        }

        public void AddJoke(Joke joke)
        {
            _serviceClient.AddJoke(joke);
        }

        public void UpdateJoke(Joke joke)
        {
            _serviceClient.UpdateJoke(joke);
        }

        public void DeleteJoke(int jokeId)
        {
            _serviceClient.DeleteJoke(jokeId);
        }
    }
}
