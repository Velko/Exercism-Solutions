(ns interest-is-interesting)

(defn interest-rate
  "Select interest rate based on current balance"
  [balance]
  (cond (< balance 0) -3.213
        (< balance 1000) 0.5
        (< balance 5000) 1.621
        :else 2.475
    )
  )

(defn annual-balance-update
  "Calculate annual balance update"
  [balance]
    (+ balance
     (* (bigdec (/ (interest-rate balance) 100))
        (abs balance)))
  )

(defn amount-to-donate
  "Calculate amount to donate"
  [balance tax-free-percentage]
  (cond (> balance 0)
    (int (/ (* balance (* tax-free-percentage 2))100))
    :else 0
  )
  )