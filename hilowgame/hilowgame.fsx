#load "../.paket/load/netcoreapp3.1/main.group.fsx"
#load "./Library.fs"
#load "./Before.fs"
#load "./After.fs"

open hilowgame

let io = IO.get()

Before.playGame io
//After.playGame io
