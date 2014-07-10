#r "System.Data.Linq"
#r @"..\packages\FSharp.Data.2.0.9\lib\net40\FSharp.Data.dll"
#load @"..\packages\FSharp.Charting.0.90.6\FSharp.Charting.fsx"

open FSharp.Data
open FSharp.Charting
open System.Data.Linq

module FreeBaseQuarksSample = 
    let data = FreebaseData.GetDataContext()

    // (Name, charge, spin, family) for each subatomic particle
    let getParticles = 
        query { for particle in data.``Science and Technology``.Physics.``Subatomic particles`` do
                where (particle.Spin.HasValue && particle.``Electric charge``.HasValue)
                select (particle.Name, particle.``Electric charge``.Value, particle.Spin.Value, particle.Family)}
        |> Seq.toArray
    
    // (Name, Productions) for each opera house
    let operaHouses =
        query { for opera in data.``Arts and Entertainment``.Opera.``Opera houses`` do
                select (opera.Name, opera.Productions)}
        |> Seq.toArray

    operaHouses
        |> Array.map (fun (x,y) -> (x, y |> Seq.toArray))
        |> Array.filter (fun (x,y) -> y.Length > 0)
