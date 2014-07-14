//TYPE PROVIDERS **AND** UNITS OF MEASURE
#r @"packages/FSharp.Data.2.0.8/lib/net40/FSharp.Data.dll"
#r "System.Data.Linq" 

open FSharp.Data
open System.Data.Linq

module FreeBaseQuarksSample = 
    let data = FreebaseData.GetDataContext()

        // (Name, charge, spin, family) for each subatomic particle. In coulombs! 
    let getParticles = 
        query { for particle in data.``Science and Technology``.Physics.``Subatomic particles`` do
                where (particle.Spin.HasValue && particle.``Electric charge``.HasValue)
                select (particle.Name, particle.``Electric charge``.Value, particle.Spin.Value, particle.Family)}
        |> Seq.toArray
