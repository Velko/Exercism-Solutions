package chessboard

// Declare a type named File which stores if a square is occupied by a piece - this will be a slice of bools
type File []bool

// Declare a type named Chessboard which contains a map of eight Files, accessed with keys from "A" to "H"
type Chessboard map[string]File

// CountInFile returns how many squares are occupied in the chessboard,
// within the given file.
func CountInFile(cb Chessboard, file string) int {
	occupied := 0
	for _, val := range cb[file] {
		if val {
			occupied++
		}
	}

	return occupied
}

// CountInRank returns how many squares are occupied in the chessboard,
// within the given rank.
func CountInRank(cb Chessboard, rank int) int {
	
	if rank < 1 || rank > 8 {
		return 0
	}

	rank--  // input is 1-based, internally it's 0-based

	occupied := 0
	for _, file := range cb {
		if file[rank] {
			occupied++
		}
	}

	return occupied
}

// CountAll should count how many squares are present in the chessboard.
func CountAll(cb Chessboard) int {
	// for typical Chess board it actually should be constant, so: 
	//return 64
	
	// but since this is an exercise about iterations, let's count them
	// anyway:
	squares := 0
	for _, file := range cb {
		for range file {
			squares++
		}
	}

	return squares
}

// CountOccupied returns how many squares are occupied in the chessboard.
func CountOccupied(cb Chessboard) int {
	occupied := 0
	for _, file := range cb {
		for _, val := range file {
			if val {
				occupied++
			}
		}
	}

	return occupied
}
