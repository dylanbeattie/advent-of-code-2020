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
            y: { max: 0, min: 0 },
            z: { max: 0, min: 0 },
            å: { max: 0, min: 0 }
        };
    }

    chonk(x, y, z, å) {
        return (x + 500) + (1000 * y + 500) + (1000000 * z + 500) + (1000000000 * å + 500);
    }

    set(x, y, z, å, value) {
        this.extents.x.max = Math.max(this.extents.x.max, x);
        this.extents.y.max = Math.max(this.extents.y.max, y);
        this.extents.z.max = Math.max(this.extents.z.max, z);
        this.extents.å.max = Math.max(this.extents.å.max, å);

        this.extents.x.min = Math.min(this.extents.x.min, x);
        this.extents.y.min = Math.min(this.extents.y.min, y);
        this.extents.z.min = Math.min(this.extents.z.min, z);
        this.extents.å.min = Math.min(this.extents.å.min, å);

        let index = this.chonk(x, y, z, å);
        this.cells[index] = value;
    }

    get(x, y, z, å) {
        let index = this.chonk(x, y, z, å);
        return (this.cells[index]);
    }
    evolve() {
        var xMin = this.extents.x.min - 2;
        var yMin = this.extents.y.min - 2;
        var zMin = this.extents.z.min - 2;
        var åMin = this.extents.å.min - 2;

        var xMax = this.extents.x.max + 2;
        var yMax = this.extents.y.max + 2;
        var zMax = this.extents.z.max + 2;
        var åMax = this.extents.å.max + 2;

        for (let x = xMin; x <= xMax; x++) 
        for (let y = yMin; y <= yMax; y++) 
        for (let z = zMin; z <= zMax; z++) 
        for (let å = åMin; å <= åMax; å++) {
            let numberOfLivingNeighbours = 0;
            for (let xx = x - 1; xx <= x + 1; xx++) 
            for (let yy = y - 1; yy <= y + 1; yy++) 
            for (let zz = z - 1; zz <= z + 1; zz++) 
            for (let åå = å - 1; åå <= å + 1; åå++) {
                if (xx == x && yy == y && zz == z && åå == å) continue;
                if (this.get(xx, yy, zz, åå) & 1 == 1) numberOfLivingNeighbours++;
            }
            let cell = this.get(x, y, z, å);
            if (cell & 1) { // cell is ACTIVE
                if (numberOfLivingNeighbours == 2 || numberOfLivingNeighbours == 3) {
                    this.set(x, y, z, å, cell | 2);
                }
            } else { // cell is INACTIVE
                if (numberOfLivingNeighbours == 3) this.set(x, y, z, å, cell | 2);
            }
        }
        for (let x = xMin; x <= xMax; x++) for (let y = yMin; y <= yMax; y++) for (let z = zMin; z <= zMax; z++) for (let å = åMin; å <= åMax; å++) {
            let cell = this.get(x, y, z, å);
            this.set(x, y, z, å, cell >>> 1);
        }
        // QUESTION: why does this not have the same effect as the loop above?
        // for(let index = 0; index < this.cells.length; index++) {
        //     this.cells[index] = this.cells[index] >>> 1;
        // }

    }

    get activeCellCount() {
        var xMin = this.extents.x.min - 2;
        var yMin = this.extents.y.min - 2;
        var zMin = this.extents.z.min - 2;
        var åMin = this.extents.å.min - 2;

        var xMax = this.extents.x.max + 2;
        var yMax = this.extents.y.max + 2;
        var zMax = this.extents.z.max + 2;
        var åMax = this.extents.å.max + 2;
        let count = 0;
        for (let x = xMin; x <= xMax; x++) for (let y = yMin; y <= yMax; y++) for (let z = zMin; z <= zMax; z++) for (let å = åMin; å <= åMax; å++) {
            if (this.get(x, y, z, å) & 1) count++;
        }
        return (count);
    }
}

let grid = new Grid();
let z = 0;
let å = 0;
input.split('\n').map((line, y) => line.split('').map((item, x) => grid.set(x, y, z, å, item == '#' ? 1 : 0)));
for (var i = 0; i < 6; i++) {
    console.log(`Generation ${i}; active cells: ${grid.activeCellCount}`)
    grid.evolve();
}
console.log(grid.activeCellCount);
console.log("Done");

