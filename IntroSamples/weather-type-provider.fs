module weather

open System.Runtime.Serialization
open System.ServiceModel
open Microsoft.FSharp.Data.TypeProviders

#if INTERACTIVE
#r "System.Runtime.Serialization"
#r "System.ServiceModel"
#r "FSharp.Data.TypeProviders"
#endif

let currentZip = "05401"

type WeatherService = Microsoft.FSharp.Data.TypeProviders.WsdlService<ServiceUri = "http://wsf.cdyne.com/WeatherWS/Weather.asmx?WSDL">

let weather = WeatherService.GetWeatherSoap().GetCityWeatherByZIP(currentZip)
let temp = weather.Temperature
let description = weather.Description
let humidity = weather.RelativeHumidity
