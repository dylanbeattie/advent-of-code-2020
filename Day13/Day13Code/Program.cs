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
		// private static readonly int[] input = new[] { 7, 13, 0, 0, 59, 0, 31, 19 };
		// Should yield 1068781

		private static readonly int[] input = new[] {
			19, 0, 0, 0, 0, 0, 0, 0, 0, 41, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			743, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 13, 17, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 29, 0, 643, 0, 0, 0, 0, 0, 37,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 23
		};

		static void Main(string[] args) {
			var sw = new Stopwatch();
			sw.Start();
			var limit = 2;
			long initial = 0;
			long interval = 1;
			while (limit < input.Length) {
				(initial, interval) = FindInterval(initial, interval, input, limit++);
			}
			var ts = initial;
			while (true) {
				if (SolvesPart2(ts)) {
					Console.WriteLine(ts);
					sw.Stop();
					Console.WriteLine($"Solved in {sw.ElapsedMilliseconds}ms");
					break;
				}
				ts += interval;
			}
		}

		static (long, long) FindInterval(long initial, long interval, int[] input, int limit) {
			var stillLooking = true;
			for (long i = initial; i < Int64.MaxValue; i+= interval) {
				if (!input.Take(limit).Select((busId, index) => {
					if (busId == 0) return (true);
					return (i + index) % busId == 0;
				}).All(b => b)) continue;
				if (stillLooking) {
					initial = i;
					stillLooking = false;
				} else {
					interval = i - initial;
					break;
				}
			}
			return (initial, interval);
		}


		static bool SolvesPart2(long ts) {
			var checks = input.Select((busId, index) => {
				if (busId == 0) return (true);
				return (ts + index) % busId == 0;
			});
			return checks.All(b => b);
		}
	}
}
