triangle(A, B, C, "equilateral") :-
    is_triangle(A, B, C),
    A =:= B,
    B =:= C,
    !.

triangle(A, B, C, "isosceles") :-
    is_triangle(A, B, C),
    ( 
        A =:= B;
        B =:= C;
        A =:= C
    ),
    !.
    
triangle(A, B, C, "scalene") :-
    is_triangle(A, B, C),
    A \== B,
    B \== C,
    C \== A,
    !.

is_triangle(A, B, C) :-
    A > 0, B > 0, C > 0,
    A + B >= C,
    B + C >= A,
    A + C >= B.