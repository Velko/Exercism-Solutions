#include "secret_handshake.h"
#include <algorithm>

using namespace std;

namespace secret_handshake {

    vector<string> commands(int cmd) {

        vector<string> handshake;
        
        if ((cmd & 0b00001) != 0)
            handshake.push_back("wink");

        if ((cmd & 0b00010) != 0)
            handshake.push_back("double blink");

        if ((cmd & 0b00100) != 0)
            handshake.push_back("close your eyes");

        if ((cmd & 0b01000) != 0)
            handshake.push_back("jump");

        if ((cmd & 0b10000) != 0)
            reverse(handshake.begin(), handshake.end());
        
        return handshake;
    }

    
}  // namespace secret_handshake


        