namespace Minesweeper

open System
open System.Drawing
open MonoTouch.UIKit
open MonoTouch.Foundation

open utils

type MinesweeperButton(data) =
    inherit UIButton()
    member m.Data : MinesweeperData = data

[<Register ("MinesweeperViewController")>]
type MinesweeperViewController () =
    inherit UIViewController ()

    override this.ViewDidLoad () =
        base.ViewDidLoad ()
        let StartNewGame (board:MinesweeperButton[,]) = 
            let v = new UIView(new RectangleF(0.f, 0.f, this.View.Bounds.Width, this.View.Bounds.Height))
            board |> Array2D.iter (fun b -> v.AddSubview b) 
            this.View.AddSubview v
            //this.View.BringSubviewToFront NewSliderControl

        let GetNewGameBoard() = 
            let CreateButtons (u:MinesweeperData) = 
                let ub = new MinesweeperButton(u)
                ub.Frame <- new RectangleF((float32)ub.Data.i*(ButtonSize+ButtonPadding)+25.f, (float32)ub.Data.j*(ButtonSize+ButtonPadding)+25.f, (float32)ButtonSize, (float32)ButtonSize)
                //ub.TouchUpInside.AddHandler MinesweeperButtonClicked
                ub.BackgroundColor <- UIColor.LightGray
                ub.SetImage(null, UIControlState.Normal)
                ub.SetTitle(ub.Data.SurroundingMines.ToString(), UIControlState.Normal)
                ub

            GetClearBoard()
                |> Array2D.map (fun unknownData -> CreateButtons unknownData)
            
        StartNewGame <| GetNewGameBoard()
