﻿//IMMUTABLE, TYPE INFERENCE
//Can't mix types: 
let n = 5
//let inverse = 1.0 / n 

//automatically changes
let square y = y * y
let inverse_square y = 1.0/(square y)


// LISTS
// ANONYMOUS FUNCTIONS
// FUNCTION COMPOSITION, PIPELINING
let first_ten = [1..10] 
let evens = [2..2..10]

let plus_3 x = x + 3
let list_plus_3 = List.map plus_3

let filtered = List.filter (fun x -> x % 2 = 0)

[1..10]
  |> filtered 
  |> list_plus_3
  |> List.sum
     
let sum_evens_plus_three = 
  filtered 
    >> list_plus_3
    >> List.sum

[1..10] 
  |> sum_evens_plus_three

sum_evens_plus_three [1..10]


// RECURSION, PATTERN MATCHING, DISCRIMINATED UNIONS/OPTION
let rec factorial x = 
    match x with 
        | 0 | 1 -> 1
        | _ when x < 0 -> 0
        | _ -> x * factorial (x - 1)
factorial 10

//type 'a option = 
//    | None
//    | Some of 'a

let find_num num list = 
    let attempt = List.tryFind(fun x -> x = num) list
    match attempt with 
        | Some(num) -> printfn "Found %d!" num
        | None -> printfn "Nothing :(" 

find_num 5 [1..2..15]
find_num 3 [1;3;3;5;3;7;3;3;1]
find_num 4 [0..5..40]


// SEQUENCES
let add2 a = 
    if (a <= 0) then
        None
    else 
        Some(a, a+2)

let adding2 = Seq.unfold(fun a -> add2 a)
Seq.take 4 (adding2 4)

let filteredAdding2 = Seq.filter (fun a -> a%4 = 0) (adding2 0)
Seq.take 4 filteredAdding2

let fibonnacci = Seq.unfold(fun(a,b) ->  Some( a+b, (b, a+b) )) (0,1)
Seq.nth 4 fibonnacci 
Seq.take 4 fibonnacci


// CURRYING
let list_difference = List.map2 (fun x y -> x - y)

//minkowski metric 
let minkowski p list1 list2 =
    let abs_powered p (x:float) = abs x ** p 
    let distance = 
      list_difference list1 list2 
        |> List.map (abs_powered p) 
        |> List.sum 
    distance ** (1.0/p)

// manhattan distance: SUM(| x - y |)
let manhattan = minkowski 1.0
// euclidean distance: SQRT ( SUM(| x - y |^2) ) 
let euclidean = minkowski 2.0

let point_1 = [0.0; 0.0]
let point_2 = [3.0; 4.0]

manhattan point_1 point_2
euclidean point_1 point_2


// UNITS OF MEASURE
[<Measure>] type F // degrees Fahrenheit
[<Measure>] type C // degrees Celsius
[<Measure>] type mi // miles
[<Measure>] type km // kilometres
[<Measure>] type hr // hour

let WindChill_US (T:float<F>) (v:float<mi/hr>) = 
    35.74<F> + 0.6215 * T - 35.75<F> * float(v) ** 0.16 + 0.3965 * T * float(v) ** 0.16

let WindChill_CA (T:float<C>) (v:float<km/hr>) = 
    13.12<C> + 0.6215 * T - 11.37<C> * float(v) ** 0.16 + 0.4275 * T * float(v) ** 0.16

let NYCTemp = 6.0<F>
let NYCWindSpeed = 45.2<mi/hr>

WindChill_US NYCTemp NYCWindSpeed

let MontrealTemp = -5.0<C>
let MontrealWindSpeed = 25.2<km/hr>

WindChill_CA MontrealTemp MontrealWindSpeed

let C_to_F (C:float<C>) = C / 5.0<C> * 9.0<F> + 32.0<F>

WindChill_US (C_to_F -10.0<C>) 45.4<mi/hr>


// DECLARING OPERATORS, USING .NET CODE
open System.Collections.Generic
let (.&) left right = 
  let cache = HashSet<'a>(right, HashIdentity.Structural)
  List.filter (fun n -> cache.Contains n) left

[1..15] .& [0..3..25]
["apples"; "oranges"; "pumpkins"; "pomegranates"; "kiwi"] .& ["bears"; "tigers"; "kiwi"; "lions"; "penguins"]


//OBJECT EXPRESSIONS, INTERFACES, MULTIPLE INHERITANCE
type IHuman = 
    abstract eyeColor : string
    abstract hairColor : string

type INeighbor = 
    abstract greetOthers : string -> unit

type IFriend = 
    inherit IHuman
    inherit INeighbor
    abstract sendText : string -> unit

let Ellie = {
        new IFriend with 
            member this.sendText x = printfn "Text sent: %A" x
        interface INeighbor with
            member this.greetOthers x = printfn "%A" x
        interface IHuman with 
            member this.eyeColor = "brown"
            member this.hairColor = "black"
    }

Ellie.greetOthers "hi"

//TYPE PROVIDERS
#r "System.Runtime.Serialization"
#r "System.ServiceModel"
#r "FSharp.Data.TypeProviders"

let currentZip = "02141"

type WeatherService = Microsoft.FSharp.Data.TypeProviders.WsdlService<ServiceUri = "http://wsf.cdyne.com/WeatherWS/Weather.asmx?WSDL">

let weather = WeatherService.GetWeatherSoap().GetCityWeatherByZIP(currentZip)

let temp = weather.Temperature
let description = weather.Description
let humidity = weather.RelativeHumidity
