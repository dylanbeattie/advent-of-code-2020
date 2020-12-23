const { ListNode } = require("./ListNode");

class Game {
    constructor(input, limit) {
        let cupola = new Array(limit || 0).fill(0).map((_, index) => index+1);
        let cups = input.split('').map(n => parseInt(n));
        cupola.splice(0,cups.length,...cups);
        this.maximumCup = cupola.length;
        this.currentCup = ListNode.BuildLoop(cupola);
        this.map = new Map();
        let node = this.currentCup.next;
        while(node != this.currentCup) {
            this.map.set(node.value, node);
            node = node.next;
        }
    }

    playMoves(count) {
        for(var i = 0; i < count; i++) this.playMove();
    }

    playMove() {
        let threeCups = this.currentCup.splice(3);
        let destCupLabel = this.currentCup.value;
        let destCup = null;
        while(destCup == null) {
            if (destCupLabel-- <=0) destCupLabel = this.maximumCup;
            destCup = this.currentCup.find(destCupLabel);
        }
        destCup.insert(threeCups);
        this.currentCup = this.currentCup.next;
    }
    toString() {
        return this.cups.join(", ");
    }
    get part2solution() {
        let cup = this.currentCup.find(1).next;
        return (cup.value * cup.next.value);
    }

    get part1solution() {
        let result = new Array();
        let cup = this.currentCup.find(1).next;
        while(cup.value != 1) {
            result.push(cup.value);
            cup = cup.next;
        }
        return result.join('');
    }
}
module.exports = { Game, ListNode };