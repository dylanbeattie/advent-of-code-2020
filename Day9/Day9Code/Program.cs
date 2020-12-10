using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.ConstrainedExecution;

namespace Day9Code {
	class Program {
private static string input = @"
35
20
15
25
47
40
62
55
65
95
102
117
150
182
127
219
299
277
309
576
";

		static void Main(string[] args) {
			var input = File.ReadAllText("input.txt");
			var numbers = input.Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries)
				.Select(line => line.Trim())
				.Select(Int64.Parse).ToList();
			var invalidNumber = numbers.FindWeirdness(25);
			var sequence = numbers.FindContiguousListOfAtLeastTwoNumbersThatAddsUpTo(invalidNumber);
			var solution = sequence.Min() + sequence.Max();
			Console.WriteLine(solution);
		}
	}

	public static class LinqExtensions {
		public static Int64 FindWeirdness(this List<Int64> inputs, int preambleSize) {
			if (inputs.Count < preambleSize) return -1;
			for (var i = preambleSize + 1; i < inputs.Count; i++) {
				if (inputs.IsWeird(i, preambleSize)) return (inputs[i]);
			}
			return -1;
		}

		public static bool IsWeird(this List<Int64> ints, int index, int preambleSize) {
			var target = ints[index];
			var preamble = ints.Skip(index - preambleSize).Take(preambleSize);
			var sum = (from a in preamble
					   from b in preamble
					   where (a != b && a + b == target)
					   select 1
				).FirstOrDefault();
			return sum == default;
		}

		public static List<Int64> FindContiguousListOfAtLeastTwoNumbersThatAddsUpTo(this List<Int64> inputs,
			long target) {
			var firstIndex = 0;
			var finalIndex = 1;
			while (finalIndex < inputs.Count) {
				if (firstIndex >= finalIndex) finalIndex++;
				var window = inputs.Skip(firstIndex).Take(finalIndex - firstIndex).ToList();
				var sum = window.Sum();
				if (sum == target) return window;
				if (sum < target) finalIndex++;
				if (sum > target) firstIndex++;
			}
			return new List<Int64>();
		}
	}
}
