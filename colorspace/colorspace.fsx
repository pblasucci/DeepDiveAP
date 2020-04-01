#load "../.paket/load/netcoreapp3.1/main.group.fsx"
#load "./Before.fs"
#load "./After.fs"
#load "./Program.fs"

open FsCheck
open colorspace

let showColorSpaces count =
  for color in Arb.generate<_> |> Gen.sample 100 count do
    let one = Before.getColorSpaces color
    let two = After.getColorSpaces color
    printfn "----------------------------"
    printfn "%s" color.Name
    printfn "----------------------------"
    printfn "Before... %s; %s; %s" one.RGB one.HSL one.HSV
    printfn "After.... %s; %s; %s" two.RGB two.HSL two.HSV

Arb.register<Program.Generators> () |> ignore
showColorSpaces 3
