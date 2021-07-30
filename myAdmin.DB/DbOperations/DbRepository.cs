using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyAdmin.Models;
using System.Data.Entity;

namespace myAdmin.DB.DbOperations
{
   public  class DbRepository
    {
       
        public int setGallery(GallerydbModel model)
        {
            using (var context = new SchoolDBEntities())
            {
                tblGallery gallery = new tblGallery()
                {
                    ImageTitle = model.ImageTitle,
                       ImageDesc=model.ImageDesc
                };
                context.tblGalleries.Add(gallery);
                context.SaveChanges();
                return gallery.imgID;

            }
        }

        public int AddTeacher(TeacherModelView model)
        {
            using (var context = new SchoolDBEntities())
            {
                Teacher _teacher = new Teacher()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Phone = model.Phone,
                    Department = model.Department,
                    role = "Teacher"
                };

                context.Teacher.Add(_teacher);
                context.SaveChanges();               
                return _teacher.id;
            }
            
        }
        public int AddUpdateStream(Stream_MasterView model)
        {
            using (var context = new SchoolDBEntities())
            {
                
                Stream_Master _stream_Master = new Stream_Master()
                {
                    Stream_ID=model.Stream_ID,
                    StreamName = model.StreamName,
                    IsActive = model.IsActive
                };
                if (model.Stream_ID > 0)
                {
                    context.Stream_Master.Add(_stream_Master);
                    context.Entry(_stream_Master).State = EntityState.Modified;
                    context.SaveChanges();
                }
                else {
                    context.Stream_Master.Add(_stream_Master);
                    context.SaveChanges();
                }
                return _stream_Master.Stream_ID;
            }

        }

        public List<Stream_Master> GetStream()
        {
            using (var context = new SchoolDBEntities())
            {

                return context.Stream_Master.ToList();
               
            }

        }


        public int AddUpdateSubject(Subject_MasterView model)
        {
            using (var context = new SchoolDBEntities())
            {
                Subject_Master _Subject_Master = new Subject_Master()
                {
                   Subject_ID=model.Subject_ID,
                   SubjectName=model.SubjectName,
                   isActive=model.isActive,
                   IsComm=model.IsComm,
                   LastUpdateUTC=System.DateTime.UtcNow,
                   LastUpdateBy= 1
                };

                context.Subject_Master.Add(_Subject_Master);
                if (model.Subject_ID > 0)
                {
                    context.Entry(_Subject_Master).State = EntityState.Modified;
                }
                context.SaveChanges();
                return _Subject_Master.Subject_ID;
            }

        }

        public List<Subject_Master> GetSubject()
        {
            using (var context = new SchoolDBEntities())
            {

                return context.Subject_Master.ToList();

            }

        }

        public int AddSubComb(Subject_CombinationView model)
        {
            using (var context = new SchoolDBEntities())
            {
                Subject_Combination _SubjectComb= new Subject_Combination(); 
                for (int i = 0; i < model.SecondChoice.Count; i++)
                {
                    _SubjectComb = new Subject_Combination()
                    {

                        FirstChoice = model.FirstChoice,
                        SecondChoice=model.SecondChoice[i],
                        StreamID = model.StreamID

                    };
                    context.Subject_Combination.Add(_SubjectComb);
                    context.SaveChanges();
                }

                //Subject_Combination _SubjectComb = new Subject_Combination()
                //{
                  
                //    FirstChoice=model.FirstChoice,
                //    //SecondChoice=model.SecondChoice,
                //    StreamID=model.StreamID

                //};

               
                return _SubjectComb.Combination_ID;
            }

        }

