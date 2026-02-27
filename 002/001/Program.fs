open System

let rec R () = 
    let x = Console.ReadLine()
    if x = ""
        then 0
        else 1 + R ()

printfn "Кол-во строк: %i" (R ())
