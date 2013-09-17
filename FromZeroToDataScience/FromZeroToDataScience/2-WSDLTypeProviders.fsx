#r "FSharp.Data.TypeProviders"
#r "System.ServiceModel"
#r "System.Runtime.Serialization"
#load @"..\packages\FSharp.Charting.0.82\FSharp.Charting.fsx"

open FSharp.Charting
open System.Runtime.Serialization
open System.ServiceModel
open Microsoft.FSharp.Data.TypeProviders

/// WSDL ///
let cityList =  
    [
    ("Burlington", "VT");
    ("Kensington", "MD");
    ("Port Jefferson", "NY"); 
    ("Panama City Beach", "FL");
    ("Knoxville", "TN");
    ("Chicago", "IL");
    ("Casper", "WY"); 
    ("Denver", "CO");
    ("Phoenix", "AZ"); 
    ("Seattle", "WA");
    ("Los Angeles", "CA"); 
    ]

module CheckAddress = 
    type ZipLookup = Microsoft.FSharp.Data.TypeProviders.WsdlService<ServiceUri = "http://www.webservicex.net/uszip.asmx">

    let GetZip citySt =
        let (city, state) = citySt

        let findCorrectState (node:System.Xml.XmlNode) = 
            state = node.SelectSingleNode("STATE/text()").Value

        let results = ZipLookup.GetUSZipSoap().GetInfoByCity(city).SelectNodes("Table") |> Seq.cast<System.Xml.XmlNode> |> Seq.filter findCorrectState
        (results |> Seq.nth 0).SelectSingleNode("ZIP/text()").Value

module GetTemps = 
    type WeatherService = Microsoft.FSharp.Data.TypeProviders.WsdlService<ServiceUri = "http://wsf.cdyne.com/WeatherWS/Weather.asmx">

    let weather = WeatherService.GetWeatherSoap().GetCityWeatherByZIP

    let temp_in zipList = 
        let convertCitiesToZips cityName = 
            let zip = CheckAddress.GetZip cityName
            ((weather zip).City, zip, (weather zip).Temperature)

        List.map convertCitiesToZips cityList

    temp_in <| cityList |> Chart.Bubble 

