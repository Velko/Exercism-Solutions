space_age("Mercury", AgeSec, Years) :-
    age_on_earth(AgeSec / 0.2408467, Years).

space_age("Venus", AgeSec, Years) :-
    age_on_earth(AgeSec / 0.61519726, Years).

space_age("Earth", AgeSec, Years) :-
    age_on_earth(AgeSec, Years).

space_age("Mars", AgeSec, Years) :-
    age_on_earth(AgeSec / 1.8808158, Years).

space_age("Jupiter", AgeSec, Years) :-
    age_on_earth(AgeSec / 11.862615, Years).

space_age("Saturn", AgeSec, Years) :-
    age_on_earth(AgeSec / 29.447498, Years).

space_age("Uranus", AgeSec, Years) :-
    age_on_earth(AgeSec / 84.016846, Years).

space_age("Neptune", AgeSec, Years) :-
    age_on_earth(AgeSec / 164.79132, Years).

age_on_earth(AgeSec, Years) :-
    Years is (AgeSec / 31557600).

