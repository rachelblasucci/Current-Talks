namespace Tasky 
open FSharp.Data.Sql
open System
open System.Data
open System.IO

module Data = 

    type sql = SqlDataProvider<ConnectionString = @"Data Source=/Users/rachel/Dropbox/Code/Github/Current-Talks/Intro to Xamarin F#/Tasky/Resources/task.sqlite;Version=3;",
                               DatabaseVendor = Common.DatabaseProviderTypes.SQLITE,
                               ResolutionPath = @"/Library/Frameworks/Mono.framework/Libraries/mono/4.5/",
                               IndividualsAmount = 1000,
                               UseOptionTypes = true>

    type task = { Description : string; Complete : int64; }

    let ctx = sql.GetDataContext()

    let GetIncompleteTasks = 
        ctx.``[main].[tasks]``
            |> Seq.filter (fun t -> t.complete = 0L)
            |> Seq.map (fun t -> {Description=t.task; Complete=t.complete})
            |> Seq.toArray

    let AddTask description = 
        let newtask = {Description = description; Complete = 0L}
        newtask

