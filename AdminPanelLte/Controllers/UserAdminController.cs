﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;



namespace AdminPanelLte.Controllers
{
    public class UserAdminController : Controller
    {
       
        // GET: UserAdmin
        public ActionResult Login()
        {
            return View();
        }

    }
}