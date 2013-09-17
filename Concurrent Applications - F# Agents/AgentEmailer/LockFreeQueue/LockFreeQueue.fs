namespace LockFreeQueue

type Agent<'T> = MailboxProcessor<'T>
type Fetch<'T> = AsyncReplyChannel<'T>

type Msg<'key,'value> = 
    | Push of 'key * 'value
    | Pull of 'key * Fetch<'value>

module LockFree = 
    let (lockfree:Agent<Msg<string,string>>) = Agent.Start(fun sendingInbox -> 
        let cache = System.Collections.Generic.Dictionary<string, string>()
        let rec loop () = async {
            let! message = sendingInbox.Receive()
            match message with 
                | Push (key,value) -> cache.[key] <- value
                | Pull (key,fetch) -> fetch.Reply cache.[key]
            return! loop ()
            }
        loop ()
        )
