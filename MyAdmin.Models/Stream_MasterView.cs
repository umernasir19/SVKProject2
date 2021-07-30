using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAdmin.Models
{
  public  class Stream_MasterView
    {
           [Key]
            public int Stream_ID { get; set; }
            [DisplayName("Stream Name")]
            public string StreamName { get; set; }
            public bool IsActive { get; set; }
       
    }
}
