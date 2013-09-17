// Learn more about F# at http://fsharp.net
// See the 'F# Tutorial' project for more help.

[<EntryPoint>]
let main argv = 
    GetReplies.postAndReply

    printfn "%A" argv
    0 // return an integer exit code
