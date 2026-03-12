#include "space_age.h"

float age(planet_t planet, int64_t seconds)
{
    double age_earth = seconds / 31557600.0;
    
    switch (planet)
    {
        case EARTH:
            return age_earth;
        case MERCURY:
            return age_earth / 0.2408467;
        case VENUS:
            return age_earth / 0.61519726;
        case MARS:
            return age_earth / 1.8808158;
        case JUPITER:
            return age_earth / 11.862615;
        case SATURN:
            return age_earth / 29.447498;
        case URANUS:
            return age_earth / 84.016846;
        case NEPTUNE:
            return age_earth / 164.79132;
        default:
            return -1;
    }
}