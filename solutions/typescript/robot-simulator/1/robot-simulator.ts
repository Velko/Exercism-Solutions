export class InvalidInputError extends Error {
  constructor(message: string) {
    super()
    this.message = message || 'Invalid Input'
  }
}

const Directions = ['north', 'east', 'south', 'west']
const Advances = [[0, 1], [1, 0], [0, -1], [-1, 0]]
type Direction = typeof Directions[number]
type Coordinates = [number, number]

export class Robot {
  direction: Direction
  position: Coordinates
  
  constructor() {
    this.direction = 'north'
    this.position = [0, 0]
  }
  
  get bearing(): Direction {
    return this.direction
  }

  get coordinates(): Coordinates {
    return this.position
  }

  place(params: { x: number; y: number; direction: string }) {
    if (Directions.includes(params.direction)) {
      this.direction = params.direction as Direction
    } else {
      throw new InvalidInputError(params.direction)
    }
    this.position = [params.x, params.y]
  }

  evaluate(instructions: string) {
    for (let step of instructions) {
      switch(step) {
        case 'R':
          this.turnRight()
          break
        case 'L':
          this.turnLeft()
          break
        case 'A':
          this.advance()
          break
      }
    }
  }

  turnRight() {
    let dirIdx = Directions.indexOf(this.direction) + 1
    this.direction = Directions[dirIdx % Directions.length]
  }

  turnLeft() {
    let dirIdx = Directions.indexOf(this.direction) + Directions.length - 1
    this.direction = Directions[dirIdx % Directions.length]
  }

  advance() {
    let dirIdx = Directions.indexOf(this.direction)
    let [posX, posY] = this.position
    let [movX, movY] = Advances[dirIdx]

    this.position = [posX + movX, posY + movY]
  }
}
