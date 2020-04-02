module hilowgame.After

open System

let (|Char|_|) value =
  let ok, c = Char.TryParse(value)
  if ok then Some c else None

let (|High|_|) (one, two) = function
  | Char 'h' | Char 'H' when one <= two -> Some () | _ -> None

let (|Low|_|) (one, two) = function
  | Char 'l' | Char 'L' when two <= one -> Some () | _ -> None

let buildRules one two =
  let pair = (one, two)
  ((|High|_|) pair, (|Low|_|) pair)

let playGame (io : IO) =

  let rec loop streak thisCard =
    io.writef "Your current streak is %i cards.\n" streak
    io.writef "Current card is: %i.\n" thisCard

    let nextCard = io.drawCard ()
#if INTERACTIVE
    io.writef "\n(DEBUG) Next Card: %i\n\n" nextCard
#endif
    let (|High|_|), (|Low|_|) = buildRules thisCard nextCard

    io.writef "Do you go [h]igher or [l]ower (or [q]uit)? "
    match io.scanfn () with
    | High | Low ->
        io.writef "\nA lucky guess! Keep going...\n\n"
        loop (streak + 1) nextCard

    | Char 'q' | Char 'Q' ->
        io.writef "\nGoodbye! (for now... you'll be back)\n\n"

    | _ ->
        io.writef "\nThat's incorrect. The next number is: %i.\n" nextCard
        io.writef "Your streak was %i cards.\n" streak

  io.writef "***** It's ::High or Low:: time! Good luck! *****\n\n"
  loop 0 (io.drawCard ())
