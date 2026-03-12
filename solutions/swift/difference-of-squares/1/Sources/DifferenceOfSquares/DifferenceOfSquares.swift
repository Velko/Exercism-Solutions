class Squares {
    var upperLimit: Int
    init(_ upTo: Int) {
        upperLimit = upTo
    }

    var squareOfSum: Int {
        var acc: Int = 0
        for n in 1...upperLimit {
            acc += n
        }

        return acc * acc
    }

    var sumOfSquares: Int {
        var acc: Int = 0
        for n in 1...upperLimit {
            acc += n * n
        }

        return acc
    }

    var differenceOfSquares: Int {
        var acc: Int = 0
        for j in 1..<upperLimit {
            for i in j+1...upperLimit {
                acc += i * j
            }
        }

        return acc * 2
    }
}
