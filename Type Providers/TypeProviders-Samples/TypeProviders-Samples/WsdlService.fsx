#r "FSharp.Data.TypeProviders"
#r "System.ServiceModel"
#r "System.Runtime.Serialization"
#load @"../packages/FSharp.Charting.0.90.7/FSharp.Charting.fsx"

open FSharp.Charting
open System.Runtime.Serialization
open System.ServiceModel
open Microsoft.FSharp.Data.TypeProviders

// show temps by zip code for following cities
let cities =  
    [
    ("Burlington", "VT")
    ("Kensington", "MD")
    ("Port Jefferson", "NY")
    ("Panama City Beach", "FL")
    ("Knoxville", "TN")
    ("Chicago", "IL")
    ("Casper", "WY") 
    ("Denver", "CO")
    ("Phoenix", "AZ")
    ("Los Angeles", "CA")
    ("Seattle", "WA")
    ]

module CheckAddress = 
    type ZipLookup = Microsoft.FSharp.Data.TypeProviders.WsdlService<
                        ServiceUri = "http://www.webservicex.net/uszip.asmx", 
                        ForceUpdate=false, LocalSchemaFile = "ZipLookup.wsdlschema"> // cached

    // get zip for (city, state) pair
    let GetZip citySt =
        let (city, state) = citySt

        let findCorrectState (node:System.Xml.XmlNode) = 
            state = node.SelectSingleNode("STATE/text()").Value

        // return top zip code
        let results = ZipLookup.GetUSZipSoap().GetInfoByCity(city).SelectNodes("Table") 
                        |> Seq.cast<System.Xml.XmlNode> 
                        |> Seq.filter findCorrectState
        (results |> Seq.nth 0).SelectSingleNode("ZIP/text()").Value

module GetTemps = 
    type WeatherService = Microsoft.FSharp.Data.TypeProviders.WsdlService<ServiceUri = "http://wsf.cdyne.com/WeatherWS/Weather.asmx", ForceUpdate=false, LocalSchemaFile = "WeatherService.wsdlschema">

    // alias
    let weather = WeatherService.GetWeatherSoap().GetCityWeatherByZIP

    let temp_in zipList = 
        let convertCitiesToZips cityName = 
            let zip = CheckAddress.GetZip cityName
            // return triple (city, zip, temp) 
            ((weather zip).City, zip, (weather zip).Temperature)

        List.map convertCitiesToZips zipList

    let data = temp_in cities
    Chart.Bubble(data, Title="Temperature by Zip", UseSizeForLabel=false).WithYAxis(Enabled=true, Max=100000., Min=0.).WithXAxis(Enabled=true).WithDataPointLabels()
