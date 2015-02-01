namespace StockTicker

open Agents
open MonoTouch.Foundation
open MonoTouch.UIKit
open System
open System.Drawing

[<Register("StockTickerViewController")>]
type StockTickerViewController() = 
    inherit UIViewController()

    let rec DrawLine n stock (stockPriceAgent:MailboxProcessor<Message>) =
        let stockAsync = stockPriceAgent.PostAndAsyncReply id
        Async.StartWithContinuations(stockAsync, 
             (fun reply -> 
                 printfn "%s %i %f" stock n reply
                 do DrawLine (n+1) stock stockPriceAgent),
             (fun _ -> ()), // exception
             (fun _ -> ())) // cancellation

    override this.ViewDidLoad() = 
        base.ViewDidLoad()

        let stockView = new UIView(this.View.Bounds, BackgroundColor = UIColor.White)
        this.View.AddSubview stockView

        let hello = new UILabel(Frame = RectangleF(10.f, 30.f, 100.f, 20.f), Text="Hello world", TextColor=UIColor.Red)
        this.View.AddSubview hello

        DrawLine 0 "apple" AppleStockPriceAgent
        DrawLine 0 "ms" MSStockPriceAgent
        DrawLine 0 "google" GoogleStockPriceAgent
