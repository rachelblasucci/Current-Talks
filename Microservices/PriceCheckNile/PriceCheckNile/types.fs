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
    
    static member ToJson (_:Product) : JsonValue = failwith ""

    static member FromJson (_:Product) =
        parseObj <| fun json -> jsonParse {
            let! sku = json .@ "sku"
            let! productid = json .@ "productId"
            let! productdescription = json .@ "productDescription"
            let! costper = json .@ "costper"
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
