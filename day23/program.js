const Game = require('./game');

function SolvePart1() {
    let game = new Game('872495136');
    game.playMoves(100);
    console.log(game.solution);
}

function SolvePart2() {
    let game = new Game('872495136', 1000);
    game.playMoves(1000);
}

SolvePart2();



