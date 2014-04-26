#nowarn "40"
namespace Minesweeper

open System
open System.Drawing
open MonoTouch.UIKit
open MonoTouch.Foundation
open utils

type MinesweeperButton(data) =
    inherit UIButton()
    member m.Data : MinesweeperData = data

type ClearedButton(data) =
    inherit UIButton()
    member m.Data : ClearedData = data

[<Register ("MinesweeperViewController")>]
type MinesweeperViewController () =
    inherit UIViewController ()

    let NewClearedMineButton mines frame = 
        let cb = new ClearedButton(ClearedData mines)
        cb.Frame <- frame
        cb.BackgroundColor <- UIColor.DarkGray
        if mines = 0 then
            cb.SetTitle("", UIControlState.Normal)
        else 
            cb.SetTitle(mines.ToString(), UIControlState.Normal)
        cb

    override this.ViewDidLoad () =
        base.ViewDidLoad ()
        let StartNewGame (board:MinesweeperButton[,]) = 
            let v = new UIView(new RectangleF(0.f, 0.f, this.View.Bounds.Width, this.View.Bounds.Height))
            board |> Array2D.iter (fun b -> v.AddSubview b) 
            this.View.AddSubview v
            //this.View.BringSubviewToFront NewSliderControl

        let rec MinesweeperButtonClicked = 
            let GameOver (view:UIView) heading text = 
                view.RemoveFromSuperview()
                view.Dispose()
                (new UIAlertView(heading, text, null, "Okay", null)).Show()
                StartNewGame <| GetNewGameBoard()

            new EventHandler(fun sender eventargs -> 
                let ms = sender :?> MinesweeperButton
                let v = ms.Superview
                if ms.Data.IsMine then
                    GameOver v ":(" "YOU LOSE!"
                else
                    v.WillRemoveSubview(ms)
                    ms.RemoveFromSuperview()
                    ms.Dispose()
                    v.AddSubview <| NewClearedMineButton ms.Data.SurroundingMines ms.Frame
                )

        and GetNewGameBoard() = 
                let CreateButtons (u:MinesweeperData) = 
                    let ub = new MinesweeperButton(u)
                    ub.Frame <- new RectangleF((float32)ub.Data.i*(ButtonSize+ButtonPadding)+25.f, (float32)ub.Data.j*(ButtonSize+ButtonPadding)+25.f, (float32)ButtonSize, (float32)ButtonSize)
                    ub.TouchUpInside.AddHandler MinesweeperButtonClicked
                    ub.BackgroundColor <- UIColor.LightGray
                    ub.SetImage(null, UIControlState.Normal)
                    ub.SetTitle("", UIControlState.Normal)
                    ub

                GetClearBoard()
                    |> Array2D.map (fun unknownData -> CreateButtons unknownData)

        StartNewGame <| GetNewGameBoard()
