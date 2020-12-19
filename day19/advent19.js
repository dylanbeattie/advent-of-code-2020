async function solve() {
    let response = await fetch("aocday19.peg");

    if (response.ok) { // if HTTP-status is 200-299
        // get the response body (the method explained below)
        let grammar1 = await response.text();
        var parser1 = peg.generate(grammar1);

        let response2 = await fetch("input.txt");
        if (response2.ok) {
            let input = await response2.text();
            console.log(input);
            let chunks = input.split(/\r?\n\r?\n/);
            let input1 = chunks[0].split(/\n/g).sort().join('\n');
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
            console.log(count);
        }
    } else {
        alert("HTTP-Error: " + response.status);
    }
}
