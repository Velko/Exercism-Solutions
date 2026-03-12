isogram(Phrase) :-
    string_lower(Phrase, LowerPhrase),
    string_codes(LowerPhrase, Chars),
    include(only_alpha, Chars, Letters),
    is_set(Letters).

only_alpha(Char)
    :- char_type(Char, alpha).


