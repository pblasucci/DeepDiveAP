module hilowgame.After

open System

let (|Char|_|) value =
  let ok, c = Char.TryParse(value)
  if ok then Some c else None

let (|High|_|) (currentCard, nextCard) = function
  | Char 'h' | Char 'H' when nextCard >= currentCard -> Some () | _ -> None

let (|Low|_|) (currentCard, nextCard) = function
  | Char 'l' | Char 'L' when nextCard <= currentCard -> Some () | _ -> None

let (|Quit|_|) = function
  | Char 'q' | Char 'Q' -> Some () | _ -> None

let buildRules currentCard nextCard =
  let pair = (currentCard, nextCard)
  ((|High|_|) pair, (|Low|_|) pair)

let playGame (io : IO) =

  let rec loop streak currentCard =
    io.writef "Your current streak is %i cards.\n" streak
    io.writef "Current card is: %i.\n" currentCard

    let nextCard = io.drawCard ()
#if INTERACTIVE
    io.writef "\n(DEBUG) Next Card: %i\n\n" nextCard
#endif
    let (|High'|_|), (|Low'|_|) = buildRules currentCard nextCard

    io.writef "Do you go [h]igher or [l]ower (or [q]uit)? "
    match io.scanfn () with
    | High' | Low' ->
        io.writef "\nA lucky guess! Keep going...\n\n"
        loop (streak + 1) nextCard

    | Quit ->
        io.writef "\nGoodbye! (for now... you'll be back)\n\n"

    | _ ->
        io.writef "\nThat's incorrect. The next number is: %i.\n" nextCard
        io.writef "Your streak was %i cards.\n" streak

  io.writef "***** It's ::High or Low:: time! Good luck! *****\n\n"
  loop 0 (io.drawCard ())
