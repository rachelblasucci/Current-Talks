#r "FSharp.Data.TypeProviders"
#r "System.Data.Linq"
#r "System.Data.Entity"
#load @"..\packages\FSharp.Charting\FSharp.Charting.fsx"

open Microsoft.FSharp.Data.TypeProviders
open System.Data.Linq
open System.Data.Entity
open FSharp.Charting

// Sum & graph landing counts by airline -- all flights, for all time 
module SqlDataConnectionSample = 
    type internal SFOData = SqlEntityConnection<ConnectionStringName = "SFO", ForceUpdate=true, Pluralize=false>
    let internal sfoContext = SFOData.GetDataContext()

    let internal sfoInfo = 
        query { for data in sfoContext.SFO do
                where (not (data.Published_Airline.Contains("United Airlines"))) // again, numbers too big
                groupValBy data.Landing_Count data.Published_Airline into g
                let total = query { for group in g do sumBy group }
                select (g.Key, total)
                }
                |> Chart.Column

    // Add landing counts for a new airline.
    let internal newSfo =
        SFOData.ServiceTypes.SFO.CreateSFO(0, 200508, "RachelsAirline", "RA", "RachelsAirline", "RA", "International", "Europe", "Passenger", "Wide Body", "","", 800000)

    sfoContext.DataContext.AddObject("RawLandingsData", newSfo)
    sfoContext.DataContext.SaveChanges()
