(ns all-your-base)

(defn digits-to-number [src digits]
  (reduce #(+(* %1 src) %2) digits)
)

(defn number-to-digits-rec [number dest]
      (if (zero? number)
      '()
      (conj (number-to-digits-rec (quot number dest) dest) (mod number dest))
    )
)

(defn number-to-digits [number dest]
      (if (zero? number)
      '(0)
      (number-to-digits-rec number dest)
    )
)

(defn invalid-digit? [digit src]
    (or (< digit 0) (>= digit src))
)

(defn convert [src digits dest]
  (cond (< src 2) nil
        (< dest 2) nil
        (empty? digits) ()
        (some #(invalid-digit? % src) digits) nil
      :else (reverse 
        (number-to-digits (digits-to-number src digits) dest)
      )
  )
)
