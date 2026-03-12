class QueenAttack {
  construct new() {
    _white = Queen.new([7, 3])
    _black = Queen.new([0, 3])
  }
  
  construct new(pieces) {
    _white = Queen.new(pieces.containsKey("white") ? pieces["white"] : [7, 3])
    _black = Queen.new(pieces.containsKey("black") ? pieces["black"] : [0, 3])

    if (_white == _black) Fiber.abort("Queens cannot share the same space")
  }

  white { _white.toArray }
  black { _black.toArray }
  
  
  toString {
    var board = (0..7).map {|i|List.filled(8, "_")}.toList

    board[_white.row][_white.col] = "W"
    board[_black.row][_black.col] = "B"

    var rows = board.map {|row|row.join(" ")}
    return rows.join("\n")
  }

  canAttack {
    var distX = _white.col - _black.col
    var distY = _white.row - _black.row

    return distX == 0 || distY == 0 || distX.abs == distY.abs
  }
}

class Queen {
  construct new(position) {
    _row = validateCoordinate(position[0])
    _col = validateCoordinate(position[1])
  }

  validateCoordinate(coord) {
    if (coord < 0 || coord >= 8) Fiber.abort("Queen must be placed on the board")
    return coord
  }

  toArray {
    return [_row, _col]
  }

  row { _row }
  col { _col }
  
  ==(other) {
    return _row == other.row && _col == other.col
  }
}
