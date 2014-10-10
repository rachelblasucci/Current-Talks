namespace EvolveXamForms

open System
open MonoTouch.UIKit
open MonoTouch.Foundation
open Xamarin.Forms
open EvolveXamForms.Core

[<Register("AppDelegate")>]
type AppDelegate() = 
    inherit UIApplicationDelegate()
    override val Window = null with get, set
    override this.FinishedLaunching(app, options) = 
        this.Window <- new UIWindow(UIScreen.MainScreen.Bounds)
        Forms.Init ()
        this.Window.RootViewController <- App.GetMainPage.CreateViewController()
        this.Window.MakeKeyAndVisible()
        true

module Main = 
    [<EntryPoint>]
    let main args = 
        UIApplication.Main(args, null, "AppDelegate")
        0
