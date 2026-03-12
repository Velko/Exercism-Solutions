export function ofSize(size: number): number[][] {
    let matrix = new Array(size)
        .fill(undefined) // has to be fill()-ed for map() to pick it up
        .map(() => new Array<number>(size));

    let left = 0
    let right = size - 1
    let top = 0
    let bottom = size - 1;

    for (let counter = 1; counter <= size * size; ) {
        for (let x = left; x <= right; ++x) {
            matrix[top][x] = counter++;
        }
        ++top;

        for (let y = top ; y <= bottom; ++y)
            matrix[y][right] = counter++;
        --right;

        for (let x = right ; x >= left; --x)
            matrix[bottom][x] = counter++;
        --bottom;

        for (let y = bottom; y >= top; --y)
            matrix[y][left] = counter++;
        ++left;
    }

    return matrix;
}
