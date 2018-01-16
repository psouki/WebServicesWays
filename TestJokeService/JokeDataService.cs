using TestJokeService.JokeService;

namespace TestJokeService
{
    public class JokeDataService
    {
        // deprecated way to construct web service
        // is good to know just for legacy application understanding 
        private readonly JokeRememberServiceSoapClient _client;

        public JokeDataService()
        {
            _client = new JokeRememberServiceSoapClient();
        }

        public Joke[] GetJokes()
        {
            return _client.GetJokes();
        }

        public Joke GetJokeById(int jokeId)
        {
            return _client.GetJokeById(jokeId);
        }

        public void AddJoke(Joke joke)
        {
            _client.AddJoke(joke);
        }

        public void UpdateJoke(Joke joke)
        {
            _client.UpdateJoke(joke);
        }

        public void DeleteJoke(int jokeId)
        {
            _client.DeleteJoke(jokeId);
        }
    }
}
