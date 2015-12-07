module Types
open Marvel.Json
open FSharp.Data
open FSharp.Data.JsonExtensions
open Newtonsoft.Json
open Marvel.EventStore

type Product = {
    Sku : string
    ProductId : int
    ProductDescription : string
    CostPer : decimal
    }

type Product with
    static member ToJson(x:Product) =
        jobj [|
            "sku" .= x.Sku
            "productId" .= x.ProductId
            "productDescription" .= x.ProductDescription
            "costper" .= x.CostPer
        |]
    static member FromJson (_:Product) =
        parseObj <| fun json -> jsonParse {
            let! sku = jget json "sku"
            let! productid = jget json "productId"
            let! productdescription = jget json "productDescription"
            let! costper = jget json "costper"
            return {
                Product.Sku = sku
                ProductId = productid
                ProductDescription = productdescription
                CostPer = costper
            }
        }

    static member EventCodec : EventCodec<Product> = jsonValueCodec "product"

type PriceCheckFailed = {
    ProductId : int
    Message : string
}
