pub const QueenError = error{
    InitializationFailure,
};

pub const Queen = struct {

    row: i8,
    col: i8,
    
    pub fn init(row: i8, col: i8) QueenError!Queen {
        if (row < 0 or row > 7 or col < 0 or col > 7)
            return QueenError.InitializationFailure;
    
        return Queen {
            .row = row,
            .col = col,
        };
    }

    pub fn canAttack(self: Queen, other: Queen) QueenError!bool {
        const dist_x = self.col - other.col;
        const dist_y = self.row - other.row;
        return dist_x == 0 or dist_y == 0 or @abs(dist_x) == @abs(dist_y);
    }
};
