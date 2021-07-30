using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using MyAdmin.Models;
using myAdmin.DB.DbOperations;
using System.IO;

namespace AdminPanelLte.Controllers
{
    public class HomeController : BaseController
    {
        DbRepository repository = null;
        public HomeController()
        {
            repository = new DbRepository();
        }
        // GET: Home
        [HttpGet]
        public ActionResult Index()
        {

            repository = new DbRepository();
           
            List<ApplicationViewModel> appmodel = repository.GetapplicationData().Where(p=>p.Status==1).ToList();
            if (Session["Student"] != null && Session["Student"].ToString() != null)
            {
                int sid = Convert.ToInt16(Session["UID"].ToString());
                appmodel = appmodel.Where(p => p.StudentID == sid).ToList();
            }
            var firstchoicelist = repository.GetSubjectCombination().Select(p => p.FirstChoice).ToList();

           ViewBag.subjectlist= repository.GetSubject().Where(p => firstchoicelist.Contains(p.Subject_ID)).ToList();


            return View(appmodel);
        }

        [HttpGet]
        public ActionResult CreateGallery()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateGallery(GallerydbModel imageModel)
        {
            try
            {
                string fileName = Path.GetFileNameWithoutExtension(imageModel.ImageFile.FileName);
                string extension = Path.GetExtension(imageModel.ImageFile.FileName);
                fileName = fileName + System.DateTime.Now.ToString() + extension;
                imageModel.ImageDesc = "~/Image/" + fileName;
                fileName = Path.Combine(Server.MapPath("~/Image/"), fileName);
                imageModel.ImageFile.SaveAs(fileName);
                if (ModelState.IsValid)
                {

                    int id = repository.setGallery(imageModel);
                    if (id > 0)
                    {
                        ModelState.Clear();
                        ViewBag.isSucess = "saved";


                    }
                }


            }
            catch (Exception e)
            {
                ViewBag.isFaild = "Faild";
            }

            return View();

        }

        public ActionResult addTeacher()
        {
            return View();
        }

        //[HttpPost]
        //public ActionResult addTeacher(TeacherModelView teacherModel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        int id = repository.AddTeacher(teacherModel);
        //            if (id>0)
        //        {
        //            ModelState.Clear();
        //            ViewBag.IsSuccess = "Data Saved";
        //        }
        //    }
        //    return View();
        //}
        public JsonResult addTeacher1(TeacherModelView teacherModel)
        {
            if (ModelState.IsValid)
            {
                int id = repository.AddTeacher(teacherModel);
                if (id > 0)
                {
                    ModelState.Clear();
                    // ViewBag.IsSuccess = "Data Saved";
                }
            }
            return Json("true", JsonRequestBehavior.AllowGet);
        }

        public ActionResult addstream(int? id)
        {
            Stream_MasterView objstreammaster = new Stream_MasterView();
            if (id > 0)
            {
                var stramList = repository.GetStream().Where(x=>x.Stream_ID==id).FirstOrDefault();
                objstreammaster.StreamName = stramList.StreamName;
                objstreammaster.Stream_ID = stramList.Stream_ID;
                objstreammaster.IsActive = stramList.IsActive;
            }
            else
            {

            }
            return View(objstreammaster);

        }
        [HttpPost]
        public JsonResult addstream(Stream_MasterView stream_MasterModel)
        {

            if (ModelState.IsValid)
            {
                if (stream_MasterModel.Stream_ID > 0)
                {
                    int id = repository.AddUpdateStream(stream_MasterModel);
                    if (id > 0)
                    {
                        ModelState.Clear();
                        ViewBag.IsSuccess = "Data Updated";
                    }
                }
                else
                {

                    int id = repository.AddUpdateStream(stream_MasterModel);
                    if (id > 0)
                    {
                        ModelState.Clear();
                        ViewBag.IsSuccess = "Data Saved";
                    }
                }
            }
            return Json("true", JsonRequestBehavior.AllowGet);

        }

        public ActionResult Viewstream()
        {

            var stramList = repository.GetStream();
            var streamViewmodel = new List<Stream_MasterView>();
            foreach (var stream in stramList)
            {
                streamViewmodel.Add(new Stream_MasterView
                {
                    IsActive = stream.IsActive,
                    StreamName = stream.StreamName,
                    Stream_ID = stream.Stream_ID
                });
            }
            return View(streamViewmodel);

        }

