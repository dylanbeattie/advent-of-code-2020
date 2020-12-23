const {Game, ListNode} = require('./game');

function SolvePart1() {
    let game = new Game('872495136');
    game.playMoves(100);
    console.log(game.part1solution);
}

function SolvePart2() {
    // [100, 1_000,10_000,100_000,1_000_000]
    [1_000_000].forEach(size => {
        var commenced = new Date().valueOf();
        let game = new Game('872495136', size);
        game.playMoves(10_000_000);
        console.log(game.part2solution);
        var concluded = new Date().valueOf();
        console.log(size, (concluded - commenced));
    });
}

SolvePart2();

// var head = ListNode.BuildLoop([1,2,3,4,5]);
// console.log(head.dump());
// head = head.next.next;
// var chunk = head.splice(3);
// console.log(head.dump());
// console.log(chunk.dump());
// // console.log(node.value);
// // node.insert(foo);
// // console.log(list.toArray());

// SolvePart1();
