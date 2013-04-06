#r "FSharp.Data.TypeProviders"
#r "System.Data.Services.Client"

open Microsoft.FSharp.Data.TypeProviders
open System.Data.Services.Client
open Microsoft.FSharp.Linq.NullableOperators

#load "show-wpf40.fsx"
module NextflixSample = 
    type NetflixData = ODataService<"http://odata.netflix.com/Catalog/", ForceUpdate=false>
    let NetflixContext = NetflixData.GetDataContext()

    NetflixContext.DataContext.SendingRequest.Add(fun e -> printfn "%A" e.Request.RequestUri)

    let godzillamovies = 
        query { for t in NetflixContext.Titles do 
                where (t.Name.Contains "Godzilla")
                where (t.ReleaseYear ?<>? System.Nullable())
//                select (t.Name, t.ReleaseYear.Value, t.BoxArt, t.AverageRating) 
              }
        |> showGrid

#load "show-wpf40.fsx"
module NorthwindSample = 
    type Northwind = ODataService< "http://services.odata.org/Northwind/Northwind.svc/" >
    let db = Northwind.GetDataContext()
    db.DataContext.SendingRequest.Add (fun x -> printfn "requesting %A" x.Request.RequestUri)

    let ordersSortedByCustomerIDAndShipDateLatestFirst= 
        query { for o in db.Orders do
                sortBy o.CustomerID; thenByNullableDescending o.ShippedDate
                select (o.CustomerID, o.OrderID, o.ShippedDate) }
        |> showGrid
