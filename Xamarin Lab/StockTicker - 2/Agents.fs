namespace StockTicker

open System
open System.Threading

type Message = AsyncReplyChannel<float>

module Agents = 
    let rand = new Random()

    let stockAgent price = 
        MailboxProcessor<Message>.Start(fun inbox ->
            let rec loop value =
                async {
                        do! Async.Sleep 2
                        let! replyChannel = inbox.Receive()
                        let next = rand.NextDouble()
                        let sign = if rand.Next()%2-1 = 0 then 1. else -1.
                        let revalue = value + sign * next
                        replyChannel.Reply revalue
                        return! loop revalue
                }
            loop price)

    let AppleStockPriceAgent = stockAgent 3.
    let MSStockPriceAgent = stockAgent 4.
    let GoogleStockPriceAgent = stockAgent 5.
