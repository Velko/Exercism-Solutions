import re

def abbreviate(words):
    chars = re.findall("([A-Z])[A-Z']*", words.upper())
    return "".join(chars)
        
