#r "FSharp.Data.TypeProviders"
#r @"..\..\BasicTypeProvider\BasicTypeProvider\bin\Debug\BasicTypeProvider.dll"

open Microsoft.FSharp.Data.TypeProviders
open Samples.HelloWorldTypeProvider
open Oredev.OneType

module OneTypeSample = 
    type oneType = OnePropertyType
    let showHello = oneType.HelloProperty

module HelloWorldSample = 
    let getData = 
        let thirtysix = Type36("36")
        let twentytwo = Type22("twentytwo")
        let fortysix = Type46("FORTY-SIX")
        let nineteen = Type19("19")
        let ninetyeight = Type98("@halibut!")

        (thirtysix, twentytwo, fortysix, nineteen, ninetyeight)
