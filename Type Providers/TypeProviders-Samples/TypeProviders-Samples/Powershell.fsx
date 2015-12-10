#r "System.ServiceModel"
#r @"..\packages\FSharp.Management\lib\net40\FSharp.Management.PowerShell.dll"
#r "System.Management.Automation"
#r "Microsoft.PowerShell.Commands.Utility"
#r "FSharp.Core"

open Microsoft.PowerShell.Commands
open Microsoft.FSharp.Core
open FSharp.Management
open System.ServiceModel
open System.Management.Automation

type PS = PowerShellProvider<PSSnapIns = "", Is64BitRequired = false>

let find choice = match (choice:Choice<List<string>, List<System.DateTime>, List<PSObject>>) with 
                        | Choice1Of3 ch -> ch.Head
                        | Choice2Of3 ch -> ch.Head.Date.ToLongDateString() + " " + ch.Head.Date.ToLongTimeString()
                        | Choice3Of3 ch -> ch.Head.BaseObject.ToString()

let today = PS.``Get-Date``()

printfn "%s" <| find today
