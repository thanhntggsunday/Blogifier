using Blogifier.Core.Entities.Base;

namespace Blogifier.Core.Entities
{
    public class Review : Entity
    {
        public string Name { get; set; }
        public string EMail { get; set; }
        public string Comment { get; set; }        
        public double Star { get; set; }
    }
}
