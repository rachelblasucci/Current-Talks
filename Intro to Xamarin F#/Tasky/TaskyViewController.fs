namespace Tasky

open MonoTouch.UIKit
open MonoTouch.Foundation
open System
open System.IO
open Data

type TaskDataSource(tasks: task[]) = 
    inherit UITableViewSource()
    member x.cellIdentifier = "TaskCell"
    override x.RowsInSection(view, section) = tasks.Length
    override x.GetCell(view, indexPath) = 
        let t = tasks.[indexPath.Item]
        let cell = 
            match view.DequeueReusableCell x.cellIdentifier with 
                | null -> new UITableViewCell(UITableViewCellStyle.Default, x.cellIdentifier)
                | _ -> view.DequeueReusableCell x.cellIdentifier
        cell.TextLabel.Text <- t.Description
        cell

[<Register ("TaskyViewController")>]
type TaskyViewController () as this =
    inherit UIViewController ()

    let addNewTask = 
        new EventHandler(fun sender eventargs -> 
            this.NavigationController.PushViewController <| (new AddTaskViewController(), false)
        )

    override this.ViewDidLoad () =
        base.ViewDidLoad ()
        this.NavigationItem.SetRightBarButtonItem (new UIBarButtonItem(UIBarButtonSystemItem.Add, addNewTask), false)

        let tasks = Data.GetIncompleteTasks()

        let table = new UITableView(this.View.Bounds)
        table.Source <- new TaskDataSource(tasks)
        this.View.Add table 

