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
            z: { max: 0, min: 0 }
        };
    }


    set(x, y, z, value) {
        this.extents.x.max = Math.max(this.extents.x.max, x);
        this.extents.y.max = Math.max(this.extents.y.max, y);
        this.extents.z.max = Math.max(this.extents.z.max, z);

        this.extents.x.min = Math.min(this.extents.x.min, x);
        this.extents.y.min = Math.min(this.extents.y.min, y);
        this.extents.z.min = Math.min(this.extents.z.min, z);

        let index = (x + 500) + (1000 * y + 500) + (1000000 * z + 500);
        this.cells[index] = value;
    }

    get(x, y, z) {
        let index = (x + 500) + (1000 * y + 500) + (1000000 * z + 500);
        return (this.cells[index]);
    }
    evolve() {
        var xMin = this.extents.x.min - 2;
        var yMin = this.extents.y.min - 2;
        var zMin = this.extents.z.min - 2;

        var xMax = this.extents.x.max + 2;
        var yMax = this.extents.y.max + 2;
        var zMax = this.extents.z.max + 2;

        //        console.log(`looping: ${xMin} <= x <= ${xMax}, ${yMin} <= y <= ${yMax}, ${zMin} <= z <= ${zMax}`);

        // GET READY...
        for (let x = xMin; x <= xMax; x++) for (let y = yMin; y <= yMax; y++) for (let z = zMin; z <= zMax; z++) {
            let numberOfLivingNeighbours = 0;
            for (let xx = x - 1; xx <= x + 1; xx++) for (let yy = y - 1; yy <= y + 1; yy++) for (let zz = z - 1; zz <= z + 1; zz++) {
                if (xx == x && yy == y && zz == z) continue;
                if (this.get(xx, yy, zz) & 1 == 1) numberOfLivingNeighbours++;
            }
            let cell = this.get(x, y, z);
            if (cell & 1) { // cell is ACTIVE
                if (numberOfLivingNeighbours == 2 || numberOfLivingNeighbours == 3) {
                    // console.log("We would set here if it didn't break everything!");
                    // console.log(`Settings grid(${x},${y},${z}) to ${cell | 2}`);
                    this.set(x, y, z, cell | 2);
                }
            } else { // cell is INACTIVE
                if (numberOfLivingNeighbours == 3) this.set(x, y, z, cell | 2);
            }
        }
        for (let x = xMin; x <= xMax; x++) for (let y = yMin; y <= yMax; y++) for (let z = zMin; z <= zMax; z++) {
            let cell = this.get(x,y,z);
            this.set(x,y,z,cell >>> 1);
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

        var xMax = this.extents.x.max + 2;
        var yMax = this.extents.y.max + 2;
        var zMax = this.extents.z.max + 2;
        let count = 0;
        for (let x = xMin; x <= xMax; x++) for (let y = yMin; y <= yMax; y++) for (let z = zMin; z <= zMax; z++) {
            if (this.get(x,y,z) & 1) count++;
        }
        return(count);
    }
}

let grid = new Grid();
let z = 0;
input.split('\n').map((line, y) => line.split('').map((item, x) => grid.set(x, y, z, item == '#' ? 1 : 0)));
for (var i = 0; i < 6; i++) {
    console.log(`Generation ${i}; active cells: ${grid.activeCellCount}`)
    grid.evolve();
}
console.log(grid.activeCellCount);
console.log("Done");

