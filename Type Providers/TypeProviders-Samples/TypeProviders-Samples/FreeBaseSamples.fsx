#r "FSharp.Data.TypeProviders"
#r @"..\packages\FSharpx.TypeProviders.Freebase.1.7.9\lib\40\FSharpx.TypeProviders.Freebase.dll"
#r @"..\packages\FSharpx.TypeProviders.Freebase.1.7.9\lib\40\FSharpx.TypeProviders.Freebase.DesignTime.dll"
#load @"..\packages\FSharp.Charting.0.82\FSharp.Charting.fsx"

open Microsoft.FSharp.Data.TypeProviders
open FSharpx.TypeProviders.Freebase
open FSharp.Charting

module FreeBaseQuarksSample = 
    let data = FSharpx.TypeProviders.Freebase.FreebaseData.GetDataContext()

    let getfliteredParticles = 
        query { for particle in data.``Science and Technology``.Physics.``Subatomic particles`` do
                where (particle.Spin.HasValue && particle.Mass.Mass.HasValue && particle.``Electric charge``.HasValue) 
                select (particle.Name, particle.Spin.Value)}
        |> Chart.Line

module FreeBaseSample = 
    let data = FSharpx.TypeProviders.Freebase.FreebaseData.GetDataContext()

    let getfliteredParticles = 
        query { for player in data.Sports.``Martial Arts``.``Martial Arts Organizations`` do
                select (player. }
        |> Chart.Column
