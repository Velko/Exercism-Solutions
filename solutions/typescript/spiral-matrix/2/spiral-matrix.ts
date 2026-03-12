export function ofSize(size: number): number[][] {
    let matrix = Array.from({length: size}, () => Array(size));

    let left = 0
    let right = size - 1
    let top = 0
    let bottom = size - 1;

    for (let counter = 1; counter <= size * size; ) {

        // left-to-right at the top row
        for (let x = left; x <= right; ++x) {
            matrix[top][x] = counter++;
        }
        ++top;

        // top-to-bottom at the right column
        for (let y = top ; y <= bottom; ++y)
            matrix[y][right] = counter++;
        --right;

        // right-to-left at the bottom row
        for (let x = right ; x >= left; --x)
            matrix[bottom][x] = counter++;
        --bottom;

        // bottom-to-top at the left column
        for (let y = bottom; y >= top; --y)
            matrix[y][left] = counter++;
        ++left;
    }

    return matrix;
}
