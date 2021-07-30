using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAdmin.Models
{
   public class User_Property
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Please Enter UID")]
        public string UserID { get; set; }

        [Required(ErrorMessage = "Please Enter Password")]
      [DataType(DataType.Password)]
        public string U_Pwd { get; set; }
        public DateTime CreatedAtUTC { get; set; }
        public bool Isadmin { get; set; }

        public string Message { get; set; }
    }
}
