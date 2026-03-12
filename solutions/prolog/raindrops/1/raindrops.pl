convert(N, Sounds) :- number_string(N, Sounds).
convert(N, Sounds) :-
    bagof(Sound, make_sound(N, Sound), SoundList),
    atomics_to_string(SoundList, Sounds).
make_sound(N, "Pling") :- 0 is mod(N, 3).
make_sound(N, "Plang") :- 0 is mod(N, 5).
make_sound(N, "Plong") :- 0 is mod(N, 7).