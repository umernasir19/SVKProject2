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
    
    public partial class Users
    {
        public int Id { get; set; }
        public string UserID { get; set; }
        public string U_Pwd { get; set; }
        public Nullable<System.DateTime> CreatedAtUTC { get; set; }
        public Nullable<bool> Isadmin { get; set; }
    }
}
