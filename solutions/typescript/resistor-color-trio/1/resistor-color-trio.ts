export function decodedResistorValue(bands: string[]) {
  let resistance = (decode_band(bands[0]) * 10 + decode_band(bands[1])) * 10 ** decode_band(bands[2]);

  let unitsPrefix = "";
  if (resistance >= 1000) {
    unitsPrefix = "kilo";
    resistance /= 1000;
  }

  return `${resistance} ${unitsPrefix}ohms`;
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