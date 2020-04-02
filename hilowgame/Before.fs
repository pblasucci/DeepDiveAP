module hilowgame.Before

open System

let playGame (io : IO) =
  let rec loop streak thisCard =
    let nextCard = io.drawCard ()
#if INTERACTIVE
    io.writef "\n(DEBUG) Next Card: %i\n\n" nextCard
#endif
    io.writef "Your streak is %i cards.\n" streak
    io.writef "The current card is: %i.\n" thisCard

    io.writef "Do you go [h]igher or [l]ower (or [q]uit)? "
    match Char.TryParse(io.scanfn ()) with
    | (true, 'h') | (true, 'H') when thisCard <= nextCard ->
        io.writef "\nA lucky guess! Keep going...\n\n"
        loop (streak + 1) nextCard

    | (true, 'l') | (true, 'L') when nextCard <= thisCard ->
        io.writef "\nA lucky guess! Keep going...\n\n"
        loop (streak + 1) nextCard

    | (true, 'q') | (true, 'Q') ->
        io.writef "\nGoodbye! (for now... you'll be back)\n\n"

    | _ ->
        io.writef "\nThat's incorrect. The next number is: %i.\n" nextCard
        io.writef "Your streak was %i cards.\n" streak

  io.writef "***** It's ::High or Low:: time! Good luck! *****\n\n"
  loop 0 (io.drawCard ())
