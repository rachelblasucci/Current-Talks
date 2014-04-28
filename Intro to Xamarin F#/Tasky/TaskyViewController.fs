namespace Tasky

open MonoTouch.UIKit
open MonoTouch.Foundation
open FSharp.Data.Sql
open System
open System.Data
open System.IO

type sql = SqlDataProvider<ConnectionString = @"Data Source=/Users/rachel/Dropbox/Code/Github/Current-Talks/Intro to Xamarin F#/Tasky/Resources/task.sqlite;Version=3;",
                                              DatabaseVendor = Common.DatabaseProviderTypes.SQLITE,
                                              ResolutionPath = @"/Library/Frameworks/Mono.framework/Libraries/mono/4.5/",
                                              IndividualsAmount = 1000,
                                              UseOptionTypes = true>

type TaskDataSource(tasks:sql.SqlService.``main.tasks.Individuals``) = 
    inherit UITableViewDataSource()
    member x.cellIdentifier = "TaskCell"
    override x.RowsInSection(view, section) = 2
    override x.GetCell(view, indexPath) = 
        let cell = 
            match view.DequeueReusableCell x.cellIdentifier with 
                | null -> view.DequeueReusableCell x.cellIdentifier
                | _ -> new UITableViewCell(UITableViewCellStyle.Default, x.cellIdentifier)
        cell.TextLabel.Text <- tasks.``As task``.``1, get groceries``.task.ToString()
        cell

[<Register ("TaskyViewController")>]
type TaskyViewController () =
    inherit UIViewController ()

    let addNewTask = 
        new EventHandler(fun sender eventargs -> 
            //add new task.
            0 |> ignore
        )

    override this.ViewDidLoad () =
        base.ViewDidLoad ()
        this.NavigationItem.SetRightBarButtonItem (new UIBarButtonItem(UIBarButtonSystemItem.Add, addNewTask), false)

        let ctx = sql.GetDataContext()
        let tasks = ctx.``[main].[tasks]``.Individuals

        let table = new UITableView()
        table.DataSource <- new TaskDataSource(tasks)
        this.View.Add(table)

