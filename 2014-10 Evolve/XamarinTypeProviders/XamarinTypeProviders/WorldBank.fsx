#r @"../packages/FSharp.Data.2.0.15/lib/net40/FSharp.Data.dll"
#load "../packages/FSharp.Charting.0.90.7/FSharp.Charting.fsx"

module WorldBank =
    open FSharp.Data
    open System

    let wb = WorldBankData.GetDataContext()

    let countries = [|
        wb.Countries.Australia;
        wb.Countries.Algeria;
        wb.Countries.Botswana;
        wb.Countries.Chile;
        wb.Countries.Iceland;
        wb.Countries.``Saudi Arabia``;
        wb.Countries.``United States``;
        wb.Countries.Uruguay;
        wb.Countries.Ghana;
        wb.Countries.Kuwait;
        wb.Countries.``Russian Federation``;
        wb.Countries.Cambodia;
        wb.Countries.Bulgaria;
        wb.Countries.Argentina;
        wb.Countries.Denmark;
        wb.Countries.``United Arab Emirates``;
        wb.Countries.``Cote d'Ivoire``;
        |]

    let internetstats = 
        countries
            |> Array.map (fun country -> 
                            (country.Name, 
                             country.Indicators.``Population, total``.[2010], 
                             country.Indicators.``Internet users (per 100 people)``.[2010]))

    internetstats
        |> Array.maxBy (fun (name,population,internet) -> internet)

    internetstats
        |> Array.minBy (fun (name,population,internet) -> internet)

