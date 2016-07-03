#load @"../packages/FsLab/FsLab.fsx" 
#load "keys.fs"

open FSharp.Data
open keys

// Get data
let wb = WorldBankData.GetDataContext()

[<Literal>] 
let Path = __SOURCE_DIRECTORY__ + "\example.json"

type Venues = JsonProvider<Path>

// Parse venue data
let foursquareBaseUrl = "https://api.foursquare.com/v2/venues/explore?client_id=" + keys.ClientId + "&client_secret=" + keys.ClientSecret + "&v=20150907"
let nearPlaceUrl = foursquareBaseUrl + "&near="
let getVenuesFor city = 
    if city = "" then None
    else 
        let venues = 
            try 
                Some(Venues.Load(nearPlaceUrl + city))
            with 
                | _ -> None

        venues
        |> Option.map (fun v -> v.Response.Groups)
        |> Option.bind 
            (fun groups -> 
                groups 
                |> Array.filter (fun g -> g.Name = "recommended" && g.Items.Length > 0)
                |> Array.map (fun g -> g.Items)
                |> Array.tryHead
                |> Option.bind 
                    (fun items -> 
                        items 
                        |> Array.tryHead 
                        |> Option.map (fun item -> item.Venue.Name)))

// Get top venue by capital city
[for country in wb.Countries -> 
    let city = country.CapitalCity
    if city <> "" then 
        getVenuesFor city
        |> Option.iter (fun v -> printfn "Top recommended foursquare venue in %s, %s is %s" city country.Name v)]
