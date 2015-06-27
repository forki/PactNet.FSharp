namespace System
open System.Reflection

[<assembly: AssemblyTitleAttribute("PactNet.FSharp")>]
[<assembly: AssemblyProductAttribute("PactNet.FSharp")>]
[<assembly: AssemblyDescriptionAttribute("A functional wrapper for making PactNet nicer to work with from FSharp")>]
[<assembly: AssemblyVersionAttribute("0.1")>]
[<assembly: AssemblyFileVersionAttribute("0.1")>]
do ()

module internal AssemblyVersionInformation =
    let [<Literal>] Version = "0.1"
