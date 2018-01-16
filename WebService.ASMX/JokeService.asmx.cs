using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using JokeData;

namespace WebService.ASMX
{
    // This way of building web service is depreciated by Microsoft. Use exclusively to maintain old web service versions. Do not use it to build new ones  
    // It has lots of problems already resolved in the WCF way of constructing web services.
    //Every time possible migrate them to WCF or Web Api.
      
    [WebService(Namespace = "http://jokes.org/", Name = "Joke Remember Service", Description = "This helps you remember some jokes to have a small talk")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class JokeService : System.Web.Services.WebService
    {
        private  readonly JokeDataService _service;
        public JokeService()
        {
            _service = new JokeDataService();
        }

        [WebMethod]
        public Joke[] GetJokes()
        {
            return _service.GetJokes();
        }

        [WebMethod]
        public Joke GetJokeById(int jokeId)
        {
            return _service.GetJokeById(jokeId);
        }

        [WebMethod]
        public void AddJoke(Joke joke)
        {
            _service.AddJoke(joke);
        }

        [WebMethod]
        public void UpdateJoke(Joke joke)
        {
            _service.UpdateJoke(joke);
        }

        [WebMethod]
        public void DeleteJoke(int jokeId)
        {
            _service.DeleteJoke(jokeId);
        }
    }
}
