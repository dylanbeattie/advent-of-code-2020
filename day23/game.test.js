const { expect, test } = require("@jest/globals");
const { Game, ListNode } = require('./game');

test('it works', () => {
    expect(1).toBe(1);
})
describe('game loads various cups correctly', () => {
    const cases = [
        ['1', [1], 1],
        ['123', [1, 2, 3], 1],
        ['231', [2, 3, 1], 2],
        ['389125467', [3, 8, 9, 1, 2, 5, 4, 6, 7], 3]
    ];
    test.each(cases)("when input is %p", (input, expectedCups, currentCup) => {
        let game = new Game(input);
        expect(game.cups).toEqual(expectedCups);
        expect(game.currentCup).toEqual(currentCup);
    });
});

describe('game loads various cups correctly in NIGHTMARE DIFFICULTY mode', () => {
    const cases = [
        ['1', 10, [1, 2, 3, 4, 5, 6, 7, 8, 9, 10]],
        ['123', 10, [1, 2, 3, 4, 5, 6, 7, 8, 9, 10]],
        ['231', 10, [2, 3, 1, 4, 5, 6, 7, 8, 9, 10]],
        ['389125467', 20, [3, 8, 9, 1, 2, 5, 4, 6, 7, 10, 11, 12,13,14,15,16,17,18,19,20]]
    ];
    test.each(cases)("when input is %p", (input, limit, expectedCups) => {
        let game = new Game(input, limit);
        expect(game.cups).toEqual(expectedCups);
    });
});

test('game plays one move', () => {
    let game = new Game('389125467');
    game.playMove();
    expect(game.cups).toEqual([2, 8, 9, 1, 5, 4, 6, 7, 3]);
    expect(game.currentCup).toBe(2);
    game.playMove();
    expect(game.cups).toEqual([5, 4, 6, 7, 8, 9, 1, 3, 2]);
    expect(game.currentCup).toBe(5);

    game.playMove();
    expect(game.cups).toEqual([8, 9, 1, 3, 4, 6, 7, 2, 5]);
    expect(game.currentCup).toBe(8);
});

test('game plays N moves', () => {
    let game = new Game('389125467');
    game.playMoves(10);
    expect(game.cups).toEqual([8, 3, 7, 4, 1, 9, 2, 6, 5]);
    expect(game.currentCup).toEqual(8);
});


test('game plays N moves', () => {
    let game = new Game('389125467');
    expect(game.part1solution).toBe('25467389');
});