        public ActionResult addSubjectMaster(int? id)
        {
            Subject_MasterView objmodel = new Subject_MasterView();
            if (id > 0)
            {
                var subjctlist = repository.GetSubject().Where(x => x.Subject_ID == id).FirstOrDefault();
                objmodel.Subject_ID = subjctlist.Subject_ID;
                objmodel.SubjectName = subjctlist.SubjectName;
                objmodel.isActive = subjctlist.isActive;
                objmodel.IsComm = subjctlist.IsComm;
                objmodel.LastUpdateUTC = DateTime.UtcNow;
               
            }
            else
            {
                objmodel.isActive = true;
            }

            return View(objmodel);

        }

        [HttpPost]
        public JsonResult addSubjectMaster(Subject_MasterView subject_Master)
        {

            if (ModelState.IsValid)
            {
                int id = repository.AddUpdateSubject(subject_Master);
                if (id > 0)
                {
                    ModelState.Clear();
                    ViewBag.IsSuccess = "Data Saved";
                    if (subject_Master.Subject_ID > 0)
                    {
                        ViewBag.IsSuccess = "Data Updated";
                    }
                    
                    
                }
            }
            return Json("true", JsonRequestBehavior.AllowGet);

        }

        public ActionResult ViewSubject()
        {

            var subjectList = repository.GetSubject();
            var subjectViewmodel = new List<Subject_MasterView>();
            foreach (var subject in subjectList)
            {
                subjectViewmodel.Add(new Subject_MasterView
                {
                    Subject_ID=subject.Subject_ID,
                     SubjectName = subject.SubjectName,
                    isActive = subject.isActive,
                    IsComm = subject.IsComm,
                    LastUpdateUTC = subject.LastUpdateUTC,
                    LastUpdateBy = subject.LastUpdateBy
                });
            }
            return View(subjectViewmodel);

        }

        public ActionResult SubComb(int? id)
        {
            Subject_CombinationView objmodel = new Subject_CombinationView();
            if (id > 0)
            {

            }
            else
            {
                objmodel.Combination_ID = 0;
            }
            var streamlist = repository.GetStream().Where(p => p.IsActive == true);
            var _stremlist = streamlist.ToList();


            var subjectList = repository.GetSubject().Where(p=>p.isActive==true && p.IsComm==false);
            var subjectViewmodel = subjectList.ToList();
            

            SelectList list = new SelectList(subjectViewmodel, "Subject_ID", "SubjectName");
            SelectList listStr = new SelectList(_stremlist, "Stream_ID", "StreamName");
            ViewBag.droplist = subjectViewmodel;
            ViewBag.scndsbjct = subjectViewmodel;
            ViewBag.streamdroplist = _stremlist;

           

            return View(objmodel);
        }

        [HttpPost]
        public ActionResult SubComb(Subject_CombinationView subject_Combination)
        {
            if (ModelState.IsValid)
            {
                int id = repository.AddSubComb(subject_Combination);
                if (id > 0)
                {
                    ModelState.Clear();
                    ViewBag.IsSuccess = "Data Saved";
                }
            }
            return RedirectToAction("SubComb");
           // return View();
        }


        public ActionResult ViewSubjectCombination()
        {

            var subcomlist = repository.GetSubjectCombination();
            
            return View(subcomlist);

        }

        [HttpPost]
        public JsonResult deleteSubCom(int streamid, int majorid)
        {
            string message = "";
            try
            {
               var flag= repository.CheckSubjectCominApplication(streamid, majorid);
                if (!flag)
                {
                    flag = repository.DeleteSubjectCombination(streamid, majorid);
                    return Json(new { data = "SuccessFull", statuscode = 400, flag = flag }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { data = "Application Exist Againts this combination", statuscode = 400 ,flag=false}, JsonRequestBehavior.AllowGet);
                }
            }
            catch(Exception ex)
            {
                return Json(new { data = ex.InnerException, statuscode = 400 }, JsonRequestBehavior.AllowGet);
            }
           // return Json(message, JsonRequestBehavior.AllowGet);

        }
    }
}