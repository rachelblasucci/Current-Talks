namespace TypeProviders_Samples

open FreeBaseSample
open ODataServiceSample
open WsdlServiceSample
open SqlDataConnectionSample
open SqlEntityConnectionSample
open HelloWorldSample

module main = 
    [<EntryPoint>]
    let main argv = 
        let from_sample = "helloworld"

        let zips = ["05401"; "05486"; "82601"]

        let data =  //FreeBaseSamples.getData
            match from_sample with 
            // F# standard ones
            | "odata" -> ODataServiceSample.godzillamovies |> ShowGrid
            | "sql-data" -> SqlDataConnectionSample.GetCustomersNamedRachel |> ShowGrid
            | "sql-entity" -> SqlEntityConnectionSample.GetCustomersNamedRachel |> ShowGrid
            | "wsdl" -> WsdlServiceSample.temp_at zips |> ShowGrid
            | "freebase" -> FreeBaseSample.getfliteredParticles |> ShowGrid

            // build your own: 
            | "helloworld" -> HelloWorldSample.getData |> ShowGrid
            | _ -> [("nothing", 0.0)]

        System.Console.ReadKey() |> ignore
        0 // return an integer exit code

