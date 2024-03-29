#r @"../packages/FSharp.Data/lib/net40/FSharp.Data.dll"
#I @"../packages/RProvider/"
#load "RProvider.fsx"

open RDotNet
open FSharp.Data
open RProvider
open RProvider.``base``
open RProvider.graphics
open System

let wb = WorldBankData.GetDataContext()

let countries = [|
    wb.Countries.Australia
    wb.Countries.Algeria
    wb.Countries.Botswana
    wb.Countries.Chile
    wb.Countries.Iceland
    wb.Countries.``Saudi Arabia``
    wb.Countries.``United States``
    wb.Countries.Uruguay
    wb.Countries.Ghana
    wb.Countries.Kuwait
    wb.Countries.``Russian Federation``
    wb.Countries.India
    wb.Countries.Belgium
    wb.Countries.Cambodia
    wb.Countries.Bulgaria
    wb.Countries.Argentina
    wb.Countries.Denmark
    wb.Countries.``United Arab Emirates``
    wb.Countries.``Cote d'Ivoire``
    |]

let consumptionPC = [for country in countries -> country.Indicators.``Electric power consumption (kWh per capita)``.[2010]]
let oil = [for country in countries -> country.Indicators.``Electricity production from oil sources (% of total)``.[2010]]
let renewable = [for country in countries -> country.Indicators.``Electricity production from renewable sources, excluding hydroelectric (kWh)``.[2010]]
let hydro = [for country in countries -> country.Indicators.``Electricity production from hydroelectric sources (% of total)``.[2010]]

let data = ["consumption", R.c(consumptionPC); 
            "oil", R.c(oil); 
            "renewable", R.c(renewable); 
            "hydro", R.c(hydro)]

//let consumption = [for country in countries -> (country.Name, R.c(country.Indicators.``Electric power consumption (kWh per capita)``.Values))]
let df = R.data_frame(namedParams data)

R.plot(df)
