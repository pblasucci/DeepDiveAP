module colorspace.Program

open System.Drawing
open System.Reflection

open FsCheck

[<Literal>]
let PublicStatic =
  BindingFlags.Public ||| BindingFlags.Static

type Generators =
  static member Color : Arbitrary<Color> =
    Arb.fromGen (Gen.oneof [|
      for property in typeof<Color>.GetProperties(PublicStatic) do
        let color = property.GetValue(null) :?> _
        Gen.constant color
    |])

let test color =
  let before = Before.getColorSpaces color
  let after = After.getColorSpaces color
  (before.HSV = after.HSV) |@ sprintf "%A <> %A" before.HSV after.HSV

[<EntryPoint>]
let main _ =
  Arb.register<Generators> () |> ignore
  Check.Quick(test)
  0 // okay
