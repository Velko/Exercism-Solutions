(ns leap)

(defn is-divisible-by? [num div]
  (zero? (mod num div))
)

(defn leap-year? [year]
  (or 
     (and 
          (is-divisible-by? year 4)
          (not (is-divisible-by? year 100))
     )
     (is-divisible-by? year 400)
  )
)



