using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAdmin.Models
{
   public class ApplicationViewModel
    {
        public int StudentID { get; set; }
        public int ApplicationId { get; set; }
        public int Status { get; set; }

        public Stream_MasterView streamList { get; set; }
        public Subject_MasterView SubjectList { get; set; }
        public Subject_MasterView CommunSubjectList { get; set; }
        public Student_Property student { get; set; }
        
        public string secondsubject { get; set; }
        public int? HS_SubjectMarks { get; set; }
    }
}
