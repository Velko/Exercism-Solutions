#include "matching_brackets.h"

#include <stddef.h>
#include <assert.h>
#define STACK_SIZE    20

bool is_paired(const char *input) {

    char bstack[STACK_SIZE];
    size_t top = 0;
    
    for (const char *c = input; *c; ++c) {
        switch (*c) {
            case '[':
                assert(top < STACK_SIZE);
                bstack[top++] = ']';
                break;
            case '(':
                assert(top < STACK_SIZE);
                bstack[top++] = ')';
                break;
            case '{':
                assert(top < STACK_SIZE);
                bstack[top++] = '}';
                break;
            case ']':
            case ')':
            case '}':
                // underflow
                if (top == 0) return false;
                // wrong pairing
                if (bstack[--top] != *c) return false;
                break;
        }
    }

    // all is good only if stack ends up empty
    return top == 0;
}