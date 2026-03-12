from datetime import timedelta

GIGA_DELTA = timedelta(seconds=10**9)

def add(moment):
    return moment + GIGA_DELTA
