namespace EvolveXamForms.Core

open Xamarin.Forms
open System

type App = class
 static member GetMainPage =
   let lbl = new Label()
   lbl.Text <- "Hi O'Reilly!"
   lbl.VerticalOptions <- LayoutOptions.CenterAndExpand
   lbl.HorizontalOptions <- LayoutOptions.CenterAndExpand
 
   let cp = new ContentPage()
   cp.Content <- lbl
   cp
end
