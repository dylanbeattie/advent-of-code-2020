console.log("Loaded!");

let input = `.#.#..##
..#....#
##.####.
...####.
#.##..##
#...##..
...##.##
#...#.#.`;

// let input = `.#.
// ..#
// ###`;

class Grid {
    // LOGIC FOR CELL VALUES
    // CELL & 1 = is it alive
    // CELL & 2 = does it survive
    // To evolve, do a zero padded bitshift cell >>> 1 - this will move the 'next' iteration into the 1 space
    // and dump the current state.
    constructor() {
        this.cells = new Array();
        this.extents = {
            x: { max: 0, min: 0 },
            y: { max: 0, min: 0 }
        };
    }


    set(x, y, value) {
        this.extents.x.max = Math.max(this.extents.x.max, x);
        this.extents.y.max = Math.max(this.extents.y.max, y);

        this.extents.x.min = Math.min(this.extents.x.min, x);
        this.extents.y.min = Math.min(this.extents.y.min, y);

        let index = (x + 500) + (1000 * y + 500);
        this.cells[index] = value;
    }

    get(x, y) {
        let index = (x + 500) + (1000 * y + 500);
        return (this.cells[index]);
    }
    evolve() {
        var xMin = this.extents.x.min - 2;
        var yMin = this.extents.y.min - 2;

        var xMax = this.extents.x.max + 2;
        var yMax = this.extents.y.max + 2;

        for (let x = xMin; x <= xMax; x++) for (let y = yMin; y <= yMax; y++) {
            let numberOfLivingNeighbours = 0;
            for (let xx = x - 1; xx <= x + 1; xx++) for (let yy = y - 1; yy <= y + 1; yy++) {
                if (xx == x && yy == y) continue;
                if (this.get(xx, yy) & 1 == 1) numberOfLivingNeighbours++;
            }
            let cell = this.get(x, y);
            if (cell & 1) { // cell is ACTIVE
                if (numberOfLivingNeighbours == 2 || numberOfLivingNeighbours == 3) {
                    this.set(x, y, cell | 2);
                }
            } else { // cell is INACTIVE
                if (numberOfLivingNeighbours == 3) this.set(x, y, cell | 2);
            }
        }
        for (let x = xMin; x <= xMax; x++) for (let y = yMin; y <= yMax; y++) {
            let cell = this.get(x,y);
            this.set(x,y,cell >>> 1);
        }
    //    QUESTION: why does this not have the same effect as the loop above?
    //     for(let index = 0; index < this.cells.length; index++) {
    //         this.cells[index] = this.cells[index] >>> 1;
    //     }
        
    }

    get activeCellCount() {
        var xMin = this.extents.x.min - 2;
        var yMin = this.extents.y.min - 2;

        var xMax = this.extents.x.max + 2;
        var yMax = this.extents.y.max + 2;
        let count = 0;
        for (let x = xMin; x <= xMax; x++) for (let y = yMin; y <= yMax; y++) {
            if (this.get(x,y) & 1) count++;
        }
        return(count);
    }
}

let grid = new Grid();
input.split('\n').map((line, y) => line.split('').map((item, x) => grid.set(x, y, item == '#' ? 1 : 0)));
for (var i = 0; i < 4; i++) {
    console.log(`Generation ${i}; active cells: ${grid.activeCellCount}`)
    grid.evolve();
}
console.log(grid.activeCellCount);
console.log("Done");

