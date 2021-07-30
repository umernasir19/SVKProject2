using myAdmin.DB.DbOperations;
using MyAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdminPanelLte.Controllers
{
    public class StudentController : BaseController
    {
        // GET: Student
        DbRepository repository = null;
        public ActionResult Register()
        {
            return View();
        }

        public ActionResult StudentRegister(Student_Property objstudent)
        {
            if (ModelState.IsValid)
            {
                if (IsDHAIDExist(objstudent.DHA_Id))
                {
                    objstudent.Message = "DHA ID Already Exist Please Login or Select Other";
                    return View("Register", objstudent);
                }
                else
                {
                    repository = new DbRepository();
                    objstudent.IsActive = true;
                    int id=repository.AddUpdateStudent(objstudent);
                    if (id > 0)
                    {
                        objstudent.Message = "Registration Completed";
                    }
                    else
                    {
                        objstudent.Message = "Registration Failed Due to Error";
                    }
                    return View("Register", objstudent);
                }
            }
            else
            {
                return View("Register", objstudent);
            }
            
        }

        public bool IsDHAIDExist(string DHAID)
        {
            repository = new DbRepository();
            return repository.IsDHAIDEXIST(DHAID);

        }
    }
}