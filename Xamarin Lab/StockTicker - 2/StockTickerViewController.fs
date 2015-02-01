namespace StockTicker

open Agents
open MonoTouch.Foundation
open MonoTouch.UIKit
open System
open System.Drawing
open Steema.TeeChart

[<Register("StockTickerViewController")>]
type StockTickerViewController() = 
    inherit UIViewController()

    let appleLine = new Styles.FastLine(Title = "Apple")
    let msLine = new Styles.FastLine(Title = "Microsoft")
    let googleLine = new Styles.FastLine(Title = "Google")

    let rec DrawLine n stock (stockPriceAgent:MailboxProcessor<Message>) (stockLine:Styles.FastLine) =
        let stockAsync = stockPriceAgent.PostAndAsyncReply id
        Async.StartWithContinuations(stockAsync, 
             (fun reply -> 
                 printfn "%s %i %f" stock n reply
                 stockLine.Add reply |> ignore
                 do DrawLine (n+1) stock stockPriceAgent stockLine),
             (fun _ -> ()), // exception
             (fun _ -> ())) // cancellation

    override this.ViewDidLoad() = 
        base.ViewDidLoad()

        let stockView = new UIView(this.View.Bounds, BackgroundColor = UIColor.White)
        this.View.AddSubview stockView

        DrawLine 0 "apple" AppleStockPriceAgent appleLine
        DrawLine 0 "ms" MSStockPriceAgent msLine 
        DrawLine 0 "google" GoogleStockPriceAgent googleLine

        let chart = new TChart(Frame = RectangleF(20.f, 20.f, 300.f, 300.f))

        chart.Series.Add appleLine |> ignore
        chart.Series.Add msLine |> ignore
        chart.Series.Add googleLine |> ignore

        stockView.AddSubview chart
