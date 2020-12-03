using System;
using System.Linq;

namespace Day3Code {
	public class Vector {
		public Vector(int x, int y) {
			this.X = x;
			this.Y = y;
		}

		public int X { get; set; }
		public int Y { get; set; }
	}

	public class Mountain {
		public int[][] Cells { get; set;  }

		public Mountain(string[] input) {
			this.Cells = input
				.Select(line => line.Select(c => c == '#' ? 1 : 0).ToArray()).ToArray();
		}

		private int Height => this.Cells.Length;
		private int MapWidth => this.Cells[0].Length;


		public int CountTrees(Vector v) {
			var treeCount = 0;
			var x = 0;
			for (var y = 0; y < this.Height; y+= v.Y) {
				treeCount += this.Cells[y][x];
				x = (x + v.X) % this.MapWidth;
			}
			return (treeCount);
		}
	}
}