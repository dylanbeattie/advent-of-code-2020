const fs = require('fs');

const nearley = require("nearley");
const { parentPort } = require('worker_threads');
const grammar = require("./nearley-part2.js");

// // var output = parser.feed("babbbabababbababbabbabba");
// // var output2 = parser.feed("aaaaaababaaaabbabbabbbbb");
// var output3 = parser.feed("babaabbabbbbabbbababbbbbbababaaaaabaaabbbaaaaababbaababa");
var input = fs.readFileSync("part2.txt", "utf8");
let lines = input.split(/\r?\n/);
let count = 0;
lines.forEach(line => {
    try {
        let parser = new nearley.Parser(nearley.Grammar.fromCompiled(grammar));
        parser.feed(line);        
        if (parser.results.length) count++;
    } catch(error) {
        // console.log(error);
    }
});
console.log(count);
