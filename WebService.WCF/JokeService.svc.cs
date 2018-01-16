using System;
using System.Collections.Generic;
using JokeWcfData;

namespace WebService.WCF
{
    // Just need to implement the contract and it is go to go.
    // It is a good practice to use this class just to call the real method.
    // This class should encapsulate the real work.
    public class JokeService : IJokeService
    {
        private readonly JokeDataService _service;

        public JokeService()
        {
            _service = new JokeDataService();
        }

        public ICollection<Joke> GetJokes()
        {
            return _service.GetJokes(); // real work: calling the get objects service
        }

        public Joke GetJokeById(int jokeId)
        {
            return _service.GetJokeById(jokeId);
        }

        public void UpdateJoke(Joke joke)
        {
            _service.UpdateJoke(joke);
        }

        public void AddJoke(Joke joke)
        {
            _service.AddJoke(joke);
        }

        public void DeleteJoke(int jokeId)
        {
            _service.DeleteJoke(jokeId);
        }
    }
}
