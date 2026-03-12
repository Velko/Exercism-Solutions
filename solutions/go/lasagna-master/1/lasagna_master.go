package lasagna

func PreparationTime(layers []string, averageMinutesPerLayer int) int {
    if averageMinutesPerLayer == 0 {
        averageMinutesPerLayer = 2
    }

    return len(layers) * averageMinutesPerLayer
}

func Quantities(layers []string) (noodles int, sauce float64) {
	noodles = 0
    sauce = 0.0

	for _, layer := range layers {
        switch layer {
            case "noodles":
        		noodles += 50
            case "sauce":
        		sauce += 0.2
        }
    }

    return
}

func AddSecretIngredient(friendsList []string, myList []string) {
    myList[len(myList)-1] = friendsList[len(friendsList)-1]
}

func ScaleRecipe(quantities []float64, portions int) []float64 {
    scaledQuantities := make([]float64, len(quantities))
    
    for i, quantity := range quantities {
        scaledQuantities[i] = quantity / 2.0 * float64(portions)
    }
   
    return scaledQuantities
}
