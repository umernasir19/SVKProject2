using AdminPanelLte.Models;
using myAdmin.DB.DbOperations;
using MyAdmin.Models;
using MyAdmin.Models.Setups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static AdminPanelLte.Models.DDLLIST;

namespace AdminPanelLte.Controllers
{
    public class ApplicationController : BaseController
    {
        // GET: Application
        DbRepository db;
        public ActionResult Application()
        {
            Application_Property objappmodel = new Application_Property();
            db = new DbRepository();
            objappmodel.streamList = db.GetStream().Where(p => p.IsActive == true).Select(x => new Stream_MasterView { StreamName = x.StreamName, Stream_ID = x.Stream_ID }).ToList();
            objappmodel.SubjectList = new List<Subject_MasterView>();// db.GetSubject().Where(p => p.isActive == true && p.IsComm == false).Select(x => new Subject_MasterView { SubjectName = x.SubjectName, Subject_ID = x.Subject_ID }).ToList();
            objappmodel.CommunSubjectList =  db.GetSubject().Where(p => p.isActive == true && p.IsComm==true).Select(x => new Subject_MasterView { SubjectName = x.SubjectName, Subject_ID = x.Subject_ID }).ToList();
            ViewBag.SubjectList= db.GetSubject().Where(p => p.isActive == true && p.IsComm == false).Select(x => new Subject_MasterView { SubjectName = x.SubjectName, Subject_ID = x.Subject_ID }).ToList();
            ViewBag.CommunSubjectList = db.GetSubject().Where(p => p.isActive == true && p.IsComm == true).Select(x => new Subject_MasterView { SubjectName = x.SubjectName, Subject_ID = x.Subject_ID }).ToList();
            
            return View(objappmodel);
        }

        public ActionResult SubmitApplication(Application_Property objapp)
        {
            try
            {
                db = new DbRepository();
                objapp.StudentID = Convert.ToInt16(Session["UID"].ToString());
                objapp.ApplicationStatus =Convert.ToInt16(DDLLIST.Status.pending); 
                int id=  db.SubmitApplication(objapp);
                if (id > 0)
                {
                    ViewBag.Message = "Successfull";
                    return RedirectToAction("Application", "Application");
                }
                else
                {
                    ViewBag.Message = "Failed";
                    return View(objapp);

                }
                
            }
            catch(Exception ex)
            {
                return View(objapp);
            }
        }

        public JsonResult GetStreamSubjects(int id)
        {
            db = new DbRepository();
            var firststreamsubjects = db.GetSubjectCombinationdata().Where(p => p.StreamID == id).Select(p=>p.FirstChoice).Distinct().ToList();
            var secondstreamsubjects = db.GetSubjectCombinationdata().Where(p => p.StreamID == id).Select(p => p.SecondChoice).Distinct().ToList();
            var firstsubject = db.GetSubject().Where(p => firststreamsubjects.Contains(p.Subject_ID)).ToList();
            var scndsubject = db.GetSubject().Where(p => secondstreamsubjects.Contains(p.Subject_ID)).ToList();

            return Json(new {firstsubject=firstsubject,secondsubject= scndsubject }, JsonRequestBehavior.AllowGet);
        }



        public JsonResult GetSecondSubjects(int strid,int frstid)
        {
            db = new DbRepository();
            var scndsubjectlist=db.GetSubjectCombinationdata().Where(p=>p.StreamID==strid && p.FirstChoice==frstid).Select(p => p.SecondChoice).Distinct().ToList();
          
           
            
            var scndsubject = db.GetSubject().Where(p => scndsubjectlist.Contains(p.Subject_ID)).ToList();

            return Json(new {secondsubject = scndsubject }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult ViewApplication()
        {
            db = new DbRepository();
           List<ApplicationViewModel> appmodel = db.GetapplicationData();
            if (Session["Student"] != null && Session["Student"].ToString() != null)
            {
                int sid = Convert.ToInt16(Session["UID"].ToString());
                appmodel= appmodel.Where(p => p.StudentID == sid).ToList();
            }



                return View(appmodel);

        }


        
            public JsonResult ChangeApplicationStatus(int appid,int status)
        {
            db = new DbRepository();
            var flag= db.ChangeApplicationStatus(appid, status);
            return Json(new { data = "SuccessFull", statuscode = 200, flag = flag }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult FilterApplication1(Filter_Class_Property filter)
        {
            db = new DbRepository();
            List<ApplicationViewModel> appmodel = db.GetapplicationData();
            if (filter.Subject > 0)
            {
                appmodel = appmodel.Where(p => p.SubjectList.Subject_ID == filter.Subject && p.Status == 1).ToList();
            }
            if (filter.criteria > 0 && filter.Marks > 0)
            {
                if (filter.criteria == 1)
                {
                    //greater than
                    appmodel = appmodel.Where(p => p.HS_SubjectMarks > filter.Marks && p.Status == 1).ToList();
                }
                if (filter.criteria == 2)
                {
                    //less than
                    appmodel = appmodel.Where(p => p.HS_SubjectMarks < filter.Marks && p.Status == 1).ToList();
                }
                else if (filter.criteria == 3)
                {
                    //equal to
                    appmodel = appmodel.Where(p => p.HS_SubjectMarks == filter.Marks && p.Status == 1).ToList();
                }
            }
            //var data=JsonConvert.SerializeObject()
            return Json(new { data = appmodel }, JsonRequestBehavior.AllowGet);
        }
    }
}