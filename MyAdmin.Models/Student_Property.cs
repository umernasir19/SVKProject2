using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAdmin.Models
{
   public class Student_Property
    {
        public int StudentID { get; set; }
        [Required(ErrorMessage ="Please Enter First Name")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Please Enter Last Name")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Please Enter DHA ID")]
        public string DHA_Id { get; set; }
        [Required(ErrorMessage = "Please Enter DOB")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "dd/MMM/yyyy")]
        public string DOB { get; set; }
        public DateTime CreatedAtUTC { get; set; }
        public DateTime UpdatedAtUTC { get; set; }
        public int UpdatedBy { get; set; }
        public bool IsActive { get; set; }

        [Required(ErrorMessage = "Please Enter Father Name")]
        public string FatherName { get; set; }
        [Required(ErrorMessage = "Please Enter Mobile No")]
        public string MobileNo { get; set; }

        [Required(ErrorMessage = "Please Enter Email ID")]
        public string email { get; set; }

        [Required(ErrorMessage = "Please Enter Percentage of Marks")]
        [DisplayName("Percentage of Marks obtained in HS")]
        public Nullable<decimal> percentageAtHS { get; set; }
       

        public string Message { get; set; }
    }
}
