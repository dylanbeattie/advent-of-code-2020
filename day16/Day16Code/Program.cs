using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;

namespace Day16Code {
	public class Program {
		private static Engine engine;
		private static List<int[]> nearbyTickets;
		private static long[] myTicket;

		static void Main(string[] args) {
			var input = File.ReadAllText("input.txt");
			var solution = SolveAdventOfCodePart2(input);
			Console.WriteLine(solution);
		}

		public static long SolveAdventOfCodePart2(string input) {
			Parse(input);
			var ticketRules = nearbyTickets.Where(engine.IsValid).Select(ticket => engine.ListRulesForTicket(ticket)).ToList();

			var fieldRules = new Dictionary<int, List<string>>();

			ticketRules.Select((rules, index) => { 

			})
			foreach (var list in ticketRules) {
				for (var fieldIndex = 0; fieldIndex < list.Length; fieldIndex++) {
					if (fieldRules.ContainsKey(fieldIndex)) {
						fieldRules[fieldIndex] = fieldRules[fieldIndex].Intersect(list[fieldIndex]).ToList();
					} else {
						fieldRules.Add(fieldIndex, list[fieldIndex].ToList());
					}
				}
			}
			var solution = fieldRules.Where(kvp => kvp.Value.Count == 1).ToDictionary(kvp => kvp.Key, kvp => kvp.Value.First());
			while (fieldRules.Any()) {
				foreach (var foundField in solution.Values) {
					foreach (var field in fieldRules.Keys) fieldRules[field].Remove(foundField);
				}
				var keysToRemove = fieldRules.Where(kvp => kvp.Value.Count == 0).Select(kvp => kvp.Key);
				foreach (var k in keysToRemove) fieldRules.Remove(k);
				foreach (var (key, value) in fieldRules.Where(kvp => kvp.Value.Count == 1)) solution.Add(key, value.First());
			}
			foreach (var key in solution.Keys) Console.WriteLine(key + ": " + solution[key]);
			var solutionIndices = solution.Where(kvp => kvp.Value.StartsWith("departure")).Select(kvp => kvp.Key)
				.ToArray();

			var answer = solutionIndices.Select(i => myTicket[i]).Aggregate(1L, (x, y) => x * y);
			return (answer);
		}

		private static void Parse(string input) {
			var chunks = input.Replace("\r", "").Split("\n\n");
			engine = new Engine(chunks[0]);
			myTicket = chunks[1].Split("\n", StringSplitOptions.RemoveEmptyEntries).Skip(1).First().Split(",").Select(Int64.Parse).ToArray();
			nearbyTickets = chunks[2].Split("\n", StringSplitOptions.RemoveEmptyEntries).Skip(1).Select(t => t.Split(",").Select(Int32.Parse).ToArray()).ToList();
		}


		public static int SolveAdventOfCodePart1(string input) {
			Parse(input);
			return nearbyTickets.Sum(ticket => engine.FindInvalidValues(ticket).Sum());
		}
	}
}

