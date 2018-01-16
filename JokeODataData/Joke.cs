using System;
using System.Collections.Generic;
using System.Data.Services.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace JokeODataData
{
    // In order to work with OData it needs this annotation
    // Make that in the references there is the Microsoft.Data.Services.Client instead System.Data.Services.Client
    // it will save tons of headache !!
    [DataServiceKey("Id")]
    public class Joke
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public int Grade { get; set; }
    }
}
