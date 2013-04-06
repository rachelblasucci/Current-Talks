namespace IntroSamples
//TYPE PROVIDERS
#r "System.Runtime.Serialization"
#r "System.ServiceModel"
#r "FSharp.Data.TypeProviders"

    module stuff = 

        [<Measure>] type C
        [<Measure>] type F
        let C_to_F (C:float<C>) = C / 5.0<C> * 9.0<F> + 32.0<F>

        let sums = [1..10] |> List.map (fun x -> x * x ) |> List.sum

    module typeproviders = 
        let currentZip = "05401"

        type WeatherService = Microsoft.FSharp.Data.TypeProviders.WsdlService<ServiceUri = "http://wsf.cdyne.com/WeatherWS/Weather.asmx?WSDL">

        type forecast = WeatherService.ServiceTypes.ws.cdyne.com.WeatherWS.Forecast

        let weather = WeatherService.GetWeatherSoap().GetCityWeatherByZIP(currentZip)

        let somethingelse = weather.Remarks
        let temp = weather.Temperature
        let description = weather.Description
        let humidity = weather.RelativeHumidity

