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
    
    public partial class Application_Master
    {
        public int ApplicationId { get; set; }
        public int StudentID { get; set; }
        public int StreamId { get; set; }
        public int FirstSubjectID { get; set; }
        public int SecondSubjectID { get; set; }
        public Nullable<int> CommunSubjecctId { get; set; }
        public int ApplicationStatus { get; set; }
        public Nullable<System.DateTime> LastUpdatedUTC { get; set; }
        public Nullable<int> LastUpdatedBy { get; set; }
        public Nullable<int> MarksAtHS { get; set; }
    }
}
