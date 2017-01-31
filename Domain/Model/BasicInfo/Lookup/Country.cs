using System.Collections.Generic;

namespace MTFS.Business.Domain.Model
{ 
    public partial class Country : Base
    {

        public Country()
        {
            locations = new HashSet<Location>();
            customers = new HashSet<Customer>();
        }

        public string countryName { get; set; }
        public virtual ICollection<Location> locations { get; set; }
        public virtual ICollection<Customer> customers { get; set; }

    }
}
