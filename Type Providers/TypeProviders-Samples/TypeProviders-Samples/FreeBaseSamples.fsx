#r "FSharp.Data.TypeProviders"
#r @"..\packages\FSharpx.TypeProviders.Freebase.1.7.9\lib\40\FSharpx.TypeProviders.Freebase.dll"
#r @"..\packages\FSharpx.TypeProviders.Freebase.1.7.9\lib\40\FSharpx.TypeProviders.Freebase.DesignTime.dll"

open Microsoft.FSharp.Data.TypeProviders
open FSharpx.TypeProviders.Freebase

#load "show-wpf40.fsx"
module FreeBaseQuarksSample = 
    let data = FSharpx.TypeProviders.Freebase.FreebaseData.GetDataContext()

    let getfliteredParticles = 
        query { for particle in data.``Science and Technology``.Physics.``Subatomic particles`` do
                where (particle.Spin.HasValue) }
        |> showGrid

#load "show-wpf40.fsx"
module FreeBaseSample = 
    let data = FSharpx.TypeProviders.Freebase.FreebaseData.GetDataContext()

    let getfliteredParticles = 
        query { for player in data.Sports.``Martial Arts``.``Martial Arts Organizations`` do
                select player }
        |> showGrid
