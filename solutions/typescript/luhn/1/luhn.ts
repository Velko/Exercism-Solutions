export function valid(digitString: string): boolean {
  let ndigits = 0;
  let csum1 = 0;
  let csum2 = 0;
  
  for (const digit of digitString) {
    if (/\d/.test(digit)) {
      ndigits +=1;
      let digVal = parseInt(digit);

      let tval2 = csum1 + digVal;

      digVal *= 2;
      if (digVal > 9) digVal -= 9;

      csum1 = csum2 + digVal;
      csum2 = tval2;
    } else if (digit === ' ') {
    } else {
      return false;
    }
  }
  return csum2 % 10 === 0 && ndigits > 1;
}
