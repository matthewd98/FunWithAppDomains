# FunWithAppDomains

I was having fun with AppDomains in .NET framework because we were using reflection in some of our projects and loading dependencies would sometimes cause issues due to shared dependencies between assemblies and version mismatch of those dependencies, thus requiring binding redirects. 
AppDomains seemed like a good way to resolve the dependency issue; however, it introduced its own complexity due to needing to create a proxy object. 

In .NET Core, with AssemblyLoadContext, the aforementioned complexity has been eliminated -- a proxy object is no longer needed.
