//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace myAdmin.DB
{
    using System;
    using System.Collections.Generic;
    
    public partial class Student_Master
    {
        public int StudentID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DHA_Id { get; set; }
        public string DOB { get; set; }
        public Nullable<System.DateTime> CreatedAtUTC { get; set; }
        public Nullable<System.DateTime> UpdatedAtUTC { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string FatherName { get; set; }
        public string MobileNo { get; set; }
        public string email { get; set; }
        public Nullable<decimal> percentageAtHS { get; set; }
    }
}
