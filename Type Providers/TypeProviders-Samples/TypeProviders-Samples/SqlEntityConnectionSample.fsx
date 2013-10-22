#r "FSharp.Data.TypeProviders"
#r "System.Data.Linq"
#r "System.Data.Entity"
#load @"..\packages\FSharp.Charting.0.87\FSharp.Charting.fsx"

open Microsoft.FSharp.Data.TypeProviders
open System.Data.Linq
open System.Data.Entity
open FSharp.Charting

module SqlDataConnectionSample = 
    type internal SFOData = SqlEntityConnection<ConnectionStringName = "SFO", ForceUpdate=true, Pluralize=false, SuppressForeignKeyProperties=false>
    let internal SFOContext = SFOData.GetDataContext()

    let internal SFOInfo = 
        query { for data in SFOContext.SFO do
                where (data.PublishedAirline <> "United Airlines")
                groupValBy data.LandingCount data.PublishedAirline into g
                let total = query { for group in g do sumByNullable group }
                select (g.Key, total.Value)
                }
                |> Seq.toList
                |> Chart.Column

    let internal NewSFO =
        SFOData.ServiceTypes.SFO.CreateSFO(0, 201310, "AirBerlin", "AB", "AirBerlin", "AB", "International", "Germany", "Passenger", "Wide Body", "Boeing")

    SFOContext.DataContext.AddObject("SFO", NewSFO)
    SFOContext.DataContext.SaveChanges()
