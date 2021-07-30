using myAdmin.DB.DbOperations;
using MyAdmin.Models;
using MyAdmin.Models.Setups;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdminPanelLte.Controllers
{
    public class BaseController : Controller
    {
        // GET: Base
        DbRepository db;

        public JsonResult FilterApplication(Filter_Class_Property filter)
        {
            db = new DbRepository();
            List<ApplicationViewModel> appmodel = db.GetapplicationData();
            if (filter.Subject > 0)
            {
                appmodel = appmodel.Where(p => p.SubjectList.Subject_ID == filter.Subject && p.Status==1).ToList();
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
                else
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