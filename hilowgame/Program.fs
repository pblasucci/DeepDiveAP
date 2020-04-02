module hilowgame.Program

open System
open Argu

type Arguments =
  | [<Unique>] After
with
  interface IArgParserTemplate with
    member me.Usage =
        match me with
        | After -> "run updated version of program"

let argsParser = ArgumentParser.Create<Arguments>(programName=nameof hilowgame)

[<EntryPoint>]
let main args =
  let result = argsParser.ParseCommandLine(args)
  let game = if result.Contains After then After.playGame else Before.playGame
  game (IO.get ())
  0 // okay
