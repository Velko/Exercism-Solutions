(ns matching-brackets)

(def bracket #{:square :curly :round})

; function calls itself recursively for each character in the text, until it reaches the send
; it passes along a growing/shrinking stack of expected closing brackets
(defn find-match [text expect]
  (if (empty? text)
    (empty? expect) ; arrived at the end of text. It's valid if there are no expectations left
    (let [[top & tail] text]
      (cond
        ; on opening bracket, add the matching closing one to expectation stack
        (= top \( ) (find-match tail (conj expect :round ))
        (= top \[ ) (find-match tail (conj expect :square ))
        (= top \{ ) (find-match tail (conj expect :curly ))
        
        ; on closing bracket, check if it's the one we're expecting, pop it off
        (= top \) ) (and (= (peek expect) :round ) (find-match tail (pop expect)))
        (= top \] ) (and (= (peek expect) :square ) (find-match tail (pop expect)))
        (= top \} ) (and (= (peek expect) :curly ) (find-match tail (pop expect)))
        
        ; some other character - just continue with same expectations
        :else (find-match tail expect)
      )
    )
  )
)

(defn valid? [text]  
  (find-match text [])
)