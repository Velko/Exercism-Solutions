(ns robot-name
    (:require [clojure.string :as str]))

(defn rand-char [start limit]
    (char (+ start (rand-int limit)))
)

(defn rand-letter []
    (rand-char 65 26)
)

(defn rand-digit []
    (rand-char 48 10)
)

(defn random-name [_]
    (str/join [(rand-letter) (rand-letter) (rand-digit) (rand-digit) (rand-digit)])
)

(defn robot []
  (atom (random-name ""))  
)

(defn robot-name [bot]
  @bot 
)

(defn reset-name [bot]
  (swap! bot random-name)
)
