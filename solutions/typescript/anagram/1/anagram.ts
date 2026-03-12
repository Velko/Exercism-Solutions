export class Anagram {

  normalizedInput: string;
  lowerInput: string;

  constructor(input: string) {
    this.lowerInput = input.toLowerCase();
    this.normalizedInput = Anagram.normalize(input);
  }

  public matches(...potentials: string[]): string[] {
    return potentials.filter(p => Anagram.normalize(p) === this.normalizedInput
        && this.lowerInput !== p.toLowerCase());
  }

  private static normalize(input: string): string {
    let chars = [...input.toLowerCase()];
    chars.sort();
    return chars.join();
  }
}
