(ns difference-of-squares)

(defn sum-from-to [start end]
    (reduce + (range start (inc end)))
)

; square-of-sum is actually sum-of-squares + "something". That "something" is exactly what we need to calculate.
;
; (a+b+c+d)*(a+b+c+d) => (a*a + a*b + a*c + a*d) + (b*a + b*b + b*c + b*d) + (c*a + c*b + c*c + c*d) + (d*a + d*b + d*c + d*d)
;                     => (a*a + a*b + a*c + a*d) + (a*b + b*b + b*c + b*d) + (a*c + b*c + c*c + c*d) + (a*d + b*d + c*d + d*d)
;                     => (2*a*b + 2*a*c + 2*a*d + 2*b*c + 2*b*d + 2*c*d) + (a*a + b*b + c*c + d*d)
;                     => 2 * (a*b + a*c + a*d + b*c + b*d + c*d) + (a*a + b*b + c*c + d*d)
;                     => 2 * (a * ( b + c + d) + b * (c + d) + c * (d)) + (a*a + b*b + c*c + d*d)
;                                                                          ^^^^^^^^^^^^^^^^^^^^^^
;                                                                               sum-of-squares
(defn difference [number]
    (* 2 
        (reduce + 
            (map #(* % (sum-from-to (inc %) number))
                 (range 1 number)
            )
        )
    )
)

(defn square [number]
  (* number number)
)

(defn sum-of-squares [number]
  (reduce + (map square (range 1 (inc number))))
)

(defn square-of-sum [number]
  (square (sum-from-to 1 number))
)

