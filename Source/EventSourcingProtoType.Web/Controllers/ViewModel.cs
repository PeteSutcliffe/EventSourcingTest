using System.Collections.Generic;
using EventSourcingProtoType.Web.Models;

namespace EventSourcingProtoType.Web.Controllers
{
    public class ViewModel 
    {
        public static List<Sport> Sports { get; set; } = new List<Sport>(); 
        public static List<Competitor> Competitors { get; set; } = new List<Competitor>(); 
        public static List<Fixture> Fixtures { get; set; } = new List<Fixture>(); 
    }
}