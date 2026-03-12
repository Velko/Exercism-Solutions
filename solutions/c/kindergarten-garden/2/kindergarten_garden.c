#include "kindergarten_garden.h"
#include <string.h>

static plant_t char2plant(char c);

plants_t plants(const char *diagram, const char *student) {
    int st_idx = (student[0] - 'A') * 2;
    const char *row2 = strchr(diagram, '\n') + 1;
    
    return (plants_t){
        .plants = {
            char2plant(diagram[st_idx]),
            char2plant(diagram[st_idx + 1]),
            char2plant(row2[st_idx]),
            char2plant(row2[st_idx + 1]),
        }
    };
}

static plant_t char2plant(char c) {
    switch (c) {
        default:
        case 'C':
            return CLOVER;
        case 'G':
            return GRASS;
        case 'R':
            return RADISHES;
        case 'V':
            return VIOLETS;
    }
}
