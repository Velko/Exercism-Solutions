export const reverseString = str => {
  let builder = [];

  for (let i = str.length - 1; i >= 0; --i) {
    builder.push(str[i]);
  }
  
  return builder.join("");
};
