#r "FSharp.Data.TypeProviders"
#r "System.Data.Linq"
#load @"..\packages\FSharp.Charting.0.87\FSharp.Charting.fsx"

open Microsoft.FSharp.Data.TypeProviders
open System.Data.Linq
open FSharp.Charting

module SqlDataConnectionSample = 
    type internal SFOData = SqlDataConnection<ConnectionStringName = "SFO", ForceUpdate=true>
    let internal SFOContext = SFOData.GetDataContext()

    let processDate (airlineMonth:int) = 
        let year = System.Convert.ToInt32(airlineMonth.ToString().[0..3])
        let month = System.Convert.ToInt32(airlineMonth.ToString().[4..5])
        new System.DateTime(year, month, 01)

    let SFOInfo airline = 
        let info = query { for data in SFOContext.SFO do
                            where (data.PublishedAirline = airline && data.RegionSummary = "Domestic" && data.AircraftType="Passenger")
                            groupValBy data.LandingCount data.Month into g
                            let total = query {for landing in g do sumByNullable landing}
                            where (total.HasValue)
                            sortBy g.Key
                            select (processDate g.Key, total.Value)
                            }
                            |> Seq.toArray
        if (info.Length > 0) then
            Some(Chart.Line(info, Name=airline))
        else 
            None

    let airlineList = 
        query { for data in SFOContext.SFO do
                where (data.LandingCount.HasValue)
                sortBy data.PublishedAirline
                select data.PublishedAirline
                distinct
                }
        |> Seq.toList
        |> List.filter (fun x -> x <> "United Airlines")

    let combine charts = 
        Chart.Combine(charts).WithLegend().WithXAxis(Min = 38500., Title="Month").WithYAxis(Title="Landing Count")

    let chartlist = List.map SFOInfo airlineList 
                    |> List.filter (fun x -> x.IsSome)
                    |> List.map (fun x -> x.Value)
                    |> combine
