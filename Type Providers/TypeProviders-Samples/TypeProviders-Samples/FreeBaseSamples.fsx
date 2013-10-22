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
    
    let aircraftModels =
        query { for manufacturers in data.Transportation.Aviation.``Aircraft manufacturers`` do
                select (manufacturers.Name, manufacturers.``Aircraft Models Manufactured``|> Seq.toArray)
        }
        |> Seq.toArray
