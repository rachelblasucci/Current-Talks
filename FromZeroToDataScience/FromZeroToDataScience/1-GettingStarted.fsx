#load "Data.fs"
open Data

/////
// Bindings and values and REPLs! Oh my! //
/////

// Print a statement with a float, and then a decimal.
printfn "This is a float: %f" 1.45
printfn "This is a decimal: %M" 1.45M

/////
// Fun with Functional Functions //
/////

// Add to the toHackerTalk function by changing e -> 3 and l -> 1
let toHackerTalk (phrase:string) =
    phrase
        .Replace('t', '7')
        .Replace('o', '0')
        .Replace('l', '1')
        .Replace('e', '3')

/////
// Chaining Functions with the Forward Pipe Operator //
/////

// Find the difference between the max high and the min low.
// Max high – min low = 1.46
let max_high = stockData
                |> List.map splitCommas
                |> List.maxBy (fun x -> x.[2])
                |> (fun x -> x.[2])

let min_low = stockData
                |> List.map splitCommas
                |> List.minBy (fun x -> x.[3])
                |> (fun x -> x.[3])

let result = float max_high - float min_low

// Find the difference between open and close on the day with the highest volume.
// Open – close on highest volume = 0.36
let finddiff = stockData
                |> List.map splitCommas
                |> List.maxBy (fun x -> x.[5])
                |> (fun x -> abs(float x.[1] - float x.[4]))


/////
// Using Data Structures to Create Larger Programs //
/////

// Add a SuperLeaf to PowerUp; fix any warnings.
type MushroomColor =
| Red
| Green
| Purple

type PowerUp =
| FireFlower
| Mushroom of MushroomColor
| Star of int
| SuperLeaf 

let handlePowerUp powerUp =
    match powerUp with
    | FireFlower -> printfn "Ouch, that's hot!"
    | Mushroom color -> match color with
                        | Red -> printfn "Please don't step on me..."
                        | Green -> printfn "1UP!!!"
                        | Purple -> printfn "Sorry, about that!"
    | Star duration -> printfn "Let me play some special music for you 
        for %d seconds." duration
    | SuperLeaf -> printfn "I can fly! I can fly!"

// Test handlePowerUp.
let powerUp = SuperLeaf
handlePowerUp powerUp
