using AdminPanelLte.Utilities;
using myAdmin.DB.DbOperations;
using MyAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdminPanelLte.Controllers
{
    public class AccountController : BaseController
    {
        // GET: Account
        DbRepository repository = null;
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult AdminLogin()
        {
            return View();
        }

        public ActionResult Admin(User_Property objmodel)
        {
            if (ModelState.IsValid)
            {
                int flag = CheckUser(objmodel);
                if (flag == 1)
                {
                    Cookies.WriteUserInCookie(objmodel);
                    Session[Config.sessionNameForuserdata] = objmodel;
                    Session["UID"] = objmodel.UserID;
                    Session["Admin"] = true;// objmodel.Sid;
                    return RedirectToAction("Index", "Home");
                }
                else if (flag == 2)
                {
                    objmodel.Message = "Wrong Credentials";
                    return View("AdminLogin", objmodel);
                }
                else
                {
                    objmodel.Message = "Not Exist";
                    return View("AdminLogin", objmodel);
                }


            }
            else
            {
                return View("AdminLogin", objmodel);
            }
        }
        public ActionResult Logout(string viewname)
        {
            Session.RemoveAll();
            Session.Clear();
            Session.Abandon();
            Cookies.ClearCookie();
            Cookies.ClearCookieStudent();
            return View(viewname);
        }
        public ActionResult SubmitLogin(Login_Property objmodel)
        {
            if (ModelState.IsValid)
            {
                var flag = CheckLogin(objmodel);
                if (flag == 1)
                {
                    Cookies.WriteStudentInCookie(objmodel);
                    Session[Config.sessionNameForStudentdata] = objmodel;
                    Session["UID"] = objmodel.Sid;
                    Session["Student"] = true;// objmodel.Sid;
                    Session["SName"] = objmodel.LastName;
                    return RedirectToAction("Index", "Home");
                }
                else if (flag == 2)
                {
                    objmodel.Message = "You are not allowed login Or Wrong Credentitals";
                    return View("Login", objmodel);
                }
                else
                {
                    objmodel.Message = "No record Exist";
                    return View("Login", objmodel);
                }


            }
            else
            {
                return View("Login", objmodel);
            }
        }

        public int CheckLogin(Login_Property loginmodel)
        {


            repository = new DbRepository();
            return repository.LoginExist(loginmodel);
        }

        public int CheckUser(User_Property usermodel)
        {


            repository = new DbRepository();
            return repository.CheckUser(usermodel);
        }
    }
}