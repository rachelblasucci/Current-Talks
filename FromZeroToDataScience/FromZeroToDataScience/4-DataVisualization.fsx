#r "System.Windows.dll"
#load @"..\packages\FSharp.Charting.0.87\FSharp.Charting.fsx"
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
open System
open System.Collections.ObjectModel
open System.Threading

let data1 = new ObservableCollection<float*int>()
data1 |> Chart.Point

type Sensor(src:seq<float * int>) =
  let terminate = ref false
  let data = src.GetEnumerator()
  let sample = new Event<float * int>()
  let t = new Thread(fun () -> 
    while not !terminate && data.MoveNext() do
      sample.Trigger(data.Current)
      Thread.Sleep(1000)
  )  
  member x.DataAvailable = sample.Publish
  member x.Start () = t.Start()
  member x.Stop () = terminate := true

let rnd = new System.Random()
let gaussianBoxMuller mean variance =
  seq {
    while (true) do
      let u = rnd.NextDouble()
      let v = rnd.NextDouble()
      yield mean + variance * sqrt(-2. * log(u)) * cos(2. * System.Math.PI * v)
  }
let sample precision count dist =
  let p = System.Math.Pow(10., precision)
  dist
    |> Seq.take count
    |> Seq.countBy (fun v -> floor(v * p) / p)

let values = sample 3. 100000 (gaussianBoxMuller 0. 1.)
let s = new Sensor(values)
s.DataAvailable.Add(fun v -> data1.Add(v))
s.Start()

s.Stop()


//Also. From: http://infsharpmajor.wordpress.com/2013/07/03/going-artistic-with-fsharp-charting/
let n = 16
let point i =
    let t = float(i % n) / float n * 2.0 * System.Math.PI in (sin t, cos t)
 
Chart.Combine(seq { for i in 0..n-1 do 
                    for j in i+1..n -> Chart.Line [point i; point j] })
                    