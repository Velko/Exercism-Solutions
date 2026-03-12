BAND_COLORS = [ "black", "brown", "red", "orange", "yellow", "green", "blue", "violet", "grey", "white"]

def value(colors):
    return BAND_COLORS.index(colors[0]) * 10 + BAND_COLORS.index(colors[1])
