// Learn more about F# at http://fsharp.net. See the 'F# Tutorial' project
// for more guidance on F# programming.

open System

let x = 4+5

[|1..10|]
   |> Array.filter (fun x -> x % 2 = 0)
   |> Array.map (fun x -> x + 3)
   |> Seq.sum
 
let sumEvensPlusThree = 
    Array.filter (fun x -> x % 2 = 0)
    >> Array.map (fun x -> x + 3)
    >> Seq.sum

sumEvensPlusThree [|1..10|]

Some 4 |> Option.map (fun n -> n*3)

let make, model = ("Toyota", "Prius")

type Transport = 
    | Car of Make:string * Model: string
    | Bus of Route:int
    | Bicycle

let getThereVia (transport:Transport) = 
    printf "You traveled by "
    match transport with 
      | Car (make,model) -> printfn "a %s %s" make model
      | Bus route -> printfn "#%i bus" route 
      | Bicycle -> printfn "bicycle"

getThereVia Bicycle
getThereVia (Bus(4))
getThereVia <| Car("toyota", "prius")

