using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using JokeAjaxData;

namespace WebService.Ajax
{
    [ServiceContract(Namespace = "")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class JokeService
    {
        private readonly JokeDataService _service;
        public JokeService()
        {
            _service = new JokeDataService();
        }

        // To receive the response in the xml format, 
        // instead of the default json format
        //[WebGet(ResponseFormat = WebMessageFormat.Xml)]
        [WebGet]
        public ICollection<Joke> GetJokes()
        {
            return _service.GetJokes();
        }

        [WebGet]
        public Joke GetJokeById(int jokeId)
        {
            return _service.GetJokeById(jokeId);
        }

        [WebInvoke]
        public Joke AddJoke(Joke joke)
        {
            return _service.AddJoke(joke);
        }

        [WebInvoke]
        public Joke UpdateJoke(Joke joke)
        {
            return _service.UpdateJoke(joke);
        }

        [WebInvoke]
        public void DeleteJoke(int jokeId)
        {
            _service.DeleteJoke(jokeId);
        } 
    }
}
