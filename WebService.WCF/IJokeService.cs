using JokeWcfData;
using System.Collections.Generic;
using System.ServiceModel;

namespace WebService.WCF
{
    //This tells that the service has to obey this contract. 
    //In order to be recognized it needs this annotation.
    [ServiceContract]
    public interface IJokeService
    {
        //In order to be available the method must indicate explicitly that is an operation of the service contract 
        [OperationContract]
        ICollection<Joke> GetJokes();

        [OperationContract]
        Joke GetJokeById(int jokeId);

        [OperationContract]
        void UpdateJoke(Joke joke);

        [OperationContract]
        void AddJoke(Joke joke);

        [OperationContract]
        void DeleteJoke(int jokeId);
    }
}
