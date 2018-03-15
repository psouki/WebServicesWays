//------------------------------------------------------------------------------
// <copyright file="WebDataService.svc.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Data.Services;
using System.Data.Services.Common;
using System.Linq;
using System.Linq.Expressions;
using System.ServiceModel.Web;
using System.Web;

namespace WebService.DataService
{
    public class JokeService : DataService<JokesDomain>
    {
        // This method is called only once to initialize service-wide policies.
        // This is the version with the context out of the box
        // All CRUD methods implemented 
        public static void InitializeService(DataServiceConfiguration config)
        {
            config.SetEntitySetAccessRule("Joke", EntitySetRights.All);
            config.UseVerboseErrors = true;
            config.DataServiceBehavior.MaxProtocolVersion = DataServiceProtocolVersion.V3;
            config.SetServiceOperationAccessRule("GradeOrdered", ServiceOperationRights.All);
        }

        //A custom method with the LINQ query provided instead of the query string
        [WebGet(UriTemplate = "GradeOrdered")]
        public IQueryable<Joke> GradeOrdered()
        {
            return CurrentDataSource.Joke.OrderBy(j => j.Grade);
        }

        //With this interceptor applied the Joke entity only will return jokes with grades
        // greater than 2. It is used to apply default filters.
        [QueryInterceptor("Joke")]
        public Expression<Func<Joke, bool>> ApplyGradeFilter()
        {
            return j => j.Grade > 2;
        }

        // This will intercept every change operation and apply this rule.
        [ChangeInterceptor("Joke")]
        public void UpdateCustomer(Joke joke, UpdateOperations operations)
        {
            if (string.IsNullOrWhiteSpace(joke.Question))
            {
                throw new DataServiceException("The question is required !");
            }
        }


    }
}
