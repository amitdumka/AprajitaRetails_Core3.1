using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using AprajitaRetails.Data;
using Microsoft.AspNetCore.Http;

namespace AprajitaRetails.Ops.Utility
{
    public static class Utils
    {
        public static string GetMemberName<T, TValue>(Expression<Func<T, TValue>> memberAccess)
        {
            return ( (MemberExpression) memberAccess.Body ).Member.Name;
        }

        public static void GetMem(Type typeClass)
        {
            MemberInfo [] members = typeof (Type).GetMembers ();//Type == ClassName
            foreach ( MemberInfo memberInfo in members.Where (p => p.MemberType == MemberTypes.Property) )
            {
                Console.WriteLine ("Name: {0}", memberInfo.Name); // Name: MyField
                Console.WriteLine ("Member Type: {0}", memberInfo.MemberType); // Member Type: Property
            }
        }
    }

    public class MemberHelper<T>
    {
        public string GetName<U>(Expression<Func<T, U>> expression)
        {
            MemberExpression memberExpression = expression.Body as MemberExpression;
            if ( memberExpression != null )
                return memberExpression.Member.Name;

            throw new InvalidOperationException ("Member expression expected.");
        }
    }


    public static class SessionUtil
    {
        public static void SetLoginSessionInfo(HttpContext context , int StoreId, string UserName, int EmpId)
        {
            context.Session.SetInt32 (Constants.STOREID, StoreId);
            context.Session.SetInt32 (Constants.EMPID, EmpId);
            context.Session.SetString (Constants.USERNAME, UserName);
        }

        public static SortedList<string, string> GetLoginSessionInfo(HttpContext context, int StoreId, string UserName, int EmpId)
        {
           var SId=  context.Session.GetInt32 (Constants.STOREID);
           var Empid= context.Session.GetInt32 (Constants.EMPID);
           var User= context.Session.GetString (Constants.USERNAME);
            SortedList<string, string> data = new SortedList<string, string> ()
            {
                {Constants.STOREID ,SId.ToString()}, {Constants.EMPID, Empid.ToString() },
                {Constants.USERNAME, User }
            };
            return data; 
        }


        public static int GetStoreId(HttpContext context)
        {
            //ToDo: in future it will take from database
            return context.Session.GetInt32 (Constants.STOREID) ?? 1;
        }
        public static string GetStoreCode(HttpContext context)
        {
            return context.Session.GetString (Constants.STORECODE);
        }
        public static void SetStoreId(HttpContext context, int storeid)
        {
            context.Session.SetInt32 (Constants.STOREID, storeid);
        }

        public static void SetStoreCode(HttpContext context, string storecode)
        {
            context.Session.SetString (Constants.STORECODE, storecode);
        }
        public static bool IsSessionSet(HttpContext context/*, AprajitaRetailsContext  db*/)
        {
            if ( !context.Session.IsAvailable || context.Session.Keys.Count () < 2 )
            {
                HelperUtil.SetStoreCode (context, "JH0006");
                HelperUtil.SetStoreId (context, 1);
                return false;
            }
            return true;
        }
    }

    public class HelperUtil
    {
        public static int GetStoreID(HttpContext context)
        {
            // ViewBag.Name = HttpContext.Session.GetString (SessionName);
            // int? id = context.Session.GetInt32 (Constants.STOREID);
            // return id?? 0;
            return context.Session.GetInt32 (Constants.STOREID) ?? 1;//ToDo: in future it will take from database
        }

        public static string GetStoreCode(HttpContext context)
        {
            return context.Session.GetString (Constants.STORECODE);
        }

        public static void SetStoreId(HttpContext context, int storeid)
        {
            context.Session.SetInt32 (Constants.STOREID, storeid);
        }

        public static void SetStoreCode(HttpContext context, string storecode)
        {
            context.Session.SetString (Constants.STORECODE, storecode);
        }

        public static bool IsSessionSet(HttpContext context)
        {
            if ( !context.Session.IsAvailable || context.Session.Keys.Count () < 2 )
            {
                HelperUtil.SetStoreCode (context, "JH0006");
                HelperUtil.SetStoreId (context, 1);
            }
            return true;
        }
    }
}
