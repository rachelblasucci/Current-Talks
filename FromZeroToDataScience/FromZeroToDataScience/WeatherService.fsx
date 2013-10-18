#r "System.Runtime.Serialization"
#r "System.ServiceModel"
#r "Fsharp.Data.TypeProviders"
open Microsoft.FSharp.Data.TypeProviders

(* ____________________________________________________________________ *)

type WeatherService = WsdlService<ServiceUri      = "http://wsf.cdyne.com/WeatherWS/Weather.asmx"
                                 ,LocalSchemaFile = "WeatherService.wsdlschema"
                                 ,ForceUpdate     = false>
let weatherProxy = WeatherService.GetWeatherSoap12()

let (|Float|) value =
  match System.Double.TryParse value with
  | true ,value -> Float value
  | false,_     -> Float 0.0

let displayForecast zipCode =
  let forecast = weatherProxy.GetCityForecastByZIP zipCode
  if  forecast.Success
    then  printfn "forecast for %s, %s" forecast.City forecast.State
          for result in forecast.ForecastResult do
            let (Float(low ))  = result.Temperatures.MorningLow
            let (Float(high))  = result.Temperatures.DaytimeHigh
            let (Float(rain1)) = result.ProbabilityOfPrecipiation.Daytime 
            let (Float(rain2)) = result.ProbabilityOfPrecipiation.Nighttime 
            printfn "[%s] High: %05.2f, Low: %05.2f, Rain: %05.2f%%"
                    (result.Date.ToShortDateString())
                    high
                    low
                    (rain1 + rain2 / 2.0) 
    else  printfn "unable to retrieve forecast data"

displayForecast "10118"
displayForecast "07054"
