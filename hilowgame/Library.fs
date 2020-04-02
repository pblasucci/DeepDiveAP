namespace hilowgame

open System

type IO =
  abstract drawCard : unit -> int
  abstract scanfn : unit -> string
  abstract writef : Printf.StringFormat<'T, unit> -> 'T

module IO =
  let get () =
    let random = Random()

    let write value =
      Console.Write(string value)
      Console.Out.Flush()

    let io = {
      new IO with
        member _.scanfn () = Console.ReadLine()
        member _.writef format = Printf.kprintf write format
        member _.drawCard () = random.Next(1, 14) // exclusive upper limit
    }
    io
