namespace EffinPickle.Core.ViewModels

open Cirrious.MvvmCross.ViewModels

type FirstViewModel() = 
    inherit MvxViewModel()
    let mutable hello : string = "Hello MvvmCross"
    member this.Hello
        with get () = hello
        and set (value) =         
            hello <- value 
            this.RaisePropertyChanged("Hello")
