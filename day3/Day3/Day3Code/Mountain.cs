using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;

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
		private readonly IEnumerable<string> lines;

		public Mountain(string filename) {
			this.lines = File.ReadLines(filename);
		}

		public int CountTrees(Vector v) {
			var x = 0;
			var y = 0;
			var treeCount = 0;
			foreach (var line in lines) {
				if (y++ % v.Y != 0) continue;
				var isTree = line[x] == '#';
				if (isTree) treeCount++;
				x = (x + v.X) % line.Length;
			}
			return (treeCount);
		}
	}
}