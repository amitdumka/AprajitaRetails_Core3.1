using Microsoft.AspNetCore.Authorization;    using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

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


    //Then use it like so:
    //MemberHelper<MyClass> memberHelper = new MemberHelper<MyClass>();
    //string name = memberHelper.GetName(x => x.MyField);
}
