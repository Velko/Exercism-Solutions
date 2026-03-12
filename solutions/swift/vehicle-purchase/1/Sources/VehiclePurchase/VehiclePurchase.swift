func canIBuy(vehicle: String, price: Double, monthlyBudget: Double) -> String {
    let monthlyPayment: Double = price / 60;
    return monthlyPayment <= monthlyBudget ? "Yes! I'm getting a \(vehicle)" :
        monthlyPayment <= monthlyBudget * 1.1 ? "I'll have to be frugal if I want a \(vehicle)" :
        "Darn! No \(vehicle) for me"
}

func licenseType(numberOfWheels wheels: Int) -> String {
    return wheels > 1 && wheels < 4 ? "You will need a motorcycle license for your vehicle" :
        wheels == 4 || wheels == 6 ? "You will need an automobile license for your vehicle" :
        wheels == 18 ? "You will need a commercial trucking license for your vehicle" :
        "We do not issue licenses for those types of vehicles"
}

func calculateResellPrice(originalPrice: Int, yearsOld: Int) -> Int {
    return yearsOld < 3 ? originalPrice * 80 / 100 :
           yearsOld < 10 ? originalPrice * 70 / 100 :
           originalPrice * 50 / 100 
}
