open System

// Считываем числа от пользователя до 1000
let readN () : int list =
    let rec plus_el sp_el =
        let value = int(Console.ReadLine())      // Считываем строку
        
        if value = 1000 then
            sp_el                                // Возвращаем список
        else
            plus_el (sp_el @ [value])            // Добавляем число
    
    plus_el []  // Начинаем с пустого списка


// Уменьшаем каждое число в списке на 1
let demotion (N: int list) : int list =
    N |> List.map (fun x -> x - 1)


// Основная программа
[<EntryPoint>]
let main argv =
    printfn "Введите числа:"
    printfn "(1000 — конец ввода)"
    
    let N = readN()             // Считываем числа от пользователя
    
    let result = demotion N     // Уменьшаем каждое число на 1
    
    printfn "Результат: %A" result
    0  