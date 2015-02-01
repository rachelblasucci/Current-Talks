open System
type Message = string * AsyncReplyChannel<string> //Tuple 

let agent = MailboxProcessor<Message>.Start(fun inbox ->
    let rec loop () =
        async {
                let! (message, replyChannel) = inbox.Receive();
                replyChannel.Reply(String.Format("Received message: {0}", message))
                do! loop ()
        }
    loop ())

// PostAndReply blocks
let messageAsync = agent.PostAndAsyncReply(fun replyChannel -> "Hello", replyChannel)

Async.StartWithContinuations(messageAsync, 
        (fun reply -> printfn "Reply received: %s" reply), //continuation
        (fun _ -> ()), //exception
        (fun _ -> ())) //cancellation
