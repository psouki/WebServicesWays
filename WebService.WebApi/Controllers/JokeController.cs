using System.Collections.Generic;
using System.Web.Http;
using JokeApiData;

namespace WebService.WebApi.Controllers
{
    public class JokeController : ApiController
    {
        private readonly JokeDataService _service;

        public JokeController()
        {
            _service = new JokeDataService();
        }

        public IEnumerable<Joke> Get()
        {
            return _service.GetJokes();
        }

        public Joke GetJoke(int id)
        {
            return _service.GetJokeById(id);
        }

        public Joke Post(Joke joke)
        {
            Joke result = _service.AddJoke(joke);
            return result;
        }

        public Joke Put(Joke joke)
        {
            Joke result = _service.UpdateJoke(joke);
            return result;
        }

        public void Delete(int id)
        {
            _service.DeleteJoke(id);
        }
    }
}
