namespace Tasky

open MonoTouch.UIKit
open MonoTouch.Foundation
open Data

[<Register ("AddTaskViewController")>]
type AddTaskViewController () =
    inherit UIViewController ()

    override this.ViewDidLoad () =
        base.ViewDidLoad ()
        let label = new UILabel()
        label.Text <- "Hello World"
        this.View.Add label
