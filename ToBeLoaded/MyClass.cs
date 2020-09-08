using System;

namespace ToBeLoaded
{
    public class MyClass : MarshalByRefObject
    {
        public string GetString() => "Hello World!";
    }
}
