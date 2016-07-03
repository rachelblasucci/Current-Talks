#r "FSharp.Data.TypeProviders"
#r @"..\..\BasicTypeProvider\BasicTypeProvider\bin\Debug\BasicTypeProvider.dll"

open Microsoft.FSharp.Data.TypeProviders
open Samples.HelloWorldTypeProvider
open PrairieDevCon.OneType

module OneTypeSample = 
    type OneType = OnePropertyType
    let showHello = OneType.HelloProperty

module HelloWorldSample = 
    let getData = 
        let thirtysix = Type36("345345345")
        let twentytwo = Type22("twentytwo")
        let fortysix = Type46("FORTY-SIX")
        let nineteen = Type19("76")
        let ninetyeight = Type98("@halibut!")

        (thirtysix, twentytwo, fortysix, nineteen, ninetyeight)
