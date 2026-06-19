#include "queen_attack.h"
#include <stdlib.h>

attack_status_t can_attack(position_t queen_1, position_t queen_2) {
    if (queen_1.row >= 8 || queen_1.column >= 8 || queen_2.row >= 8 || queen_2.column >= 8)
        return INVALID_POSITION;

    int deltaX = queen_1.column - queen_2.column;
    int deltaY = queen_1.row - queen_2.row;

    if (deltaX == 0 && deltaY == 0)
        return INVALID_POSITION;

    if (deltaX == 0 || deltaY == 0 || abs(deltaX) == abs(deltaY))
        return CAN_ATTACK;

    return CAN_NOT_ATTACK;
}