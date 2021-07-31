using AdminPanelLte.Utilities;
using MyAdmin.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace AdminPanelLte.Models
{
    public static class Utility
    {
        public static bool SendSms(string phone,string text)
        {
            try
            {

                return true;
            }
            catch(Exception ex)
            {
                return false;
            }

        }

        public static string GetResponse(string sURL)

        {

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(sURL);

            request.MaximumAutomaticRedirections = 4;

            request.Credentials = CredentialCache.DefaultCredentials;

            try
            {

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                Stream receiveStream = response.GetResponseStream();

                StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);

                string sResponse = readStream.ReadToEnd();

                response.Close();

                readStream.Close();

                return sResponse;

            }
            catch
            {
                return "";
            }
        }


        public static User_Property GetUserDatafromSession(this HttpSessionStateBase session, string sessionkey = null)
        {
            try
            {
                string key = sessionkey == null ? Config.sessionNameForuserdata : sessionkey;

                if (session[key] == null)//session expired, so take the help of cookie//
                {
                    var cook = Cookies.GetUserDataFromCookie();

                    if (cook == null)//cookie null means cookie not exist=> user logged out//
                    {
                        HttpContext.Current.Session.RemoveAll();
                        return null;
                    }
                    else//cookie exists, so overload the session from cookie//
                    {
                        session[key] = cook;
                        return session[key] as User_Property;
                    }
                }
                else//session not expired, so return it//
                    return session[key] as User_Property;
            }
            catch
            {
                return null;
            }
        }

        public static Student_Property GetStudentDatafromSession(this HttpSessionStateBase session, string sessionkey = null)
        {
            try
            {
                string key = sessionkey == null ? Config.sessionNameForStudentdata : sessionkey;

                if (session[key] == null)//session expired, so take the help of cookie//
                {
                    var cook = Cookies.GetStudentDataFromCookie();

                    if (cook == null)//cookie null means cookie not exist=> user logged out//
                    {
                        HttpContext.Current.Session.RemoveAll();
                        return null;
                    }
                    else//cookie exists, so overload the session from cookie//
                    {
                        session[key] = cook;
                        return session[key] as Student_Property;
                    }
                }
                else//session not expired, so return it//
                    return session[key] as Student_Property;
            }
            catch
            {
                return null;
            }
        }


    }
}