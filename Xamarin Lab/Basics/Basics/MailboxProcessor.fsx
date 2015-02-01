// 1 agent
let agent = 
        MailboxProcessor.Start(fun inbox ->
            let rec loop state =
                async {
                        let! msg = inbox.Receive()
                        printfn "got message %s, with state %i" msg state
                        return! loop (state + 1)
                }
            loop 0)

agent.Post "hello!"

// 100000 agents
let alloftheagents =
    [ for i in 0 .. 100000 ->
       MailboxProcessor.Start(fun inbox ->
         async { while true do
                   let! msg = inbox.Receive()
                   if i % 10000 = 0 then
                       printfn "agent %d got message '%s'" i msg })]

for agent in alloftheagents do
    agent.Post "ping!"
