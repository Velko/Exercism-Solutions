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

(defn random-name []
    (str/join [(rand-letter) (rand-letter) (rand-digit) (rand-digit) (rand-digit)])
)

(def name-history (atom #{}))

(defn random-unique-name [_]
  (loop [new-name (random-name)]
    (if (not (contains? @name-history new-name))
      (do
        (swap! name-history conj new-name)
        new-name
      )
      (recur (random-name))
    )
  )
)

(defn robot []
  (atom (random-unique-name ""))  
)

(defn robot-name [bot]
  @bot 
)

(defn reset-name [bot]
  (swap! bot random-unique-name)
)
