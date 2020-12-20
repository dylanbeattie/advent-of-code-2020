async function solve() {
    let response = await fetch("aocday19.peg");

    if (response.ok) { // if HTTP-status is 200-299
        // get the response body (the method explained below)
        let grammar1 = await response.text();
        var parser1 = peg.generate(grammar1);

        let response2 = await fetch("example_part_2.txt");
        if (response2.ok) {
            let input = await response2.text();
            let chunks = input.split(/\r?\n\r?\n/);
            let input1 = chunks[0];
            input1 = input1.replace(/^8: 42$/m, "8: 42 | 42 8");
            input1 = input1.replace(/^11: 42 31$/m, "11: 42 31 | 42 11 31");
            input1 = input1.split(/\n/g).sort().join('\n') + "\n";
            console.log("==================================");
            console.log(input1);
            console.log("==================================");
            let grammar2 = parser1.parse(input1);

            document.write(`<pre>${grammar2}</pre>`);

            let parser2 = peg.generate(grammar2);
            console.log(parser2);
            let lines = chunks[1].split(/\r?\n/g);
            let count = 0;
            lines.forEach(line => {
                try {
                    parser2.parse(line);
                    console.log("OK", line);
                    count++;
                } catch (error) {
                    console.log("NO", line);
                    console.log(error);
                }
            });
            document.write("<h1>" + count + "</h1>");
            console.log(count);
        }
    } else {
        alert("HTTP-Error: " + response.status);
    }
}
