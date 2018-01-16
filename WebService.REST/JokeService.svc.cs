using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Xml.Serialization;
using JokeRestData;

namespace WebService.REST
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
        // When the enableWebScript is not specified the default response format is xml
        // but if automaticFormatSelectionEnabled is true we transfer this power to the caller 
        [WebGet(UriTemplate = "/jokes", ResponseFormat = WebMessageFormat.Json)]
        public ICollection<Joke> GetJokes()
        {
            return _service.GetJokes();
        }

        //[WebGet(UriTemplate = "/joke/{jokeId}")]
        // to use this more friendly url it has to be string
        // and we have to handle the conversion ourselves 
        //public Joke GetJokeById(string jokeId)
        //{
        //    int id;
        //    int.TryParse(jokeId, out id);
        //    return _service.GetJokeById(id);
        //}

        // We have two choices here:
        // Or use "?" and specify the parameter to be recognized as int
        // Or use more friendly "/" [id value] but it has to be a string
        [WebGet(UriTemplate = "/joke?id={jokeId}")]
        public Joke GetJokeById(int jokeId)
        {
            return _service.GetJokeById(jokeId);
        }

        [WebInvoke(UriTemplate = "/joke")]
        public Joke AddJoke(Joke joke)
        {
            return _service.AddJoke(joke);
        }

        [WebInvoke(Method = "PUT", UriTemplate = "/joke")]
        public Joke UpdateJoke(Joke joke)
        {
            return _service.UpdateJoke(joke);
        }

        // using the more friendly url
        [WebInvoke(Method = "DELETE", UriTemplate = "/joke/{jokeId}")]
        public void DeleteJoke(string jokeId)
        {
            int id;
            int.TryParse(jokeId, out id);
            _service.DeleteJoke(id);
        }
    }
}
