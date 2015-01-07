#r "FSharp.Data.TypeProviders"
#r "System.Data.Linq"

#load @"../packages/FSharp.Charting.0.90.9/FSharp.Charting.fsx"

open Microsoft.FSharp.Data.TypeProviders
open System.Data.Linq
open FSharp.Charting
open System.Data.SqlClient

// Using data from SFO, find landing counts over time for domestic passenger flights by airline
module SqlDataConnectionSample = 
    type internal SFOData = SqlDataConnection<ConnectionStringName = "SFO", Pluralize=false>
    let internal SFOContext = SFOData.GetDataContext()
        
    let processDate (airlineMonth:float) = // parse airlineMonth from yyyymm
        let year = System.Convert.ToInt32(airlineMonth.ToString().[0..3])
        let month = System.Convert.ToInt32(airlineMonth.ToString().[4..5])
        new System.DateTime(year, month, 01)

    let airlineList = 
        query { for data in SFOContext.RawLandingsData do
                sortBy data.PublishedAirline
                select data.PublishedAirline
                distinct
                }
        |> Seq.filter (fun x -> not <| x.Contains("United Airlines")) // high numbers throw off the chart

    // find (month, landingCount) for domestic, passenger flights by airline param
    // return line chart
    let GetData airline = 
        let info = query { for data in SFOContext.RawLandingsData do
                            where (data.PublishedAirline = airline && data.GEOSummary = "Domestic" && data.LandingAircraftType="Passenger")
                            groupValBy data.LandingCount data.ActivityPeriod into g
                            let total = query {for landing in g do sumBy landing}
                            sortBy g.Key
                            select (processDate g.Key, total)
                            }
                            |> Seq.toArray
        if (info.Length > 0) then
            Some(Chart.Line(info, Name=airline))
        else 
            None

    let combine charts = 
        Chart.Combine(charts).WithLegend().WithXAxis(Min = 38500., Title="Month").WithYAxis(Title="Landing Count")

    let chartlist = Seq.map GetData airlineList 
                    |> Seq.filter (fun x -> x.IsSome)
                    |> Seq.map (fun x -> x.Value)
                    |> combine
