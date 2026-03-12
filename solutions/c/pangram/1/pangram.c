#include <stdint.h>
#include <stddef.h>

#include "pangram.h"

bool is_pangram(const char *sentence)
{
    if (sentence == NULL)
        return false;

    uint32_t mask = 0;
    for (const char *p = sentence; *p; ++p)
    {
        char c = *p | 0x20; // to lowercase

        if (c >= 'a' && c <= 'z')
            mask |= 1 << (c - 'a');
    }

    return mask == (1 << 26) - 1;
}