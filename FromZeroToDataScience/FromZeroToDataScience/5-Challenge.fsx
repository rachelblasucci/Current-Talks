#load @"..\packages\FSharp.Charting.0.82\FSharp.Charting.fsx"
#r @"..\packages\FSharp.Data.1.1.8\lib\net40\FSharp.Data.dll"
open FSharp.Charting
open FSharp.Data


let data = WorldBankData.GetDataContext()

let countries = data.Countries |> Seq.filter (fun x -> (x:WorldBankData.ServiceTypes.Country).Indicators.``Pump price for gasoline (US$ per liter)``.Years.Contains(2010)) 

let gas_prices = [|for country in countries -> (country.Name, country.Indicators.``Pump price for gasoline (US$ per liter)``.[2010])|]

let find_max (x:(string * float)) = 
    let (name, price) = x
    price

let sorted_prices = Array.sortBy find_max gas_prices |> Array.rev

let (highest_name, highest_price) = sorted_prices.[0]
highest_name

let top_10 = Array.sub sorted_prices 0 10 |> Chart.Column

