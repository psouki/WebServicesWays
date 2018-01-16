using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Xml.Linq;
using System.Xml.Serialization;
using JokeRestClient.Const;
using Newtonsoft.Json;

namespace JokeRestClient
{
    public class JokeDataService
    {
        public ICollection<Joke> GetJokes()
        {
            // to comply to the single responsibility principle 
            // we can create this method that create a request that will used in every method
            HttpWebRequest request = CreateRequest("http://localhost:34087/JokeService.svc/jokes", Verb.Get, Format.Json);
            WebResponse response = request.GetResponse();
            ICollection<Joke> result = JsonConvert.DeserializeObject<ICollection<Joke>>(ReadResponse(response));

            return result;
        }

        public Joke GetJokeById(int jokeId)
        {
            // as it was set automaticFormatSelectionEnabled to true
            // who determines what format is the caller
            // here it is specified Xml
            HttpWebRequest request = CreateRequest("http://localhost:34087/JokeService.svc/joke?id=" + jokeId, Verb.Get, Format.Xml);
            var response = request.GetResponse();

            // if the XmlRoot annotation is not in the Joke 
            // we may use the statement below
            var serializer = new XmlSerializer(typeof(Joke));
            Joke result;

            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                // without the annotation the serializer will throw the exception:
                // There is an error in XML document (1,2). It is because of the namespace 
                result = (Joke)serializer.Deserialize(streamReader);
            }

            // if the XmlRoot annotation is not in the Joke 
            // we must use the method below
            //XDocument doc = XDocument.Parse(ReadResponse(response));

            //Joke result = ConvertXMLToJoke(doc);

            return result;
        }

        private Joke ConvertXMLToJoke(XDocument doc)
        {
            // to make it more readable we can extract the xml namespace
            var ns = $"{{{doc.Root.GetDefaultNamespace()}}}";

            // Finding the root
            XElement root = doc.Element($"{ns}Joke");

            return new Joke
            {
                Id = int.Parse(root.Element($"{ns}Id").Value),
                Answer = root.Element($"{ns}Answer")?.Value,
                Grade = int.Parse(root.Element($"{ns}Grade").Value),
                Question = root.Element($"{ns}Question")?.Value,
                Title = root.Element($"{ns}Title")?.Value
            };
        }

        public Joke AddJoke(Joke joke)
        {
            HttpWebRequest request = CreateRequest("http://localhost:34087/JokeService.svc/joke", Verb.Post, Format.Json, joke);
            var response = request.GetResponse();
            Joke result = JsonConvert.DeserializeObject<Joke>(ReadResponse(response));

            return result;
        }

        public Joke UpdateJoke(Joke joke)
        {
            HttpWebRequest request = CreateRequest("http://localhost:34087/JokeService.svc/joke", Verb.Put, Format.Json, joke);
            var response = request.GetResponse();
            Joke result = JsonConvert.DeserializeObject<Joke>(ReadResponse(response));

            return result;
        }

        public void DeleteJoke(int jokeId)
        {
            HttpWebRequest request = CreateRequest("http://localhost:34087/JokeService.svc/joke/" + jokeId, Verb.Delete, Format.Json);
            request.GetResponse();
        }

        // This method returns a web request after serialize an object into Json and put it into the stream
        private HttpWebRequest CreateRequest<T>(string uri, string verb, string format, T entity)
        {
            HttpWebRequest request = CreateRequest(uri, verb, format);
            if (entity == null) return null;

            using (StreamWriter sr = new StreamWriter(request.GetRequestStream()))
            {
                sr.Write(JsonConvert.SerializeObject(entity));
            }

            return request;
        }

        // This method simply creates a request 
        // because GetJokes, GetJokeById and DeleteJoke don't need to pass an object
        private HttpWebRequest CreateRequest(string uri, string verb, string format)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.Accept = format;
            request.ContentType = format;
            request.Method = verb;

            return request;
        }

        internal string ReadResponse(WebResponse response)
        {
            if (response == null) return string.Empty;

            using (var streamReader = new StreamReader(response.GetResponseStream()))
                return streamReader.ReadToEnd();
        }


    }
}
