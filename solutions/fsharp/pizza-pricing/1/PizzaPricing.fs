module PizzaPricing

type Pizza = 
    | Margherita
    | Caprese
    | Formaggio
    | ExtraSauce of Pizza
    | ExtraToppings of Pizza

let rec pizzaPrice (pizza: Pizza): int =
    match pizza with
    | Margherita -> 7
    | Caprese -> 9
    | Formaggio -> 10
    | ExtraSauce basePizza -> (pizzaPrice basePizza) + 1
    | ExtraToppings basePizza -> (pizzaPrice basePizza) + 2

let orderPrice(pizzas: Pizza list): int =
    let extraFee =     
        match pizzas with
        | [ _ ] -> 3
        | [ _; _ ] -> 2
        | _ -> 0

    let rec itemsTotal(pizzas: Pizza list) =
        match pizzas with
        | [] -> 0
        | head :: tail ->
            pizzaPrice head +
            itemsTotal tail

    extraFee + itemsTotal pizzas
