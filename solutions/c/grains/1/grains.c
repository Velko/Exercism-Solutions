#include "grains.h"


#define NUM_SQUARES    64

uint64_t square(uint8_t index) {
    if (index < 1 || index > NUM_SQUARES) return 0;
    return 1ULL << (index - 1);
}

uint64_t total(void) {
    return (uint64_t)-1;
}