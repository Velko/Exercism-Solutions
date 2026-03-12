(ns squeaky-clean
  (:require [clojure.string :as str]))

(defn clean
  "Some docstring"
  [s]
  ( -> s
    (str/replace #" " "_")
    (str/replace #"\p{Cntrl}" "CTRL")
    (str/replace #"-(\p{L})" #(str/upper-case (%1 1)))
    (str/replace #"[^\p{L}_]" "")
    (str/replace #"[α-ω]" "")
  )
)
