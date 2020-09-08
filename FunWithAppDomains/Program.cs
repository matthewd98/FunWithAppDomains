using System;
using System.Linq;
using System.Reflection;

namespace FunWithAppDomains
{
    class Program
    {
        static void Main(string[] _)
        {
            Console.WriteLine("Start");

            AppDomainSetup setup = new AppDomainSetup
            {
                ApplicationBase = Environment.CurrentDirectory
            };
            AppDomain domain = AppDomain.CreateDomain("ProxyDomain", null, setup);
            ProxyObject proxyObject = (ProxyObject)domain.CreateInstanceFromAndUnwrap(typeof(ProxyObject).Assembly.Location, "FunWithAppDomains.ProxyObject");
            proxyObject.InstantiateObject();
            var result = proxyObject.InvokeMethod("GetString", null);
            Console.WriteLine(result);

            Console.ReadKey();
        }

        static void ReflectionInSameAppDomain()
        {
            var assembly = Assembly.LoadFrom(@"..\..\..\..\ToBeLoaded\bin\Debug\net48\ToBeLoaded.dll");
            var type = assembly.GetTypes().FirstOrDefault(t => t.Name.Contains("MyClass"));
            dynamic inst = Activator.CreateInstance(type);

            Console.WriteLine(inst.GetString());
        }

        //Doesn't work.. creates a TransparentProx, which you can't do anything with.
        //Solution: create a moderator class and the load the assembly inside of it -
        //  https://stackoverflow.com/questions/658498/how-to-load-an-assembly-to-appdomain-with-all-references-recursively
        //static void ReflectionInDifferentAppDomain()
        //{
        //    AppDomain ad = AppDomain.CreateDomain("New domain");
        //    var inst = ad.CreateInstanceFromAndUnwrap(
        //        @"C:\Users\mattd\source\repos\FunWithAppDomains\ToBeLoaded\bin\Debug\net48\ToBeLoaded.dll",
        //        "ToBeLoaded.MyClass");
        //}
    }
}