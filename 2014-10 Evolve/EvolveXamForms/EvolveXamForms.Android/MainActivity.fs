namespace EvolveXamForms.Android

open System
open Android.App
open Android.Content
open Android.Content.PM
open Android.Runtime
open Android.Views
open Android.Widget
open Android.OS
open Xamarin.Forms
open Xamarin.Forms.Platform.Android
open EvolveXamForms.Core

[<Activity(Label = "EvolveXamForms.Android", MainLauncher = true)>]
type MainActivity() = 
    inherit AndroidActivity()
    override this.OnCreate(bundle) = 
        base.OnCreate(bundle)
        Forms.Init(this, bundle)
        SetPage(App.GetMainPage)
