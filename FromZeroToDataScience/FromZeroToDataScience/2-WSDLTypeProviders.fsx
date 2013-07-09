#r "FSharp.Data.TypeProviders"
#r "System.ServiceModel"
#r "System.Runtime.Serialization"
#load @"C:\Users\rachel\Dropbox\Github\Talks & Samples\Try F#\FromZeroToDataScience\packages\FSharp.Charting.0.82\FSharp.Charting.fsx"

open FSharp.Charting
open System.Runtime.Serialization
open System.ServiceModel
open Microsoft.FSharp.Data.TypeProviders

/// WSDL ///
let cityList =  
    [
    ("Burlington", "VT");
    ("Port Jefferson", "NY"); 
    ("Kensington", "MD"); 
    ("Panama City Beach", "FL");
    ("Casper", "WY"); 
    ("Phoenix", "AZ"); 
    ("Los Angeles", "CA"); 
    ]

module CheckAddress = 
    type ZipLookup = Microsoft.FSharp.Data.TypeProviders.WsdlService<ServiceUri = "http://www.webservicex.net/uszip.asmx?WSDL">

    let citySt = ("Phoenix", "WY")
    let GetZip citySt =
        let (city, state) = citySt
        let block = ZipLookup.GetUSZipSoap().GetInfoByCity(city)
        block.SelectSingleNode("/Table/ZIP/text()").Value


module GetTemps = 
    type WeatherService = Microsoft.FSharp.Data.TypeProviders.WsdlService<ServiceUri = "http://wsf.cdyne.com/WeatherWS/Weather.asmx?WSDL">

    let weather = WeatherService.GetWeatherSoap().GetCityWeatherByZIP

    let temp_in zipList = 
        let convertCitiesToZips city = 
            let zip = CheckAddress.GetZip city
            ((weather zip).City, zip, (weather zip).Temperature)

        List.map convertCitiesToZips cityList

    temp_in <| cityList |> Chart.Bubble

