using System;
using System.Linq;
using System.Reflection;

namespace FunWithAppDomains
{
    public class ProxyObject : MarshalByRefObject
    {
        private Type _type;
        public object _object;

        public void InstantiateObject()
        {
            var assembly = Assembly.LoadFrom(@"..\..\..\..\ToBeLoaded\bin\Debug\net48\ToBeLoaded.dll"); //LoadFrom loads dependent DLLs (assuming they are in the app domain's base directory

            _type = assembly.GetTypes().FirstOrDefault(t => t.Name.Contains("MyClass"));
            _object = Activator.CreateInstance(_type);
        }

        public object InvokeMethod(string methodName, object[] args)
        {
            var methodinfo = _type.GetMethod(methodName);
            return methodinfo.Invoke(_object, args);
        }
    }
}
