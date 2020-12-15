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

let submit = document.getElementById("submit");
submit.addEventListener("click", function (event) {
    document.getElementById('output').innerHTML = "Running...";
    window.setTimeout(function () {
        let inputElement = document.getElementById("input");
        let limitElement = document.getElementById("limit");
        let input = inputElement.value.split(",").map(v => parseInt(v));
        let limit = parseInt(limitElement.value);
        var commenced = new Date().valueOf();
        let result = solve(input, limit);
        var completed = new Date().valueOf();
        let output = `Solution: ${result}<br />Elapsed time: ${completed - commenced}ms`;
        document.getElementById('output').innerHTML = output;
    }, 100);
    return (false);
});
