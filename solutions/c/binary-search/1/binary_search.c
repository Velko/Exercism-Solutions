#include "binary_search.h"


const int *binary_search(int value, const int *arr, size_t length) {
    int lo = 0;
    int hi = length;

    while (lo < hi) {
        int mid = lo + (hi - lo) / 2;

        if (arr[mid] == value) return arr + mid;

        if (value > arr[mid])
            lo = mid + 1;
        else
            hi = mid;
    }

    return NULL;
}