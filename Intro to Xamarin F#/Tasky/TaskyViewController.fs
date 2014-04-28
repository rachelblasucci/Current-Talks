namespace Tasky

open MonoTouch.UIKit
open MonoTouch.Foundation
open FSharp.Data.Sql
open System
open System.Data
open System.IO

type sql = SqlDataProvider<ConnectionString = @"Data Source=Resources/task.sqlite;Version=3;",
                                              DatabaseVendor = Common.DatabaseProviderTypes.SQLITE,
                                              ResolutionPath = @"/Library/Frameworks/Mono.framework/Libraries/mono/4.5/",
                                              IndividualsAmount = 1000,
                                              UseOptionTypes = true >


[<Register ("TaskyViewController")>]
type TaskyViewController () =
    inherit UIViewController ()

    override this.ViewDidLoad () =
        base.ViewDidLoad ()
        let ctx = sql.GetDataContext()
        let tasks = ctx.``[main].[tasks]``.Individuals.``1``
        tasks |> ignore 
