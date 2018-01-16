using System;
using System.Runtime.Serialization;

namespace JokeRestData
{
    [Serializable]
    [DataContract]
    public class Joke
    {
        [DataMember]
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
