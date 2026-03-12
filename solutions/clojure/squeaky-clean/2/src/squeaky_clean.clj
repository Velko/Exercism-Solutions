(ns squeaky-clean
  (:require [clojure.string :as str]))

(defn replace-ctrl [s]
  (apply str (map #(if (Character/isISOControl %1) "CTRL" %1) s)))

(defn clean
  "Some docstring"
  [s]
  ( -> s
    (str/replace #" " "_")
    (replace-ctrl)
    (str/replace #"-(\p{L})" #(str/upper-case (%1 1)))
    (str/replace #"[^\p{L}_]" "")
    (str/replace #"[α-ω]" "")
  )
)

