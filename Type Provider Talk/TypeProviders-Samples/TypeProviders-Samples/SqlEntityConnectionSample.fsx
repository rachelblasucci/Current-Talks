#r "FSharp.Data.TypeProviders"
#r "System.Data.Entity"

open Microsoft.FSharp.Data.TypeProviders
open System.Data.Entity

#load "show-wpf40.fsx"

module SqlEntityConnectionSample = 
    type internal CustomerEntityData = SqlEntityConnection<ConnectionStringName = "Customer", ForceUpdate=false>
    let internal CustomerContext = CustomerEntityData.GetDataContext()

    let GetCustomersNamedRachel = 
        query { for customer in CustomerContext.tblCustomer do
                where (customer.FirstName.Equals("Rachel")) }
                |> showGrid
