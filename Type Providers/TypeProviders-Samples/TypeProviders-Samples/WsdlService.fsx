#r "System.ServiceModel"
#r "System.Runtime.Serialization"
#r "System.Xml"
#r "../packages/FSharp.Data.TypeProviders/lib/net40/FSharp.Data.TypeProviders.dll"
#load @"../packages/FSharp.Charting/FSharp.Charting.fsx"

open FSharp.Charting
open System.Runtime.Serialization
open System.ServiceModel
open FSharp.Data.TypeProviders
open System.Xml

// show temps by zip code for following cities
let cities = 
    [
    ("Burlington", "VT")
    ("Kensington", "MD")
    ("Port Jefferson", "NY")
    ("Panama City Beach", "FL")
    ("Knoxville", "TN")
    ("Sandusky", "OH")
    ("Casper", "WY") 
    ("Montclair", "NJ")
    ]

module CheckAddress = 
    // Can only call once every two hours!! 
    type ZipLookup = WsdlService<
                        ServiceUri = "http://ws.cdyne.com/psaddress/addresslookup.asmx?WSDL", 
                        ForceUpdate=true, LocalSchemaFile = "ZipLookup.wsdlschema"> // cached

    // get zip for (city, state) pair
    let GetZip citySt =
        let (city, state) = citySt

        // return top zip code
        let topResult = ZipLookup.GetAddressLookupSoap().CityStateToZipCodeMatcher(city, state, true, "0") |> Seq.item 0

        topResult.ToString() //.SelectSingleNode("ZIP/text()").Value

module GetTemps = 
    type WeatherService = WsdlService<ServiceUri = "http://wsf.cdyne.com/WeatherWS/Weather.asmx">

    // alias
    let weather = WeatherService.GetWeatherSoap().GetCityWeatherByZIP

    let tempIn zipList = 
        let convertCitiesToZips cityName = 
            let zip = CheckAddress.GetZip cityName
            // return triple (city, zip, temp) 
            ((weather zip).City, zip, (weather zip).Temperature)

        List.map convertCitiesToZips zipList

    let data = tempIn cities

    Chart.Bubble(data, Title="Temperature by Zip", UseSizeForLabel=false).WithYAxis(Enabled=true, Max=100000., Min=0.).WithXAxis(Enabled=true).WithDataPointLabels()
