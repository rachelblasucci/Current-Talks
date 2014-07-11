#r "System.ServiceModel"
#r @"..\packages\FSharp.Management.0.1.1\lib\net40\FSharp.Management.PowerShell.dll"
#r "System.Management.Automation"
#r "Microsoft.PowerShell.Commands.Utility"
#r "FSharp.Core"

open Microsoft.PowerShell.Commands
open Microsoft.FSharp.Core
open FSharp.Management
open System.ServiceModel
open System.Management.Automation

type PS = PowerShellProvider<PSSnapIns = "", Is64BitRequired = false>

let find choice = match (choice:Choice<List<System.DateTime>, List<string>, List<PSObject>>) with 
                        | Choice1Of3 ch -> ch.Head.Date.ToLongDateString() + " " + ch.Head.Date.ToLongTimeString()
                        | Choice2Of3 ch -> ch.Head
                        | Choice3Of3 ch -> ch.Head.BaseObject.ToString()

let today = PS.``Get-Date``()

printfn "%s" <| find today
