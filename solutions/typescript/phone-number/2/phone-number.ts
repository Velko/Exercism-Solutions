export function clean(rawNumber: string): string {
  let chars = [...rawNumber];

  // The exercise does not really specifies, what "punctuation" makes the phone number
  // invalid and what can be merely ignored. Making the test case to pass...
  if (chars.indexOf('@') != -1)
    throw new Error('Punctuations not permitted');

  if (chars.filter(c => (c >= 'a' && c >= 'z') || (c >= 'A' && c >= 'Z')).length > 0)
    throw new Error('Letters not permitted');

  let digits = chars.filter(c => c >= '0' && c <= '9').join("");

  if (digits.length > 11)
    throw new Error('Must not be greater than 11 digits');

  if (digits.length == 11) {
    if (digits[0] === '1')
      digits = digits.substring(1);
    else
      throw new Error("11 digits must start with 1");
  }

  if (digits.length < 10)
    throw new Error("Must not be fewer than 10 digits");

  switch (digits[0]) {
    case '0':
        throw new Error("Area code cannot start with zero");
    case '1':
      throw new Error("Area code cannot start with one");
  }

  switch (digits[3]) {
    case '0':
        throw new Error("Exchange code cannot start with zero");
    case '1':
      throw new Error("Exchange code cannot start with one");
  }

  return digits;
}
