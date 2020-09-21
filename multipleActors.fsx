#time "on"
#r "nuget: Akka.FSharp"
#r "nuget: Akka.TestKit"

open Akka.Actor
open Akka.FSharp

// type CustomException() =
//     inherit Exception()

// type Message =
//     | Echo of string
//     | Crash

let system = ActorSystem.Create("FSharpActorModel")
let args: string array = fsi.CommandLineArgs |> Array.tail
let maxvalue = args.[0] |> int
let seqlen = args.[1] |> int

//function that calculates the square
let perfectSquare (lb: int, N: int, k: int) =
    let mutable sum = 0.0
    let mutable first = lb
    let mutable flag = 0
    while first <= N do
        for i = first to first + k - 1 do
            sum <- sum + float (float i * float i)
        let mutable sq = sqrt (float sum)
        if (sq - floor (sq) = 0.0) then
            printfn "%i" first
            flag <- 1
        sum <- 0.0
        first <- first + 1

type ProcessorMessage = Task of int * int * int

let parentActor (mailbox: Actor<_>) =
    let rec loop () =
        actor {
            let! Task (x, y, z) = mailbox.Receive()
            perfectSquare (x, y, z) |> ignore
            return! loop ()
        }

    loop ()

let childActors (lastvalue, length) =
    async {
        let ref1 = spawn system "ChildActor1" parentActor
        ref1 <! Task(1, lastvalue / 3, length)

        System.Threading.Thread.Sleep 500

        let ref2 = spawn system "ChildActor2" parentActor
        ref2 <! Task(lastvalue / 3, lastvalue / 2, length)

        System.Threading.Thread.Sleep 500

        let ref3 = spawn system "ChildActor3" parentActor
        ref3 <! Task(lastvalue / 2, lastvalue, length)

    }
    |> Async.RunSynchronously

childActors (maxvalue, seqlen)
