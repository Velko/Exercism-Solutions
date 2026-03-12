abbreviate(Sentence, Acronym) :-
    words(Sentence, Words),
    first_letters(Words, Letters),
    atomics_to_string(Letters, MixedCaseAcronym),
    string_upper(MixedCaseAcronym, Acronym).

words(Sentence, Words) :-
    split_string(Sentence, " -,.", " -_,.", Words).

first_letters(Words, Letters) :-
    maplist(first_letter, Words, Letters).
    
first_letter(Word, Letter) :-
    sub_string(Word, 0, 1, _, Letter).
