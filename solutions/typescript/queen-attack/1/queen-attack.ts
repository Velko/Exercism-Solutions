type Position = readonly [number, number]

const ROW = 0
const COL = 1

type Positions = {
  white: Position
  black: Position
}
export class QueenAttack {
  public readonly black: Position
  public readonly white: Position

  // white: [whiteRow, whiteColumn]
  // black: [blackRow, blackColumn]
  constructor(positions: Partial<Positions> = {}) {
    this.black = positions.black ?? [0, 3];
    this.white = positions.white ?? [7, 3];

    if (this.white[ROW] < 0 || this.white[ROW] > 7 ||
        this.white[COL] < 0 || this.white[COL] > 7 ||
        this.black[ROW] < 0 || this.black[ROW] > 7 ||
        this.black[COL] < 0 || this.black[COL] > 7) {
        throw new Error('Queen must be placed on the board');
    }

    if (this.white[ROW] == this.black[ROW] &&
        this.white[COL] == this.black[COL]) {
        throw new Error('Queens cannot share the same space');
    }
  }

  toString(): string {

    let board = Array<string>(8);
    for (let r = 0; r < 8; ++r) {
        let row = Array(8).fill('_');

        if (r === this.black[ROW]) {
            row[this.black[COL]] = 'B';
        }

        if (r === this.white[ROW]) {
            row[this.white[COL]] = 'W';
        }

        board[r] = row.join(' ');
    }

    return board.join('\n');
  }

  get canAttack(): boolean {
    let distanceRows = Math.abs(this.white[ROW] - this.black[ROW]);
    let distanceCols = Math.abs(this.white[COL] - this.black[COL]);

    return distanceRows === 0 || distanceCols === 0 || distanceRows === distanceCols;
  }
}
