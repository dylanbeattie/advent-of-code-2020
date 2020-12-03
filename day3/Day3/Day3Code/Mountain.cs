using System;
using System.Linq;

namespace Day3Code {
	public class Mountain {
		public int[][] Cells { get; set;  }

		public Mountain(string[] input) {
			this.Cells = input
				.Select(line => line.Select(c => c == '#' ? 1 : 0).ToArray()).ToArray();
		}

		private int Height => this.Cells.Length;
		private int MapWidth => this.Cells[0].Length;


		public int CountTrees(int dx, int dy) {
			var treeCount = 0;
			var x = 0;
			for (var y = 0; y < this.Height; y+= dy) {
				treeCount += this.Cells[y][x];
				x = (x + dx) % this.MapWidth;
			}
			return (treeCount);
		}
	}
}