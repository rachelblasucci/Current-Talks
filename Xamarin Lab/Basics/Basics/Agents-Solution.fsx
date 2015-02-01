open System
open System.Threading

let rand = new Random()

module OriginalAgent = 
    type Message = float
    let stockAgent = 
        MailboxProcessor<Message>.Start(fun inbox ->
            let rec loop () =
                async {
                        let! price = inbox.Receive()
                        let next = rand.NextDouble()
                        let sign = if rand.Next()%2-1 = 0 then 1. else -1.
                        let revalue = price + sign * next
                        printfn "%f" revalue
                        return! loop ()
                }
            loop () )
    
    stockAgent.Post 3.

// With ReplyChannel
module ReplyChannelAgent = 
    type Message = AsyncReplyChannel<float>

    let stockAgent price = 
        MailboxProcessor<Message>.Start(fun inbox ->
            let rec loop value =
                async {
                        let! replyChannel = inbox.Receive()
                        let next = rand.NextDouble()
                        let sign = if rand.Next()%2-1 = 0 then 1. else -1.
                        let revalue = value + sign * next
                        replyChannel.Reply revalue
                        return! loop revalue
                }
            loop price)

    let AppleStockPriceAgent = stockAgent 3.

    let stockAsync = AppleStockPriceAgent.PostAndAsyncReply id

    Async.StartWithContinuations(stockAsync, 
         (fun reply -> 
             printfn "Apple: %f" reply),
         (fun _ -> ()), // exception
         (fun _ -> ())) // cancellation
