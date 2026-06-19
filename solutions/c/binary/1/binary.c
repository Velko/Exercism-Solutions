#include "binary.h"

int convert(const char *input) {

    int result = 0;

    for (const char *c = input; *c; ++c) {
        result *= 2;
        switch (*c) {
            case '1':
                result += 1;
                break;
            case '0':
                break;
            default:
                return INVALID;
        }
    }

    return result;
}