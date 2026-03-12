package binarysearch

func SearchInts(list []int, key int) int {
    lo := 0
    hi := len(list)-1

    for lo <= hi {
        i := (lo + hi) / 2
       
        if list[i] < key {
            lo = i + 1
        } else if list[i] > key {
            hi = i - 1
        } else {
        	return i
        }
    }

    return -1
}
