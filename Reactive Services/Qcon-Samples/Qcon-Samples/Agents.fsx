//25-30
module Agents

type Agent<'T> = MailboxProcessor<'T>

// 100k agents
let alloftheagents =
    [ for i in 0 .. 100000 ->
        Agent.Start(fun inbox ->
            async { while true do
                    let! msg = inbox.Receive()
                    if i % 10000 = 0 then
                        printfn "agent %d got message '%s'" i msg })]
 
for agent in alloftheagents do
    agent.Post "ping!"

// Also see Replier for replying. 
// Also see Scanner for scanning.
    