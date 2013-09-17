namespace Emailer
open Data
open Agents

module main = 

    [<EntryPoint>]
    let main argv = 
        // post bulk to filterAgent
        // *filterAgent* filters by email type, then posts to individual type agents
        // *templatingAgent* templates and prepare emails, then post to sending agent 
        // *sending agent* "sends"
        // at any point can post to *erroring agent*

        filterAgent.Post GetData

        printfn "Type any key to continue.." 
        System.Console.ReadKey() |> ignore

        0 // return an integer exit code
