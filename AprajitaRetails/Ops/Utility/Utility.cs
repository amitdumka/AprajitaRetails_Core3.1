using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using AprajitaRetails.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AprajitaRetails.Ops.Utility
{
    public static class Utils
    {
        public static string GetMemberName<T, TValue>(Expression<Func<T, TValue>> memberAccess)
        {
            return ((MemberExpression)memberAccess.Body).Member.Name;
        }

        public static void GetMem(Type typeClass)
        {
            MemberInfo[] members = typeof(Type).GetMembers();//Type == ClassName
            foreach (MemberInfo memberInfo in members.Where(p => p.MemberType == MemberTypes.Property))
            {
                Console.WriteLine("Name: {0}", memberInfo.Name); // Name: MyField
                Console.WriteLine("Member Type: {0}", memberInfo.MemberType); // Member Type: Property
            }
        }
    }

    public class MemberHelper<T>
    {
        public string GetName<U>(Expression<Func<T, U>> expression)
        {
            MemberExpression memberExpression = expression.Body as MemberExpression;
            if (memberExpression != null)
                return memberExpression.Member.Name;

            throw new InvalidOperationException("Member expression expected.");
        }
    }


    public static class SessionCookies
    {
        //Cookies






        /// <summary>
        /// Remove Cookies
        /// </summary>
        /// <param name="context"></param>
        /// <param name="key"></param>
        public static void Remove(HttpContext context, string key) { context.Response.Cookies.Delete(key); }

        /// <summary>
        /// Write Coookies Values
        /// </summary>
        /// <param name="context"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="isPersistent"></param>
        public static void Write(HttpContext context, string key, string value, bool isPersistent)
        {
            CookieOptions options = new CookieOptions
            {
                Secure = true,
                HttpOnly = true,
                SameSite = SameSiteMode.None
            };
            if (isPersistent)
                options.Expires = DateTime.Now.AddDays(1);
            else
                options.Expires = DateTime.Now.AddSeconds(200);

            context.Response.Cookies.Append(key, value, options);

        }

        /// <summary>
        /// Read Cookies Values
        /// </summary>
        /// <param name="context"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string Read(HttpContext context, string key) { return context.Request.Cookies[key]; }

        /// <summary>
        /// Set Login Session Values
        /// </summary>
        /// <param name="context"></param>
        /// <param name="StoreId"></param>
        /// <param name="UserName"></param>
        /// <param name="EmpId"></param>
        public static void SetLoginSessionInfo(HttpContext context, int StoreId, string UserName, int EmpId)
        {
            context.Session.SetInt32(Constants.STOREID, StoreId);
            context.Session.SetInt32(Constants.EMPID, EmpId);
            context.Session.SetString(Constants.USERNAME, UserName);

            Write(context, Constants.STOREID, StoreId.ToString(), true);
            Write(context, Constants.EMPID, EmpId.ToString(), true);
            Write(context, Constants.USERNAME, UserName, true);


        }

        /// <summary>
        /// Get Session Values
        /// </summary>
        /// <param name="context"></param>
        /// <param name="StoreId"></param>
        /// <param name="UserName"></param>
        /// <param name="EmpId"></param>
        /// <returns></returns>
        public static SortedList<string, string> GetLoginSessionInfo(HttpContext context)
        {
            if (IsSessionSet(context))
            {
                var SId = context.Session.GetInt32(Constants.STOREID);
                var Empid = context.Session.GetInt32(Constants.EMPID);
                var User = context.Session.GetString(Constants.USERNAME);
                SortedList<string, string> data = new SortedList<string, string>() { { Constants.STOREID, SId.ToString() }, { Constants.EMPID, Empid.ToString() }, { Constants.USERNAME, User } };
                return data;
            }
            else
            {

                var SId = Int32.Parse(Read(context, Constants.STOREID));
                var Empid = Int32.Parse(Read(context, Constants.EMPID));
                var User = Read(context, Constants.USERNAME);
                SetLoginSessionInfo(context, SId, User, Empid);
                SortedList<string, string> data = new SortedList<string, string>() { { Constants.STOREID, SId.ToString() }, { Constants.EMPID, Empid.ToString() }, { Constants.USERNAME, User } };
                return data;


            }

        }


        public static int GetStoreId(HttpContext context)
        {
            //ToDo: in future it will take from database
            return context.Session.GetInt32(Constants.STOREID) ?? 1;
        }
        public static string GetStoreCode(HttpContext context)
        {
            return context.Session.GetString(Constants.STORECODE);
        }
        public static void SetStoreId(HttpContext context, int storeid)
        {
            context.Session.SetInt32(Constants.STOREID, storeid);
        }

        public static void SetStoreCode(HttpContext context, string storecode)
        {
            context.Session.SetString(Constants.STORECODE, storecode);
        }
        public static bool IsSessionSet(HttpContext context/*, AprajitaRetailsContext  db*/)
        {

            if (context.Session.IsAvailable)
                if (context.Session.Keys.Count() < 2)
                    return false;
                else return true;
            else return false;
        }
    }

    public class HelperUtil
    {
        public static int GetStoreID(HttpContext context)
        {
            // ViewBag.Name = HttpContext.Session.GetString (SessionName);
            // int? id = context.Session.GetInt32 (Constants.STOREID);
            // return id?? 0;
            return context.Session.GetInt32(Constants.STOREID) ?? 1;//ToDo: in future it will take from database
        }

        public static string GetStoreCode(HttpContext context)
        {
            return context.Session.GetString(Constants.STORECODE);
        }

        public static void SetStoreId(HttpContext context, int storeid)
        {
            context.Session.SetInt32(Constants.STOREID, storeid);
        }

        public static void SetStoreCode(HttpContext context, string storecode)
        {
            context.Session.SetString(Constants.STORECODE, storecode);
        }

        public static bool IsSessionSet(HttpContext context)
        {
            if (!context.Session.IsAvailable || context.Session.Keys.Count() < 2)
            {
                HelperUtil.SetStoreCode(context, "JH0006");
                HelperUtil.SetStoreId(context, 1);
            }
            return true;
        }
    }
}
