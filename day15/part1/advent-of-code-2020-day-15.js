function solve(input, limit) {
    let indexes = new Map(input.map((value, index) => [value, index + 1]));
    let bucket = NaN;
    let target = input[input.length - 1];
    for (let index = input.length; index < limit; index++) {
        target = (indexes.has(target) ? index - indexes.get(target) : 0);
        indexes.set(bucket, index);
        bucket = target;
    }
    return target;
}

