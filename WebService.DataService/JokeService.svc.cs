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
        }
    }
}
