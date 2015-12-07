// Check prices on Nile web site for a product. ;-) 

#load "refs.fsx"
#load "types.fs"
#load "logging.fs"
open System
open EventStore.ClientAPI
open Marvel.EventStore
open Marvel
open Types
open Logging

let [<Literal>] Service = "Check_price_on_Nile"

type Input = 
  | Product of Product

type Output = 
  | ProductPriceNile of Product * decimal
  | ProductPriceCheckFailed of PriceCheckFailed

let handle (input:Input) =
    async {
        return Some(ProductPriceNile({Sku="343434"; ProductId = 17; ProductDescription = "My amazing product"; CostPer=1.96M}, 3.96M))
    }

let interpret id output =
    match output with
    | Some (Output.ProductPriceNile (e, price))  -> async {()} // write to event store
    | Some (Output.ProductPriceCheckFailed e) -> async {()} // log failure
    | None -> async.Return ()

let consume = 
    EventStoreQueue.consume (decodeT Input.Product) handle interpret
    |> catchLogThrow Log

EventStoreQueue.subscribeBufferedWithCheckpointStream (EventStore.connHost "MyEventStoreConnection") "$myeventstream" true 500 100 (TimeSpan.FromSeconds 1.0) Service
|> AsyncSeq.concatSeq
|> AsyncSeq.iterAsyncParThrottled 200 consume
