#r "FSharp.Data.TypeProviders"
#r "System.ServiceModel"
#r "System.Runtime.Serialization"

open System.Runtime.Serialization
open System.ServiceModel
open Microsoft.FSharp.Data.TypeProviders

type WeatherService = Microsoft.FSharp.Data.TypeProviders.WsdlService<ServiceUri = "http://wsf.cdyne.com/WeatherWS/Weather.asmx?WSDL">

type forecastReturn = WeatherService.ServiceTypes.ws.cdyne.com.WeatherWS.ForecastReturn

let test_zips = ["02134"; "90031"; "10001"]

#load "show-wpf40.fsx"
module GetTemps = 
    let temp_at zips = 
        let weather x = WeatherService.GetWeatherSoap().GetCityWeatherByZIP(x)

        List.map (fun x -> (x, (weather x).City, (weather x).Temperature)) zips 

    temp_at test_zips |> showGrid

#load "show-wpf40.fsx"
module GetForecast = 
    let forecast_at zips = 
        let forecast x = WeatherService.GetWeatherSoap12().GetCityForecastByZIP(x):forecastReturn

        List.map (fun x -> (x, (forecast x).City)) zips
    
    forecast_at test_zips |> showGrid
