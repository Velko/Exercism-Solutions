from string import ascii_letters

def response(hey_bob):

    hey_bob = hey_bob.strip()

    if hey_bob == "":
        return "Fine. Be that way!"

    is_question = hey_bob[-1] == "?"
    has_letters = any(filter(lambda c: c in ascii_letters, hey_bob))
    is_shouting = hey_bob.upper() == hey_bob and has_letters

    if is_question:
        if is_shouting:
            return "Calm down, I know what I'm doing!"

        return "Sure."

    if is_shouting:
        return "Whoa, chill out!"

    return "Whatever."
