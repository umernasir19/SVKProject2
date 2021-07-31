using MyAdmin.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminPanelLte.Utilities
{
    public class Cookies
    {
        public Cookies()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static void WriteUserInCookie(User_Property user)
        {
            try
            {
                HttpCookie myCookie = HttpContext.Current.Request.Cookies[Config.userDataCookie];
                if (myCookie != null)
                {
                    myCookie.Expires = DateTime.Now.AddDays(-1d);//Set the Expire date to yesterday
                    HttpContext.Current.Response.Cookies.Add(myCookie);
                }
                myCookie = new HttpCookie(Config.userDataCookie);
                string json = JsonConvert.SerializeObject(user);
                myCookie[Config.userDataCookie] = json;
                myCookie.Expires = DateTime.Now.AddDays(365d);
                HttpContext.Current.Response.Cookies.Add(myCookie);
            }
            catch
            {

            }
        }

        public static User_Property GetUserDataFromCookie()
        {
            try
            {
                HttpCookie myCookie = HttpContext.Current.Request.Cookies[Config.userDataCookie];
                string json = (myCookie[Config.userDataCookie]);
                User_Property udata = JsonConvert.DeserializeObject<User_Property>(json);

                return udata;
            }
            catch
            {
                return null;
            }
        }

        public static void WriteStudentInCookie(Login_Property user)
        {
            try
            {
                HttpCookie myCookie = HttpContext.Current.Request.Cookies[Config.studentDataCookie];
                if (myCookie != null)
                {
                    myCookie.Expires = DateTime.Now.AddDays(-1d);//Set the Expire date to yesterday
                    HttpContext.Current.Response.Cookies.Add(myCookie);
                }
                myCookie = new HttpCookie(Config.studentDataCookie);
                string json = JsonConvert.SerializeObject(user);
                myCookie[Config.studentDataCookie] = json;
                myCookie.Expires = DateTime.Now.AddDays(365d);
                HttpContext.Current.Response.Cookies.Add(myCookie);
            }
            catch
            {

            }
        }

        public static Student_Property GetStudentDataFromCookie()
        {
            try
            {
                HttpCookie myCookie = HttpContext.Current.Request.Cookies[Config.studentDataCookie];

                string json = myCookie[Config.studentDataCookie];
                Student_Property udata = JsonConvert.DeserializeObject<Student_Property>(json);

                return udata;
            }
            catch
            {
                return null;
            }
        }

        public static void ClearCookie()
        {
            string cookiename = Config.userDataCookie;
            if (HttpContext.Current.Request.Cookies[cookiename] != null)
            {
                HttpCookie myCookie = new HttpCookie(cookiename);
                myCookie.Expires = DateTime.Now.AddDays(-1d);
                HttpContext.Current.Response.Cookies.Add(myCookie);
            }
        }

        public static void ClearCookieStudent()
        {
            string cookiename = Config.studentDataCookie;
            if (HttpContext.Current.Request.Cookies[cookiename] != null)
            {
                HttpCookie myCookie = new HttpCookie(cookiename);
                myCookie.Expires = DateTime.Now.AddDays(-1d);
                HttpContext.Current.Response.Cookies.Add(myCookie);
            }
        }
    }
}