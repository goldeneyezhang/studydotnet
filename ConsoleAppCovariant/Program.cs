using System;
using System.Collections.Generic;

namespace ConsoleAppCovariant
{
    delegate T MyFunc<out T>();
    delegate void MyFunc2<in T>(T obj);
    delegate Tout MyFunc3<in Tin, out Tout>(Tin obj);
    class Program
    {
        static void Main(string[] args)
        {
            // 协变
            Func<string> func1 = () => "KKKK";
            Func<object> func2 = func1;
            // 逆变
            Action<object> fun3 = t => { };
            Action<string> fun4 = fun3;
            // 协变
            MyFunc<string> str1 = () => "TTTT";
            MyFunc<object> str2 = str1;
            // 逆变
            MyFunc2<object> str3 = t => { };
            MyFunc2<string> str4 = str3;

            MyFunc3<object, string> str11 = t => "loud";
            MyFunc3<string, string> str22 = str11; //第一个泛型的逆变（object->string）
            MyFunc3<object, object> str33 = str11; //第二个泛型的协变（string->object）
            MyFunc3<string, string> str44 = str11; //第一个泛型的逆变和第二个泛型的协变

            // 协变
            IEnumerable<string> list = new List<string>();
            IEnumerable<object> list2 = list;

            // 逆变
            IComparable<object> list3 = null;
            IComparable<string> list4 = list3;

            IMotion<Teacher> x = new Run<Teacher>();
            IMotion<People> y = x;
            Console.WriteLine("Hello World!");
        }
    }
    public class People
    {

    }
    public class Teacher: People
    {

    }
    public interface IMotion<out T>
    {

    }
    public class Run<T> : IMotion<T>
    {

    }

}
