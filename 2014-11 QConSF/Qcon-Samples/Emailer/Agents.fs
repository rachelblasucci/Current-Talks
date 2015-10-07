#nowarn "40"
namespace Emailer 
open System.Linq

module Agents = 
    open Types

    let couldfail number (email:string) = 
        if (email.Contains "an") then
            failwith <| "Cannot send to email " + email
        if (email.Contains "en") then
            failwith <| "Templating failed for message " + number.ToString() + " to " +  email

    /// ERROR QUEUE 
    let rec errorAgent = Agent.Start(fun errorInbox -> 
        let getId = function 
            | EmailType1Message e1 -> e1.Number
            | EmailType2Message e2 -> e2.Number
            | EmailType3Message e3 -> e3.Number            

        let setEmail change message = 
            match message with  
                | EmailType1Message e1 -> e1.Email <- change e1.Email
                | EmailType2Message e2 -> e2.Email <- change e2.Email
                | EmailType3Message e3 -> e3.Email <- change e3.Email
            message

        let rec loop () = async {
            errorInbox.Scan(fun (message:string, number) ->
                let replaceAn = setEmail (fun x -> x.Replace("an", "a"))
                let actionAn = async {
                    Data.GetData 
                        |> Array.choose (fun x -> if (number = getId x) then Some(x) else None)
                        |> Array.map replaceAn // because "failure" is simulated by checking for 'an' 
                        |> (filterAgent:Agent<Message array>).Post }
                let actionEn = async { printfn "Message %d failed permanently." number }

                if (message.Contains "Cannot send") then
                    printfn "Retrying failure: %s" message 
                    Some(actionAn)
                else if (message.Contains "Templating") then
                    Some(actionEn)
                else
                    None
                ) |> Async.RunSynchronously
            return! loop ()
            }
        loop ())

    /// SENDING AGENT
    and sendingAgent = Agent.Start(fun sendingInbox -> 
        let rec loop () = async {
            let! message, email, number = sendingInbox.Receive()

            // Oh no! Pending failure!
            try 
                couldfail number email 
                printfn "Message number %d to %s sent" number email
            with 
                | Failure(msg) -> errorAgent.Post (msg, number)

            return! loop ()
            }
        loop ()
        )

    /// TEMPLATING AGENT
    and templateAgent templateFn :Agent<'EmailType array> = Agent.Start(fun typeInbox -> 
        let rec loop () = async {
            let! typeMessages = typeInbox.Receive()

            // template
            let emails = Array.Parallel.map templateFn typeMessages

            // post to sending agent
            Array.Parallel.iter sendingAgent.Post emails
            return! loop ()
            }
        loop ()
        )

    and EmailType1Agent = templateAgent Templates.templateType1
    and EmailType2Agent = templateAgent Templates.templateType2
    and EmailType3Agent = templateAgent Templates.templateType3

    /// FILTERING AGENT
    and filterAgent = Agent.Start(fun filterInbox -> 
        let isEmailType num = function 
            | EmailType1Message _ -> 1 = num
            | EmailType2Message _ -> 2 = num
            | EmailType3Message _ -> 3 = num

        let extract msg =
            unbox<_> (match msg with
                      | EmailType1Message e1 -> box e1
                      | EmailType2Message e2 -> box e2
                      | EmailType3Message e3 -> box e3)
            
        let rec loop () = async {
            let! messageArray = filterInbox.Receive()

            let type1,other = Array.Parallel.partition (isEmailType 1) messageArray
            let type2,type3 = Array.Parallel.partition (isEmailType 2) other

            EmailType1Agent.Post <| Array.Parallel.map extract type1
            EmailType2Agent.Post <| Array.Parallel.map extract type2
            EmailType3Agent.Post <| Array.Parallel.map extract type3

            return! loop () 
            }
        loop ()
        )
