(ns accumulate)

(defn accumulate [fun coll]
    (if (empty? coll)
        ()
        (conj
           (accumulate
              fun
              (rest coll)
           )
           (fun (first coll))
        )
    )
)
