/* So here's the scenario:

WE have an array. BIG SPARSE array, with ints in it:

[ , , , 1, , , , , , , , , , , , , , 2, 3, 2, , , , , , , , 1 , , , , , 2 , , , ,2 , 3 , , , ]

We want to apply a bitshift operator to every element of this array, so that a[x] => a[x] >>> 1

*/

let grid1 = new Array();
grid1[1] = 1;
grid1[100] = 2;
grid1[1000] = 3;
grid1[10000] = 2;

let grid2 = new Array();
grid2[1] = 1;
grid2[100] = 2;
grid2[1000] = 3;
grid2[10000] = 2;

console.log(grid1);
console.log(grid2);

for(var i = 0; i < grid1.length; i++) grid1[i] = grid1[i] >>> 1;
grid2.forEach((value, index) => grid2[index] = value >>> 1);


console.log(grid1.filter(c => c).length);
console.log(grid2.filter(c => c).length);
