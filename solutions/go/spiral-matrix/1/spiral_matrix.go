package spiralmatrix

func SpiralMatrix(size int) [][]int {
	matrix := make([][]int, size)

    for r := range matrix {
        matrix[r] = make([]int, size)
    }

	left := 0
    right := size - 1
    top := 0
    bottom := size - 1
    counter := 1

    for counter <= size * size {
            for x := left; x <= right; x++ {
                matrix[top][x] = counter
                counter++
            }
            top++
            for y := top ; y <= bottom; y++ {
                matrix[y][right] = counter
                counter++
            }
            right--
            for x := right ; x >= left; x-- {
                matrix[bottom][x] = counter
                counter++
            }
            bottom--
            for y := bottom; y >= top; y-- {
                matrix[y][left] = counter
                counter++
            }
            left++
    }
    
    return matrix
}
