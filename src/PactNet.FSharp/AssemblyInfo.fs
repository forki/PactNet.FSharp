namespace System
open System.Reflection

[<assembly: AssemblyTitleAttribute("PactNet.FSharp")>]
[<assembly: AssemblyProductAttribute("PactNet.FSharp")>]
[<assembly: AssemblyDescriptionAttribute("A functional wrapper for making PactNet nicer to work with from FSharp")>]
[<assembly: AssemblyVersionAttribute("1.0")>]
[<assembly: AssemblyFileVersionAttribute("1.0")>]
do ()

module internal AssemblyVersionInformation =
    let [<Literal>] Version = "1.0"