        public List<Subject_CombinationView> GetSubjectCombination()
        {
            using (var context = new SchoolDBEntities())
            {
                var result = (from subc in context.Subject_Combination
                              group subc by subc.FirstChoice into g
                              join str in context.Stream_Master on g.FirstOrDefault().StreamID equals str.Stream_ID
                              join sub in context.Subject_Master on g.FirstOrDefault().FirstChoice equals sub.Subject_ID
                              join secsub in context.Subject_Master on g.FirstOrDefault().SecondChoice equals secsub.Subject_ID
                               select new Subject_CombinationView
                              {
                                  
                                  stream = context.Stream_Master.Where(p => p.Stream_ID == str.Stream_ID).FirstOrDefault().StreamName,
                                  subjectname = context.Subject_Master.Where(p => p.Subject_ID == sub.Subject_ID).FirstOrDefault().SubjectName,
                                  

                               }).ToList();

              var R21=  context.Subject_Combination
             .Join(
             context.Subject_Master,
             tCS => tCS.FirstChoice,
             s => s.Subject_ID,
             (tCS, s) =>
             new
             {
                 tCS = tCS,
                 s = s
             }
             )
             .Join(
             context.Stream_Master,
             p => p.tCS.StreamID,
             st => st.Stream_ID,
             (p, st) =>
             new
             {
                 p = p,
                 st = st
             }
             )
             .Join(
             context.Subject_Master,
             x => x.p.tCS.SecondChoice,
             c => c.Subject_ID,
             (x, c) =>
             new
             {
                 x = x,
                 c = c
             }
             )
             .GroupBy(
             y =>
             new
             {
                 first = y.x.p.tCS.FirstChoice,
                 firstsubject = y.x.p.s.SubjectName,
                 streamid=y.x.st.Stream_ID,//.tCS.StreamID,
                 streamname=y.x.st.StreamName
             },
             y => y.c.SubjectName
             )
             .Select(
             g =>
             new
             {
                 sirstsubid = g.Key.first,
                 firstsub = g.Key.firstsubject,
                 strid=g.Key.streamid,
                 streamnam=g.Key.streamname,
                 //FatherName = g.Key.FatherName,
                 //Address = g.Key.Address,
                 //ContactNo = g.Key.MobileNo,
                 courseName = g.Select(e => e).Distinct()
             }
             ).ToList().Select(l =>
             new Subject_CombinationView()
             {
                 FirstChoice = l.sirstsubid,
                 subjectname = l.firstsub,
                 StreamID=l.strid,
                 //FatherName = l.FatherName,
                 //Address = l.Address,
                 //ContactNo = l.ContactNo,
                 stream=l.streamnam,
                 SecondSubject = string.Join(", ", l.courseName.ToArray())
             }).ToList();


                return R21;

            }

        }

