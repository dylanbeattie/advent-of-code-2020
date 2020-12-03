using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace Day3Code {
	class Program {
		static void Main(string[] args) {
			var mountain = new Mountain(File.ReadAllLines("input.txt"));
			var trees = mountain.CountTrees(3, 1);
			Console.WriteLine(trees);
		}
	}
}
