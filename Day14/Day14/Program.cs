using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day14 {

	class Program {
//		private const string input = @"mask = 000000000000000000000000000000X1001X
//mem[42] = 100
//mask = 00000000000000000000000000000000X0XX
//mem[26] = 1";

		static void Main(string[] args) {
			var mask = String.Empty.PadRight(36, 'X');
			var input = File.ReadAllText("input.txt");
			var lines = input
				.Replace("\r", "")
				.Split("\n", StringSplitOptions.RemoveEmptyEntries)
				.Select(line => line.Trim());
			var memory = new Dictionary<long, long>();
			foreach (var line in lines) {
				var tokens = line.Split(" = ");
				if (tokens[0] == "mask") {
					mask = tokens[1];
				} else {
					var value = Int64.Parse(tokens[1]);
					var memoryAddress = Int32.Parse(Regex.Replace(tokens[0], @"\D", ""));
					//var valueToWrite = Masker.ApplyMask(mask, value);
					foreach (var fuzzedAddress in Masker.FuzzMask(mask, memoryAddress)) {
						memory[fuzzedAddress] = value;
					}
				}
			}

			var sum = memory.Values.Sum();
			Console.WriteLine(sum);
		}
	}
}
