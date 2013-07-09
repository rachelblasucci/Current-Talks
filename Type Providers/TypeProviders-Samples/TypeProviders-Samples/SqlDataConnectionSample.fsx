#r "FSharp.Data.TypeProviders"
#r "System.Data.Linq"
#load @"..\packages\FSharp.Charting.0.82\FSharp.Charting.fsx"

open Microsoft.FSharp.Data.TypeProviders
open System.Data.Linq
open FSharp.Charting

module SqlDataConnectionSample = 
    type internal CustomerData = SqlDataConnection<ConnectionStringName = "Customer", ForceUpdate=false>
    let internal CustomerContext = CustomerData.GetDataContext()

    let GetCustomersNamedRachel = 
        query { for customer in CustomerContext.TblCustomer do
                where (customer.FirstName.Equals("Rachel")) 
                select (customer.FirstName, customer.Id)}
                |> Chart.Area
