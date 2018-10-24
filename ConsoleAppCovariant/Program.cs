using System;

namespace ConsoleAppCovariant
{
    delegate T MyFunc<out T>();
    delegate void MyFunc2<in T>(T obj);
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

            Console.WriteLine("Hello World!");
        }
    }
}
