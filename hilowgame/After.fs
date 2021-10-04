module hilowgame.After

open System

let (===) expected actual =
    (Char.ToUpper expected) = (Char.ToUpper actual)

let (|Char|_|) (expected: char) value =
  let parsed, actual = Char.TryParse(value)
  if parsed && expected === actual then Some () else None

let (|High|_|) (currentCard, nextCard) = function
  | Char 'h' when nextCard >= currentCard -> Some () | _ -> None

let (|Low|_|) (currentCard, nextCard) = function
  | Char 'l' when nextCard <= currentCard -> Some () | _ -> None

let (|Quit|_|) = function
  | Char 'q' -> Some () | _ -> None

let playGame (io : IO) =

  let rec loop streak currentCard =
    io.writef "Your current streak is %i cards.\n" streak
    io.writef "Current card is: %i.\n" currentCard

    let nextCard = io.drawCard ()
#if INTERACTIVE
    io.writef "\n(DEBUG) Next Card: %i\n\n" nextCard
#endif
    let (|HighOk|_|) = (|High|_|) (currentCard, nextCard)
    let (|LowOk|_|) = (|Low|_|) (currentCard, nextCard)

    io.writef "Do you go [h]igher or [l]ower (or [q]uit)? "
    match io.scanfn () with
    | HighOk | LowOk ->
        io.writef "\nA lucky guess! Keep going...\n\n"
        loop (streak + 1) nextCard

    | Quit ->
        io.writef "\nGoodbye! (for now... you'll be back)\n\n"

    | _ ->
        io.writef "\nThat's incorrect. The next number is: %i.\n" nextCard
        io.writef "Your streak was %i cards.\n" streak

  io.writef "***** It's ::High or Low:: time! Good luck! *****\n\n"
  loop 0 (io.drawCard ())
