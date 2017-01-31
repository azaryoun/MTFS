using System.Collections.Generic;
namespace MTFS.Business.Domain.Model
{ 
    public partial class Currency : Base
    { 
        public string code { get; set; } 
        public string name { get; set; }
        public string  symbol { get; set; }

    }
}
