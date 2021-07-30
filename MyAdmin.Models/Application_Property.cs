using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAdmin.Models
{
   public class Application_Property
    {
        public int ApplicationId { get; set; }
        public int StudentID { get; set; }
        [Required(ErrorMessage ="Select Stream")]
        [DisplayName("Stream")]
        public int StreamId { get; set; }
        [Required(ErrorMessage = "Select First Subject")]
        [DisplayName("First Subjet")]
        public int FirstSubjectID { get; set; }
        [Required(ErrorMessage = "Select Second Subject")]
        [DisplayName("Second Subject")]
        public int SecondSubjectID { get; set; }
        [Required(ErrorMessage = "Select Communicative Subject")]
        [DisplayName("Communicative Subject")]
        public int CommunSubjecctId { get; set; }
        public int ApplicationStatus { get; set; }
        public DateTime LastUpdatedUTC { get; set; }
        public int LastUpdatedBy { get; set; }

        [Required(ErrorMessage = "Please Enter Marks At HS")]
        [DisplayName("HS Marks in this Subject")]
        public int MarksAtHS { get; set; }
        public List<Stream_MasterView> streamList { get; set; }
        public List<Subject_MasterView> SubjectList { get; set; }
        public List<Subject_MasterView> CommunSubjectList { get; set; }
    }
}
