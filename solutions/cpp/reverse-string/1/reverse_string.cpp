#include "reverse_string.h"

namespace reverse_string {

    std::string reverse_string(std::string str) {
        return std::string(str.rbegin(), str.rend());
    }

}  // namespace reverse_string
