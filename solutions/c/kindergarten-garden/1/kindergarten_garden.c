#include "kindergarten_garden.h"
#include <string.h>

static plant_t char2plant(char c);

plants_t plants(const char *diagram, const char *student) {
    int st_idx = (student[0] - 'A') * 2;
    const char *row2 = strchr(diagram, '\n') + 1;
    
    plants_t pl;
    pl.plants[0] = char2plant(diagram[st_idx]);
    pl.plants[1] = char2plant(diagram[st_idx + 1]);
    pl.plants[2] = char2plant(row2[st_idx]);
    pl.plants[3] = char2plant(row2[st_idx + 1]);
    
    return pl;
}

static plant_t char2plant(char c) {
    switch (c) {
        case 'G':
            return GRASS;
        case 'C':
            return CLOVER;
        case 'R':
            return RADISHES;
        case 'V':
            return VIOLETS;
    }

    return -1;
}
