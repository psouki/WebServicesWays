using System.Xml;
using System.Xml.Serialization;

namespace JokeRestClient
{
    [XmlRoot(Namespace = "http://schemas.datacontract.org/2004/07/JokeRestData")]
    public class Joke
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public int Grade { get; set; }
    }
}
