using System;
using System.Runtime.Serialization;

namespace JokeWcfData
{
    [Serializable]
    [DataContract] // tells the service what returns 
    public class Joke
    {
        [DataMember] // gives better information about the member name
        public int Id { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string Question { get; set; }

        [DataMember]
        public string Answer { get; set; }

        [DataMember]
        public int Grade { get; set; }
    }
}
