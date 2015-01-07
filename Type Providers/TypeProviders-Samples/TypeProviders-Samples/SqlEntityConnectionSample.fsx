#r "FSharp.Data.TypeProviders"
#r "System.Data.Linq"
#r "System.Data.Entity"
#load @"..\packages\FSharp.Charting.0.90.9\FSharp.Charting.fsx"

open Microsoft.FSharp.Data.TypeProviders
open System.Data.Linq
open System.Data.Entity
open FSharp.Charting

// Sum & graph landing counts by airline -- all flights, for all time 
module SqlDataConnectionSample = 
    type internal SFOData = SqlEntityConnection<ConnectionStringName = "SFO", ForceUpdate=true, Pluralize=false, SuppressForeignKeyProperties=false>
    let internal SFOContext = SFOData.GetDataContext()

    let internal SFOInfo = 
        query { for data in SFOContext.RawLandingsData do
                where (not (data.PublishedAirline.Contains("United Airlines"))) // again, numbers too big
                groupValBy data.LandingCount data.PublishedAirline into g
                let total = query { for group in g do sumBy group }
                select (g.Key, total)
                }
                |> Chart.Column

    // Add landing counts for a new airline.
    let internal NewSFO =
        SFOData.ServiceTypes.RawLandingsData.CreateRawLandingsData(0, 201407., "RachelsAirline", "RachelsAirline", "International", "Europe", "Passenger", "Wide Body", 800000., 800000., "2014", "July")


    SFOContext.DataContext.AddObject("RawLandingsData", NewSFO)
    SFOContext.DataContext.SaveChanges()
