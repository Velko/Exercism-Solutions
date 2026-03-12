convert(N, Numeral) :-
    take_digits(1000,     N,    [],  DigM,  RemM ),
    take_digits( 900,  RemM,  DigM, DigCM,  RemCM),
    take_digits( 500, RemCM, DigCM,  DigD,  RemD ),
    take_digits( 400,  RemD,  DigD, DigCD,  RemCD),
    take_digits( 100, RemCD, DigCD,  DigC,  RemC ),
    take_digits(  90,  RemC,  DigC, DigXC,  RemXC),
    take_digits(  50, RemXC, DigXC,  DigL,  RemL ),
    take_digits(  40,  RemL,  DigL, DigXL,  RemXL),
    take_digits(  10, RemXL, DigXL,  DigX,  RemX ),
    take_digits(   9,  RemX,  DigX,  Dig9,  Rem9 ),
    take_digits(   5,  Rem9,  Dig9,  Dig5,  Rem5 ),
    take_digits(   4,  Rem5,  Dig5,  Dig4,  Rem4 ),
    take_digits(   1,  Rem4,  Dig4,  Dig1,      _),
    maplist(roman_digit, Dig1, Digits),
    atomics_to_string(Digits, Numeral).

take_digits(D, Number, Lin, Lout, Rem) :-
    (
     Number >= D,
     take_digits(D, Number - D, Lin, Prev, Rem),
     append(Prev, [D], Lout),
     !
    );(
      Lout = Lin,
      Rem is Number,
      !
    ).

roman_digit(1, "I").
roman_digit(4, "IV").
roman_digit(5, "V").
roman_digit(9, "IX").
roman_digit(10, "X").
roman_digit(40, "XL").
roman_digit(50, "L").
roman_digit(90, "XC").
roman_digit(100, "C").
roman_digit(400, "CD").
roman_digit(500, "D").
roman_digit(900, "CM").
roman_digit(1000, "M").
