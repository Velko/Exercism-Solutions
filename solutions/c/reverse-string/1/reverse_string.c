#include "reverse_string.h"
#include <string.h>
#include <stdlib.h>

char *reverse(const char *value) {
    int len = strlen(value);
    char *reversed = malloc(len + 1); // result is freed by caller
    reversed[len] = '\0';

    char *t = reversed + len - 1;
    for (const char *s = value; *s; ++s, --t) {
        *t = *s;
    }
    
    return reversed;
}