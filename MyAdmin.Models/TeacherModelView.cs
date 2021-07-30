using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MyAdmin.Models
{
    public class TeacherModelView
    {
        [Key]
        public int id { get; set; }
        [Required]
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [Required]
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        [Required]
        [DisplayName("Mobile Number")]
        public string Phone { get; set; }
        [Required]
        [DisplayName("Select Department")]
        public string Department { get; set; }
        
        public string role { get; set; } 
    }
}
