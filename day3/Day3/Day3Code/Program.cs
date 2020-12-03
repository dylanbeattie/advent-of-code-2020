using System;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Day3Code {
	class Program {
		static void Main(string[] args) {
			var mountain = new Mountain("input.txt");
			var inputs = new[] {
				new Vector(1, 1),
				new Vector(3, 1),
				new Vector(5, 1),
				new Vector(7, 1),
				new Vector(1, 2)
			};
			var treeCounts = inputs.Select(v => mountain.CountTrees(v));
			var total = treeCounts.Aggregate((x, y) => x * y);
			Console.WriteLine(total);
		}
	}
}
