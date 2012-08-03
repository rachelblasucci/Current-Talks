namespace IntroSamples

    module stuff = 

        [<Measure>] type C
        [<Measure>] type F
        let C_to_F (C:float<C>) = C / 5.0<C> * 9.0<F> + 32.0<F>

        let sums = [1..10] |> List.map (fun x -> x * x ) |> List.sum
