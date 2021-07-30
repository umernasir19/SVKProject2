using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAdmin.Models
{
   public class Subject_MasterView
    {
       
        public int Subject_ID { get; set; }

        [DisplayName("Subject Name")]
        public string SubjectName { get; set; }

        [DisplayName("Active")]
        public bool isActive { get; set; }

        [DisplayName("Communicative")]
        public bool IsComm { get; set; }

        public Nullable<System.DateTime> LastUpdateUTC { get; set; }

        public Nullable<int> LastUpdateBy { get; set; }
    }
}
