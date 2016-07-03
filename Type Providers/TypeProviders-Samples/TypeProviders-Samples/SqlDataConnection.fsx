#r "../packages/FSharp.Data.SqlClient/lib/net40/FSharp.Data.SqlClient.dll"
#load @"../packages/FSharp.Charting/FSharp.Charting.fsx"
#load "keys.fs"

open FSharp.Charting
open FSharp.Data
open keys

// Using data from SFO, find landing counts over time for domestic passenger flights by airline
module SqlDataConnectionSample = 
    let processDate (airlineMonth:int) = // parse airlineMonth from yyyymm
        let year = System.Convert.ToInt32(airlineMonth.ToString().[0..3])
        let month = System.Convert.ToInt32(airlineMonth.ToString().[4..5])
        new System.DateTime(year, month, 01)

    let airlineList = 
        use cmd = new SqlCommandProvider<"
            SELECT DISTINCT PublishedAirline 
            FROM RawLandingsData" , connectionString>()
        // high numbers throw off the chart
        cmd.Execute() 
        |> Seq.filter (fun x -> not <| x.Contains("United Airlines")) 
        |> Seq.toArray

    // find (month, landingCount) for domestic, passenger flights by airline param
    // return line chart
    let GetData airline = 
        let info = 
            use cmd = new SqlCommandProvider<"
                SELECT ActivityPeriod, SUM(LandingCount) AS TotalLandings, Count(PublishedAirline) AS countPA
                FROM RawLandingsData
                WHERE PublishedAirline = @airline AND GEOSummary = 'Domestic' AND LandingAircraftType='Passenger'
                GROUP BY ActivityPeriod", connectionString>()
            cmd.Execute(airline = airline) 
            |> Seq.map (fun r -> ((processDate <| int r.ActivityPeriod), r.TotalLandings))
            |> Seq.filter (fun (activityDate, landings) -> landings.IsSome)
            |> Seq.map (fun (activityDate, landings) -> (activityDate, landings.Value))
            |> Seq.toArray

        if (info.Length > 0) then
            Some(Chart.Line(info, Name=airline))
        else 
            None

    let combine charts = 
        Chart.Combine(charts).WithLegend().WithXAxis(Min = 38500., Title="Month").WithYAxis(Title="Landing Count")

    let chartlist = 
        airlineList
        |> Array.map GetData  
        |> Array.filter (fun x -> x.IsSome)
        |> Array.Parallel.map (fun x -> x.Value)
        |> combine
