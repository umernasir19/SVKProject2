using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MyAdmin.Models
{
  public  class Subject_CombinationView
    {
        public int Combination_ID { get; set; }
        public List<SelectListItem> SUbjects { get; set; }
        public int FirstChoice { get; set; }
        public List<int> SecondChoice { get; set; }
       // public int SecondChoice { get; set; }
        public int StreamID { get; set; }

        public string stream { get; set; }
        public string subjectname { get; set; }
        public string SecondSubject { get; set; }
    }
}
