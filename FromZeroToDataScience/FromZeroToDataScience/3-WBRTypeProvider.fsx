// inspired/borrowed from @brandewinder: http://www.clear-lines.com/blog/post/Visualize-WorldBank-data-with-R-and-FSharp-Type-Providers.aspx
#r @"..\packages\RProvider.1.0.3\lib\RDotNet.dll"
#r @"..\packages\RProvider.1.0.3\lib\RProvider.dll"
#r @"..\packages\FSharp.Data.1.1.10\lib\net40\FSharp.Data.dll"

open RDotNet
open FSharp.Data
open RProvider
open RProvider.``base``
open RProvider.graphics
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
    wb.Countries.India;
    wb.Countries.Belgium;
    wb.Countries.Cambodia;
    wb.Countries.Bulgaria;
    wb.Countries.Argentina;
    wb.Countries.Denmark;
    wb.Countries.``United Arab Emirates``;
    wb.Countries.``Cote d'Ivoire``;
    |]

let consumptionPC = [for country in countries -> country.Indicators.``Electric power consumption (kWh per capita)``.[2010]]
let production = [for country in countries -> country.Indicators.``Electricity production (kWh)``.[2010]]
let renewable = [for country in countries -> country.Indicators.``Electricity production from renewable sources (kWh)``.[2010]]
let oil = [for country in countries -> country.Indicators.``Electricity production from oil sources (kWh)``.[2010]]

let data = ["consumption", R.c(consumptionPC); 
            "production", R.c(production); 
            "renewable", R.c(renewable); 
            "oil", R.c(oil)]

//let consumption = [for country in countries -> (country.Name, R.c(country.Indicators.``Electric power consumption (kWh per capita)``.Values))]
let df = R.data_frame(namedParams data)

R.plot(df)
