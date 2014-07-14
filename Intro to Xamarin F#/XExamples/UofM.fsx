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

let C_to_F C = C / 5.0<C> * 9.0<F> + 32.0<F>

WindChill_US (C_to_F -10.0<C>) 45.4<mi/hr>
