const { MODULESPECIFIER_TYPES } = require("@babel/types");

class Game {

    constructor(input, limit) {
        let cupola = new Array(limit || 0).fill(0).map((_, index) => index+1);
        let cups = input.split('').map(n => parseInt(n));
        cupola.splice(0,cups.length,...cups);
        this.cups = cupola;
        this.maximumCup = this.cups.length;
    }

    get currentCup() {
        return this.cups[0];
    }
    playMoves(count) {
        for(var i = 0; i < count; i++) this.playMove();
    }

    playMove() {
        let indexOfCurrentCup = this.cups.indexOf(this.currentCup);
        let threeCups = this.cups.splice(indexOfCurrentCup+1, 3);
        let destinationCup = this.currentCup - 1;
        while(this.cups.indexOf(destinationCup) < 0) {
            destinationCup--;
            if (destinationCup < 0) destinationCup = this.maximumCup;
        }
        let destinationCupIndex = this.cups.indexOf(destinationCup);
        this.cups.splice(destinationCupIndex + 1, 0, ...threeCups);
        this.cups.push(this.cups.shift());
    }
    toString() {
        return this.cups.join(", ");
    }
    get solution() {
        var cut = this.cups.indexOf(1);
        return this.cups.slice(cut+1).concat(this.cups.slice(0,cut)).join('');
    }
}
module.exports = Game;