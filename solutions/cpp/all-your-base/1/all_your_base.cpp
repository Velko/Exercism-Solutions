#include "all_your_base.h"

#include <stdexcept>
#include <algorithm>

using namespace std;

namespace all_your_base {

    vector<unsigned int> convert(unsigned int in_base, vector<unsigned int> in_digits, unsigned int out_base)
    {
        if (in_base < 2)
            throw invalid_argument("in_base");
        
        if (out_base < 2)
            throw invalid_argument("out_base");

        unsigned int value = 0;
        for (auto digit : in_digits) {
            if (digit >= in_base)
                throw invalid_argument("in_digits");
            
            value *= in_base;
            value += digit;
        }

        vector<unsigned int> out_digits;
        while (value > 0) {
            out_digits.push_back(value % out_base);
            value /= out_base;
        }
        reverse(out_digits.begin(), out_digits.end());
        
        return out_digits;
    }

}  // namespace all_your_base
