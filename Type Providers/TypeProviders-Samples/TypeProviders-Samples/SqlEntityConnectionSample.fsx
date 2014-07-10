#r "FSharp.Data.TypeProviders"
#r "System.Data.Linq"
#r "System.Data.Entity"
#load @"..\packages\FSharp.Charting.0.90.6\FSharp.Charting.fsx"

open Microsoft.FSharp.Data.TypeProviders
open System.Data.Linq
open System.Data.Entity
open FSharp.Charting

// Sum & graph landing counts by airline -- all flights, for all time 
module SqlDataConnectionSample = 
    type internal SFOData = SqlEntityConnection<ConnectionStringName = "SFO", ForceUpdate=true, Pluralize=false, SuppressForeignKeyProperties=false>
    let internal SFOContext = SFOData.GetDataContext()

    let internal SFOInfo = 
        query { for data in SFOContext.SFO do
                where (not (data.Published_Airline.Contains("United Airlines"))) // again, numbers too big
                groupValBy data.Landing_Count data.Published_Airline into g
                let total = query { for group in g do sumBy group }
                select (g.Key, total)
                }
                |> Chart.Column

    // Add landing counts for a new airline.
    let internal NewSFO =
        SFOData.ServiceTypes.SFO.CreateSFO(201407., "AirBerlin", "AirBerlin", "International", "Europe", "Passenger", "Wide Body", 400000., 400000., "2014", "July", 0)

    SFOContext.DataContext.AddObject("SFO", NewSFO)
    SFOContext.DataContext.SaveChanges()
