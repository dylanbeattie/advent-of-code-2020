using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;

namespace Day13Code {
	/*
	 * 19,x,x,x,x,x,x,x,x,41,x,x,x,x,x,x,x,x,x,743,x,x,x,x,x,x,x,x,x,x,x,x,13,17,x,x,x,x,x,x,x,x,x,x,x,x,x,x,29,x,643,x,x,x,x,x,37,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,23
	 */

	class Program {
		//private static readonly int[] input = new[] {
		//	19, 0, 0, 0, 0, 0, 0, 0, 0, 41, 0, 0, 0, 0, 0, 0, 0, 0, 0,
		//	743, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 13, 17, 0, 0, 0, 0,
		//	0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 29, 0, 643, 0, 0, 0, 0, 0, 37,
		//	0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 23
		//};

		private static readonly int[] input = new[] { 7, 13, 0, 0, 59, 0, 31 };

		static void Main(string[] args) {
			var sw = new Stopwatch();
			sw.Start();
			(var root, var delta) = FindInterval(input, 5);
			Console.WriteLine(root);
			Console.WriteLine(delta);
			Console.ReadKey(false);
		}

		static (long, long) FindInterval(int[] input, int limit) {
			long initial = 0;
			long interval = 1;
			for (long i = 0; i < Int64.MaxValue; i++) {
				if (!input.Take(limit).Select((busId, index) => {
					if (busId == 0) return (true);
					return (i + index) % busId == 0;
				}).All(b => b)) continue;
				if (initial == 0) {
					initial = i;
				} else {
					interval = i - initial;
					break;
				}
			}
			return (initial, interval);
		}


		static bool SolvesPart2(long ts) {

			//var things = input.Select((busId, index) => (busId, index))
			//	.ToDictionary(tuple => tuple.busId, tuple => tuple.index);

			//Enumerable.Range(0, Int32.MaxValue)
			//	.Select(i => )
			var checks = input.Select((busId, index) => {
				if (busId == 0) return (true);
				return (ts + index) % busId == 0;
			});
			return checks.All(b => b);
		}
	}
}
