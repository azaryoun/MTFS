using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MTFS.Business.Domain.Model
{
    public abstract class Base
    {
        public int id { get; set; } 
        public DateTime? modifedDate { get; set; }
        public int? modiferUserId { get; set; }
        [Column(TypeName = "DateTime2")]
        public DateTime  createdDate { set; get; }
        //[ForeignKey("createdUserId")]
        //public User cc { get; set; }
        //[ForeignKey("modiferUserId")]
        //public User mm { get; set; }
        public int createdUserId { set; get; }
        [Timestamp]
        public byte[] rowVersion { get; set; } 

    }
}
