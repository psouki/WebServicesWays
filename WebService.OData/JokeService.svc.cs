//------------------------------------------------------------------------------
// <copyright file="WebDataService.svc.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.Data.Services;
using System.Data.Services.Common;
using System.Linq;
using JokeODataData;

namespace WebService.OData
{
    public class JokeService : DataService<JokeContext>
    {
        // This method is called only once to initialize service-wide policies.
        public static void InitializeService(DataServiceConfiguration config)
        {
            config.SetEntitySetAccessRule("Jokes", EntitySetRights.All);
            config.UseVerboseErrors = true;
            config.DataServiceBehavior.MaxProtocolVersion = DataServiceProtocolVersion.V3;
        }
    }

    // This is the version with custom context implemented 
    // To take more control of the CRUD methods, there is an example below 
    public class JokeContext : IUpdatable
    {
        private readonly JokeDataService _service;
        public JokeContext()
        {
            _service = new JokeDataService();
        }

        // It needs a IQueryable to make the search
        public IQueryable<Joke> Jokes => _service.GetJokes().AsQueryable();

        // To create record, first needs to create the instance of the object
        // A Post Call
        public object CreateResource(string containerName, string fullTypeName)
        {
            var joke = new Joke();
            _service.AddJoke(joke);
            return joke.Id;
        }

        // First step of the update command
        // Retrieve the object with the number id provided by the put call url
        // ex [host]/JokeService.svc/Jokes(2)
        public object GetResource(IQueryable query, string fullTypeName)
        {
            // the OData internals give us a IQueryable like (Jokes.Where(j=>j.Id == 2)) 
            // here we do what linq would do internally 
            var enumerator = query.GetEnumerator();
            enumerator.MoveNext();
            Joke joke = (Joke)enumerator.Current;
            return joke.Id;
        }

        public object ResetResource(object resource)
        {
            Joke joke = _service.GetJokeById((int)resource);
            return joke.Id;
        }

        // Object creation second step, 
        // That will be called for each instance property contained in the post call json
        // This will be used also for the update method at the  third step
        public void SetValue(object targetResource, string propertyName, object propertyValue)
        {
            Joke joke = _service.GetJokeById((int)targetResource);
            switch (propertyName)
            {
                case "Answer":
                    joke.Answer = propertyValue.ToString();
                    break;
                case "Question":
                    joke.Question = propertyValue.ToString();
                    break;
                case "Title":
                    joke.Title = propertyValue.ToString();
                    break;
                case "Grade":
                    joke.Grade = (int)propertyValue;
                    break;

            }
        }

        public object GetValue(object targetResource, string propertyName)
        {
            throw new System.NotImplementedException();
        }

        public void SetReference(object targetResource, string propertyName, object propertyValue)
        {
            throw new System.NotImplementedException();
        }

        public void AddReferenceToCollection(object targetResource, string propertyName, object resourceToBeAdded)
        {
            throw new System.NotImplementedException();
        }

        public void RemoveReferenceFromCollection(object targetResource, string propertyName, object resourceToBeRemoved)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteResource(object targetResource)
        {
            _service.DeleteJoke((int)targetResource);
        }

        // Every request finishes here 
        public void SaveChanges()
        {
            _service.Save();
        }

        // return the object last saved
        public object ResolveResource(object resource)
        {
            var joke = _service.GetJokeById((int)resource);
            return joke;
        }

        public void ClearChanges()
        {
            throw new System.NotImplementedException();
        }
    }
}
