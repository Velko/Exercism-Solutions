SOUNDS = {3: "Pling", 5: "Plang", 7: "Plong"}

def convert(number):

    sounds = []
    for factor, sound in SOUNDS.items():
        if number % factor == 0:
            sounds.append(sound)

    if sounds:
        return "".join(sounds)

    return str(number)
