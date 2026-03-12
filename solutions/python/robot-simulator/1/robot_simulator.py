EAST = 0
NORTH = 1
WEST = 2
SOUTH = 3

NUM_DIRECTIONS = 4

class Robot:
    def __init__(self, direction=NORTH, x_pos=0, y_pos=0):
        self.direction = direction
        self.coordinates = (x_pos, y_pos)

    def move(self, instructions):
        for step in instructions:
            match step:
                case 'L':
                    self.turn_left()    
                case 'R':
                    self.turn_right()
                case 'A':
                    self.advance()
    
    def turn_left(self):
        self.direction = (self.direction + 1) % NUM_DIRECTIONS
    
    def turn_right(self):
        self.direction = (self.direction + NUM_DIRECTIONS - 1) % NUM_DIRECTIONS

    def advance(self):
        self.coordinates = add_t(self.coordinates, OFFSETS[self.direction])

OFFSETS = [(1, 0), (0, 1), (-1, 0), (0, -1)]

def add_t(pos1, offset):
    (x1, y1) = pos1
    (x2, y2) = offset
    return (x1 + x2, y1 + y2)