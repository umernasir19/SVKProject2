using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminPanelLte.Models
{
    public class DDLLIST
    {
        public enum Status
        {
           pending=1,
           accepted=2,
           rejected=3

        }

        public enum LoginMsg
        {
            Successful,
            WrongCredentials,
            EmailExists,
            InActive,
            NotExists,
            MobileExist,
            PhoneExist
        }
    }
}