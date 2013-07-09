#r "FSharp.Data.TypeProviders"
#r "System.ServiceModel"
#r "System.Runtime.Serialization"
#load @"..\packages\FSharp.Charting.0.82\FSharp.Charting.fsx"

open FSharp.Charting
open System.Runtime.Serialization
open System.ServiceModel
open Microsoft.FSharp.Data.TypeProviders

let cityList =  
    [
    ("Burlington", "VT"); 
    ("Port Jefferson", "NY"); 
    ("Kensington", "MD"); 
    ("Panama City Beach", "FL")
    ("Casper", "WY"); 
    ("Phoenix", "AZ"); 
    ("Los Angeles", "CA"); 
    ]

module CheckAddress = 
    type PavAddress = Microsoft.FSharp.Data.TypeProviders.WsdlService<ServiceUri = "http://pav3.cdyne.com/PavService.svc?wsdl">

    let GetZip citySt =
        let (city, state) = citySt 
        PavAddress.Getpavsoap().GetZipCodesForCityAndState(city, state, "0972B9BB-F217-4AF7-AEB1-D4FEAE10253F").ZipCodes.[0]

module GetTemps = 
    type WeatherService = Microsoft.FSharp.Data.TypeProviders.WsdlService<ServiceUri = "http://wsf.cdyne.com/WeatherWS/Weather.asmx?WSDL">

    let temp_in cities = 
        let weather x = WeatherService.GetWeatherSoap().GetCityWeatherByZIP(x)

        let convertCitiesToZips city = 
            let zip = CheckAddress.GetZip city
            ((weather zip).City, zip, (weather zip).Temperature)

        List.map convertCitiesToZips cities

    temp_in <| cityList |> Chart.Bubble
