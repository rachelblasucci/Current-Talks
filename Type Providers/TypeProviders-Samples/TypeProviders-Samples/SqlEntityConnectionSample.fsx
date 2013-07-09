#r "FSharp.Data.TypeProviders"
#r "System.Data.Entity"
#load @"..\packages\FSharp.Charting.0.82\FSharp.Charting.fsx"

open Microsoft.FSharp.Data.TypeProviders
open System.Data.Entity
open FSharp.Charting

module SqlEntityConnectionSample = 
    type internal CustomerEntityData = SqlEntityConnection<ConnectionStringName = "Customer", ForceUpdate=false>
    let internal CustomerContext = CustomerEntityData.GetDataContext()

    let GetCustomersNamedRachel = 
        query { for customer in CustomerContext.tblCustomer do
                where (customer.FirstName.Equals("Rachel"))
                select (customer.FirstName, customer.Id) }
                |> Chart.Area
