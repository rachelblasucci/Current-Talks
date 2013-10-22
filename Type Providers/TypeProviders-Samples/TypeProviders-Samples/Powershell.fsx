#r "System.ServiceModel"
#r @"..\packages\PowerShellTypeProvider.0.2.2\lib\net45\PowerShellTypeProvider.dll"
#r "System.Management.Automation"
#r "Microsoft.PowerShell.Commands.Utility"
#r "FSharp.Core"

open Microsoft.PowerShell.Commands
open Microsoft.FSharp.Core
open FSharp.PowerShell
open System.ServiceModel
open System.Management.Automation

type PS = PowerShellTypeProvider<PSSnapIns="", Is64BitRequired=false>

let find choice = match (choice:Choice<List<System.DateTime>, List<string>>) with 
                        | Choice1Of2 ch -> ch.Head.Date.ToLongDateString() + " " + ch.Head.Date.ToLongTimeString()
                        | Choice2Of2 ch -> ch.Head

let today = PS.``Get-Date``()

printfn "%s" <| find today
