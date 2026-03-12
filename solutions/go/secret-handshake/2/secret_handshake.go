package secrethandshake

func Reverse(input []string) {
    for i, j := 0, len(input)-1; i < j; i, j = i+1, j-1 {
        input[i], input[j] = input[j], input[i]
    }
}

func Handshake(code uint) []string {
    handshake := []string{}

	if code & 0b00001 != 0 {
        handshake = append(handshake, "wink")
    }

    if code & 0b00010 != 0 {
        handshake = append(handshake, "double blink")
    }

    if code & 0b00100 != 0 {
        handshake = append(handshake, "close your eyes")
    }

    if code & 0b01000 != 0 {
        handshake = append(handshake, "jump")
    }

    if code & 0b10000 != 0 {
        Reverse(handshake)
    }
    
    return handshake
}
