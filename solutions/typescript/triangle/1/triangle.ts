export class Triangle {

  sides: number[];
  
  constructor(...sides: number[]) {
    this.sides = sides;
  }

  get isEquilateral() {
    return this.isValid &&
      this.sides[0] == this.sides[1] &&
      this.sides[1] == this.sides[2];
  }

  get isIsosceles() {
    return this.isValid && (
      this.sides[0] == this.sides[1] ||
      this.sides[1] == this.sides[2] ||
      this.sides[2] == this.sides[0]
    );
  }

  get isScalene() {
    return this.isValid &&
      this.sides[0] != this.sides[1] &&
      this.sides[1] != this.sides[2] &&
      this.sides[2] != this.sides[0];
  }

  get isValid() {
    return this.sides[0] > 0 && this.sides[1] > 0 && this.sides[2] >  0 &&
        this.sides[0] + this.sides[1] > this.sides[2] &&
        this.sides[1] + this.sides[2] > this.sides[0] &&
        this.sides[0] + this.sides[2] > this.sides[1];
  }
}
