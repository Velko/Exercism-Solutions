pub fn eggCount(number: usize) usize {
    var result: usize = 0;

    for (0..@bitSizeOf(usize)) |b| {
        if (number & (@as(usize, 1) << @intCast(b)) != 0)
            result += 1;
    }

    return result;
}
