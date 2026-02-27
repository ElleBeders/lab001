open System

let choice () : int =                 // Выбор действия
    printfn "Выбери действие: "
    printfn "1 - добавить el"
    printfn "2 - удалить el"
    printfn "3 - найти el"
    printfn "4 - соединить списки"
    printfn "5 - получить el по номеру"
    int(Console.ReadLine())

let f_1 (sp_1: int list) (sp_2: int list) : int list * int list =     // Добавление el
    printf "Введите el: "
    let a = int(Console.ReadLine())
    printf "Добавить в список 1 или 2: "
    if int(Console.ReadLine()) = 1
    then
        (sp_1 @ [a], sp_2)              // sp_1 обновлён, sp_2 без изменений
    else
        (sp_1, sp_2 @ [a])              // sp_1 без изменений, sp_2 обновлён

let f_2 (sp_1: int list) (sp_2: int list) : int list * int list =
    printf "Введите el: "
    let el = int(Console.ReadLine())
    
    let rec f_2_2 acc = function                // Удаляем первый el из списка
        | [] -> acc
        | x :: xs when x = el -> f_2_2 acc xs   // пропустили x, продолжаем
        | x :: xs -> f_2_2 (acc @ [x]) xs       // добавили x, продолжаем
    
    printf "Удалить из списка 1 или 2: "
    if int(Console.ReadLine()) = 1 
    then
        (f_2_2 [] sp_1, sp_2)  // обновили sp_1
    else
        (sp_1, f_2_2 [] sp_2)  // обновили sp_2

let f_3 (sp_1: int list) (sp_2: int list) : bool =
    printf "Введите el для поиска: "
    let el = int(Console.ReadLine())
    printf "Искать в списке 1 или 2: "
    let li = int(Console.ReadLine())
    
    let rec check_1 = function           // Проверяем, есть ли el в списке
        | [] -> false                     // список пуст - элемента нет
        | x :: xs when x = el -> true     // нашли элемент - true
        | _ :: xs -> check_1 xs          // продолжаем поиск
    
    if li = 1 then
        check_1 sp_1   // проверяем sp_1
    else
        check_1 sp_2  // проверяем sp_2


let f_4 (sp_1: int list) (sp_2: int list) : int list * int list =     // Добавление el
    printf "Записать соединение в список 1 или 2: "
    if int(Console.ReadLine()) = 1
    then
        (sp_1 @ sp_2, sp_2)              // sp_1 обновлён, sp_2 без изменений
    else
        (sp_1, sp_1 @ sp_2)              // sp_1 без изменений, sp_2 обновлён

let f_5 (sp_1: int list) (sp_2: int list) : Option<int> =
    printf "Искать в списке 1 или 2: "
    let li = int(Console.ReadLine())
    printf "Введите номер элемента: "
    let index = int(Console.ReadLine())
    
    let rec check_2 i = function
        | [] -> None                        // список кончился - элемента нет
        | x :: xs when i = 0 -> Some x      // нашли нужный индекс - возвращаем элемент
        | _ :: xs -> check_2 (i - 1) xs     // уменьшаем индекс, идём дальше

    if li = 1 then
        check_2 index sp_1  // ищем в sp_1
    else
        check_2 index sp_2  // ищем в sp_2


[<EntryPoint>]
let main argv =
    let rec loop sp_1 sp_2 =
        let x = choice ()
        if x = 0 then                          // Завершаем программу
            printfn "Программа завершена."
        else
            match x with                       // Обработка выбора
            | 1 ->
                let (newSp1, newSp2) = f_1 sp_1 sp_2
                printfn "Список 1: %A" newSp1
                printfn "Список 2: %A" newSp2
                loop newSp1 newSp2             // Продолжаем цикл с обновлёнными списками
                
            | 2 ->
                let (newSp1, newSp2) = f_2 sp_1 sp_2
                printfn "Список 1: %A" newSp1
                printfn "Список 2: %A" newSp2
                loop newSp1 newSp2             // Продолжаем цикл с обновлёнными списками

            | 3 ->
                let found = f_3 sp_1 sp_2
                if found 
                    then
                        printfn "Элемент найден!"
                    else
                        printfn "Элемента нет в списке."
                loop sp_1 sp_2                 // Продолжаем цикл

            | 4 ->
                let (newSp1, newSp2) = f_4 sp_1 sp_2
                printfn "Список 1: %A" newSp1
                printfn "Список 2: %A" newSp2
                loop newSp1 newSp2             // Продолжаем цикл с обновлёнными списками

            | 5 ->
                let res = f_5 sp_1 sp_2
                match res with
                    | Some value -> printfn "Найден элемент: %d" value
                    | None -> printfn "Элемент не найден"
                loop sp_1 sp_2                 // Продолжаем цикл

            | _ ->
                printfn "Введите 1, 2 или 0 (для выхода)."
                
                loop sp_1 sp_2                 // Продолжаем цикл без изменения списков

    
    loop [] []                                 // Начальный вызов цикла с пустыми списками
    0 
