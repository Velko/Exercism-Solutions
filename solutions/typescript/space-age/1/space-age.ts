export type Planet = 'mercury' |
                     'venus'   |
                     'earth'   |
                     'mars'    |
                     'jupiter' |
                     'saturn'  |
                     'uranus'  |
                     'neptune';

export function age(planet: Planet, seconds: number): number {
  let age_on_earth = seconds / 31557600;

  switch(planet) {
    case 'mercury':
      return round2Digits(age_on_earth / 0.2408467);
    case 'venus':
      return round2Digits(age_on_earth / 0.61519726);
    case 'earth':
      return round2Digits(age_on_earth);
    case 'mars':
      return round2Digits(age_on_earth / 1.8808158);
    case 'jupiter':
      return round2Digits(age_on_earth / 11.862615);
    case 'saturn':
      return round2Digits(age_on_earth / 29.447498);
    case 'uranus':
      return round2Digits(age_on_earth / 84.016846);
    case 'neptune':
      return round2Digits(age_on_earth / 164.79132);
  }
}

function round2Digits(age: number): number {
  return Math.round(age * 100) / 100;
}