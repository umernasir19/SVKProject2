using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAdmin.Models
{
   public class Login_Property
    {

        public int? Sid { get; set; }
        [Required(ErrorMessage = "Please Enter DHA ID")]
        public string DHA_Id { get; set; }
        [Required(ErrorMessage = "Please Enter LastName")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Please Enter DOB")]
        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }
        public String Message { get; set; }
    }
}
