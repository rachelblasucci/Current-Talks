#load @"..\packages\FSharp.Charting.0.82\FSharp.Charting.fsx"
open FSharp.Charting
open System

/////
// Charting Quick-Start //
/////

// Convert the “Combining Charts” example to an asterisk, and center it on the chart.
Chart.Combine([ Chart.Line [(2, 15.); (1, 10.)]
                Chart.Line [(2, 10.); (1, 15.)]
                Chart.Line [(1.5, 7.5); (1.5, 17.5)] ])
                .WithXAxis(Min=0., Max=3.)
                .WithYAxis(Min=5., Max=20.)

/////
// Introduction to Data Visualization //
/////

// Add tan(x) to the combination graph; adjust axes, from and to limits, and height of sin/cos functions. Bonus: use ChartFunction instead of chartFunction.
let chartFunction step (fromX, toX) f =
    seq { for x in { fromX .. step .. toX } -> (x, f x) }
            |> Chart.Line

let ChartFunction (fromX, toX) f =
    let step = (toX - fromX) / 40.
    chartFunction step (fromX, toX) f

let sinX = ChartFunction (-3., 3.) (fun x ->  10. * (sin x))
let cosX = ChartFunction (-3., 3.) (fun x -> 10. * (cos x))
let tanX = ChartFunction (-3., 3.) (fun x -> tan x)
[ sinX; cosX; tanX ] |> Chart.Combine

/////
// Sampling Functions and Performance //
/////

// Using DateTime.Now.Ticks, find the difference in processing time for the final samples.
let rnd = new System.Random()
let uniform = seq { while (true) do yield rnd.NextDouble() }

let start = DateTime.Now.Ticks
//[ for x in { -3.14 .. 0.1 .. 3.14 } -> (x, sin x) ] |> Chart.Point
[| for x in { -3.14 .. 0.1 .. 3.14 } -> (x, sin x) |] |> Chart.Point
//seq { for x in { -3.14 .. 0.1 .. 3.14 } -> (x, sin x) } |> Chart.Point
let finish = DateTime.Now.Ticks

let time = finish - start


//Also. From: http://infsharpmajor.wordpress.com/2013/07/03/going-artistic-with-fsharp-charting/
let n = 16
let point i =
    let t = float(i % n) / float n * 2.0 * System.Math.PI in (sin t, cos t)
 
Chart.Combine(seq { for i in 0..n-1 do 
                    for j in i+1..n -> Chart.Line [point i; point j] })
                    