        public List<Subject_Combination> GetSubjectCombinationdata()
        {
            using (var context = new SchoolDBEntities())
            {
                var result = context.Subject_Combination.ToList();


                return result;

            }

        }
        public bool IsDHAIDEXIST(string dhaid)
        {
            using (var context = new SchoolDBEntities())
            {
                var exist=context.Student_Master.Where(p => p.DHA_Id == dhaid).FirstOrDefault();
                if (exist != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

        }

        public int AddUpdateStudent(Student_Property model)
        {
            try
            {
                using (var context = new SchoolDBEntities())
                {
                    DateTime DOB = Convert.ToDateTime(model.DOB);
                    Student_Master _model_Master = new Student_Master()
                    {
                        StudentID = model.StudentID,
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        FatherName = model.FatherName,
                        MobileNo = model.MobileNo,
                        percentageAtHS = model.percentageAtHS,
                        email = model.email,
                        DOB = DOB.ToString("dd/MMM/yyyy"),
                        DHA_Id = model.DHA_Id,
                        UpdatedBy = 0,
                        IsActive = model.IsActive,
                        CreatedAtUTC = DateTime.UtcNow,// model.IsComm,
                        UpdatedAtUTC = DateTime.UtcNow

                    };

                    context.Student_Master.Add(_model_Master);
                    //if (model.Subject_ID > 0)
                    //{
                    //    context.Entry(_Subject_Master).State = EntityState.Modified;
                    //}
                    context.SaveChanges();
                    return _model_Master.StudentID;
                }
            }
            catch(Exception ex)
            {
                return 0;
            }

        }


        public int LoginExist(Login_Property loginmodel)
        {
            using (var context = new SchoolDBEntities())
            {
                string DOB = loginmodel.DOB.ToString("dd/MMM/yyyy");
                //var exist = context.Student_Master.Where(p => p.DHA_Id == loginmodel.DHA_Id && p.LastName==loginmodel.LastName &&p.DOB==DOB).FirstOrDefault();
                var exist = context.Student_Master.Where(p => p.DHA_Id == loginmodel.DHA_Id).FirstOrDefault();

                if (exist != null)
                {
                    
                    if (exist.DOB==DOB&&exist.LastName==loginmodel.LastName)
                    {
                        var studentapplication = context.Application_Master.Where(p => p.StudentID == exist.StudentID).ToList();
                        if (studentapplication.Count > 0)
                        {
                            var flag = 2;
                            var pendingapp = studentapplication.Where(p => p.ApplicationStatus == 1).ToList();
                            var activeapp = studentapplication.Where(p => p.ApplicationStatus == 2).ToList();
                            var rejectedapp = studentapplication.Where(p => p.ApplicationStatus == 3).ToList();
                            if (rejectedapp.Count > 0)
                            {
                                flag = 2;
                            }
                            if (pendingapp.Count > 0)
                            {
                                loginmodel.Sid = exist.StudentID;
                                flag = 1;
                            }
                            if (activeapp.Count > 0)
                            {
                                loginmodel.Sid = exist.StudentID;
                                flag = 1;
                            }
                            return flag;
                        }
                        {
                            loginmodel.Sid = exist.StudentID;
                            loginmodel.LastName = exist.FirstName +exist.LastName;
                            return 1;
                        }
                    }
                    else
                    {
                        //loginmodel.Sid = exist.StudentID;
                        return 2;
                    }
                }
                else
                {
                    
                    return 3;
                }
            }

        }
        public int CheckUser(User_Property loginmodel)
        {
            using (var context = new SchoolDBEntities())
            {

                var exist = context.Users.Where(p => p.UserID == loginmodel.UserID).FirstOrDefault();
                if (exist != null)
                {
                    if (exist.U_Pwd == loginmodel.U_Pwd)
                    {
                        //login sucees
                        loginmodel.Id = exist.Id;
                        return 1;
                    }
                    else
                    {//wrong cred
                        return 2;
                    }
                }
                else
                {//not exist
                    return 3;
                }
            }
        }
        
        public int SubmitApplication(Application_Property model)
        {
            using (var context = new SchoolDBEntities())
            {
                Application_Master _App_Master = new Application_Master()
                {
                    ApplicationId = model.ApplicationId,
                    StreamId=model.StreamId,
                    FirstSubjectID = model.FirstSubjectID,
                    SecondSubjectID = model.SecondSubjectID,
                    CommunSubjecctId = model.CommunSubjecctId,
                    StudentID = model.StudentID,
                    MarksAtHS=model.MarksAtHS,
                    ApplicationStatus = model.ApplicationStatus,
                    LastUpdatedUTC=DateTime.UtcNow
                };

                context.Application_Master.Add(_App_Master);
                if (model.ApplicationId > 0)
                {
                    context.Entry(_App_Master).State = EntityState.Modified;
                }
                context.SaveChanges();
                return _App_Master.ApplicationId;
            }
        }


        public List<ApplicationViewModel> GetapplicationData()
        {
            using (var context = new SchoolDBEntities())
            {
                var result = context.Application_Master.ToList().Select(p => new ApplicationViewModel
                { ApplicationId = p.ApplicationId,
                Status=p.ApplicationStatus,
                HS_SubjectMarks=p.MarksAtHS,
                StudentID=p.StudentID,
                    student = context.Student_Master.Where(S => S.StudentID == p.StudentID).Select(x => new Student_Property
                    { FirstName = x.FirstName ,
                    LastName=x.LastName,
                        DHA_Id =x.DHA_Id,
                        DOB =x.DOB,
                        FatherName=x.FatherName,
                        percentageAtHS=x.percentageAtHS,
                        MobileNo=x.MobileNo,
                        email=x.email
                    }
                    ).FirstOrDefault(),
                    
                    streamList = context.Stream_Master.Where(S => S.Stream_ID == p.StreamId).Select(x => new Stream_MasterView { StreamName = x.StreamName }).FirstOrDefault(),
                    SubjectList= context.Subject_Master.Where(S => S.Subject_ID == p.FirstSubjectID).Select(x => new Subject_MasterView { SubjectName = x.SubjectName,Subject_ID=x.Subject_ID }).FirstOrDefault(),
                    CommunSubjectList= context.Subject_Master.Where(S => S.Subject_ID == p.CommunSubjecctId).Select(x => new Subject_MasterView { SubjectName = x.SubjectName, Subject_ID = x.Subject_ID }).FirstOrDefault(),
                    secondsubject = context.Subject_Master.Where(S => S.Subject_ID == p.SecondSubjectID).FirstOrDefault().SubjectName
                    //context.Stream_Master.Where(S => S.Stream_ID == p.StreamId).Select(x => new Stream_MasterView { StreamName = x.StreamName }).FirstOrDefault(),


                }).ToList();


                return result;

            }

        

        }

        public bool CheckSubjectCominApplication(int streamid,int majorid)
        {
            //return true if exist
            try
            {
                using(var db= new SchoolDBEntities())
                {
                    var applicationcheck = db.Application_Master.Where(p => p.FirstSubjectID == majorid || p.SecondSubjectID == majorid).ToList();
                    if (applicationcheck.Count > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch(Exception ex)
            {
                return false;
            }

        }

        public bool DeleteSubjectCombination(int streamid, int majorid)
        {
            try
            {
                using (var db = new SchoolDBEntities())
                {
                    db.Subject_Combination.RemoveRange(db.Subject_Combination.Where(p => p.StreamID == streamid && p.FirstChoice == majorid));
                    db.SaveChanges();

                        
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public bool ChangeApplicationStatus(int appid, int status)
        {
            try
            {
                using (var context = new SchoolDBEntities())
                {
                    Application_Master Appmaster = context.Application_Master.Where(p => p.ApplicationId == appid).FirstOrDefault();

                    Appmaster.ApplicationStatus = status;
                    Appmaster.LastUpdatedUTC = DateTime.UtcNow;
                    context.Application_Master.Add(Appmaster);
                   
                        context.Entry(Appmaster).State = EntityState.Modified;
                   
                    context.SaveChanges();
                    
                }

                return true;
            }
               
            
            catch (Exception ex)
            {
                return false;
            }

        }
    }
}
