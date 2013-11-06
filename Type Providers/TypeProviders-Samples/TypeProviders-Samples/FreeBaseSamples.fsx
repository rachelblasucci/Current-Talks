#r @"..\packages\FSharp.Data.1.1.10\lib\net40\FSharp.Data.dll"
#r "System.Data.Linq"
#load @"..\packages\FSharp.Charting.0.87\FSharp.Charting.fsx"

open FSharp.Data
open FSharp.Charting
open System.Data.Linq

module FreeBaseQuarksSample = 
    let data = FreebaseData.GetDataContext()
    let getParticles = 
        query { for particle in data.``Science and Technology``.Physics.``Subatomic particles`` do
                where (particle.Spin.HasValue && particle.``Electric charge``.HasValue)
                select (particle.Name, particle.``Electric charge``.Value, particle.Spin.Value, particle.Family)}
        |> Seq.toArray
    
    let operaHouses =
        query { for opera in data.``Arts and Entertainment``.Opera.``Opera houses`` do
                select (opera.Name, opera.Productions)
        }
        |> Seq.toArray

    operaHouses
        |> Array.map (fun (x,y) -> (x, y |> Seq.toArray))
        |> Array.filter (fun (x,y) -> y.Length > 0)
