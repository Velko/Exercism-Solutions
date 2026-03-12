export function decodedValue(bands: string[]) {
  return decode_band(bands[0]) * 10 + decode_band(bands[1]);
}

enum BandValue {
  black,
  brown,
  red,
  orange,
  yellow,
  green,
  blue,
  violet,
  grey,
  white 
}

function decode_band(color: string): number {
  let colorKey = color as keyof typeof BandValue;
  return BandValue[colorKey];
}