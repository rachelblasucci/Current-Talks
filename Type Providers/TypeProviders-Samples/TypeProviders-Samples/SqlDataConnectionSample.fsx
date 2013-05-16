#r "FSharp.Data.TypeProviders"
#r "System.Data.Linq"

open Microsoft.FSharp.Data.TypeProviders
open System.Data.Linq

#load "show-wpf40.fsx"

module SqlDataConnectionSample = 
    type internal CustomerData = SqlDataConnection<ConnectionStringName = "Customer", ForceUpdate=false>
    let internal CustomerContext = CustomerData.GetDataContext()

    let GetCustomersNamedRachel = 
        query { for customer in CustomerContext.TblCustomer do
                where (customer.FirstName.Equals("Rachel")) }
                |> showGrid

