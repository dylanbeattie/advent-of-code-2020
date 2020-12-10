using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day10Code {
	class Program {
//		public static string input = @"28
//33
//18
//42
//31
//14
//46
//20
//48
//47
//24
//23
//49
//45
//19
//38
//39
//11
//1
//32
//25
//35
//8
//17
//7
//9
//4
//2
//34
//10
//3";
//		private static string input = @"1
//4
//5
//6
//7
//10";
		static void Main(string[] args) {
			var input = File.ReadAllText("input.txt");
			var ratings = input.Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)
				.Select(line => line.Trim())
				.Select(Int32.Parse)
				.OrderBy(rating => rating).ToList();
			ratings.Insert(0, 0);
			var graph = SolvePart2(ratings);
			Console.WriteLine("Counting leaves...");
			var foo = CountLeaves(graph, 0); // expecting 1
			Console.Write(foo);

			//foreach (var rating in graph.Keys) {
			//	Console.WriteLine(rating + ": ");
			//	foreach (var next in graph[rating]) {
			//		Console.WriteLine("   - " + next);
			//	}
			//}
			// ratings = ratings.Append(ratings.Max() + 3).ToList();
		}
		static Dictionary<int, long> counts = new Dictionary<int, long>();
		public static long CountLeaves(Dictionary<int, List<int>> graph, int root) {
			if (counts.ContainsKey(root)) return counts[root];
			if (graph.ContainsKey(root) && graph[root].Any()) {
				var count = graph[root].Sum(rating => CountLeaves(graph, rating));
				counts[root] = count;
				return count;
			}
			counts[root] = 1;
			return 1;
		}


		public static Dictionary<int, List<int>> SolvePart2(List<int> ratings) {
			var vertices = new Dictionary<int, List<int>>();
			for (int i = 0; i < ratings.Count; i++) {
				var rating = ratings[i];
				if (vertices.ContainsKey(rating)) continue;
				vertices[rating] = new List<int>();
				var j = i;
				while (true) {
					j++;
					if (j == ratings.Count) break;
					var nextRating = ratings[j];
					if (nextRating - rating <= 3) {
						vertices[rating].Add(nextRating);
					} else {
						break;
					}
				};
			}

			return vertices;
		}

		public static void SolvePart1(IList<int> ratings) {

			var deltas =
					ratings.Select((rating, index) => index + 1 >= ratings.Count
						? 0
						: ratings[index + 1] - rating);

			var howManyOnes = deltas.Count(r => r == 1);
			var howManyThrees = deltas.Count(r => r == 3) + 1;
			Console.WriteLine(howManyThrees * howManyOnes);
			Console.WriteLine(deltas);
		}
	}

	/*
	 * Recursively:
	 *
	 * If a single adaptor exists:
	 * Combinations = 1 (assuming 1 <= rating <= 3)
	 * TWO adaptors:
	 *   Potentials:
	 * 1 2 (5) = 2: 0>2>5, 0 > 1 > 2 > 5
	 * 1 3 (6) = 2: 0 > 3 > 6, 0 > 1 > 3 > 6
	 * 1 4 (7) = 1: 0 > 1 > 4 > 7
	 * 2 3 (6) = 2: 0 > 2 > 3 > 6, 0 > 3 > 6
	 * 2 4 (7) = 1: 0 > 2 > 4 > 7
	 * 2 5 (8) = 1: 0 > 2 > 5 > 8
	 * 3 4 (7) = 1: 0 > 3 > 4 > 7
	 * 3 5 (8) = 1: 0 > 3 > 5 > 8
	 * 3 6 (9) = 1: 0 > 3 > 6 > 9
	 * 1 2 3 (6) = 4:	0 > 1 > 2 > 3 > 6
	 *					0 > 1 > 3 > 6
	 *					0 > 2 > 3 > 6
	 * *				0 > 3 > 6
	 * 1 2 3 4 (7) = 4    0 > 1 > 2> 3> 4 > 7 
	 *					0 > 2 > 3 > 4> 7 
	 * 					0 > 2 > 4> > 7
	 *					0 > 3 > 4 > 7
	 * 1 2 3 4 5		0 > 1 > 2 > 3> 4 > 5 > 8
	 *					0 > 1 > 2 > 3 > 5 > 8
	 *					0 > 1 > 2 > 5 > 8
	 *					0 > 1 > 3 > 4 > 5 > 8
	 *					0 > 1 > 3 > 5 > 8
	 *					0 > 1 > 4 > 5 > 8
	 *					0 > 2 > 3 > 4 > 5 > 8
	 *					0 > 2 > 3 > 5 > 8
	 *					0 > 2 > 4 > 5 > 8
	 *					0 > 2 > 5 > 8
	 *					0 > 
	 *
	 * */
}
