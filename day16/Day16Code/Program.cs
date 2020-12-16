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
		private static List<List<int>> nearbyTickets;
		private static long[] myTicket;

		static void Main(string[] args) {
			var input = File.ReadAllText("input.txt");
			var solution = SolveAdventOfCodePart2(input);
			Console.WriteLine(solution);
		}

		public static long SolveAdventOfCodePart2(string input) {
			Parse(input);
			var validTickets = nearbyTickets.Where(engine.IsValid).ToList();
			var matrix = validTickets
				.Select(ticket => engine.ListRulesForTicket(ticket)
					.Select(list => list.Select(rule => rule.Name).ToList()).ToList()).ToList();
			Dictionary<int, List<string>> fieldRules = new Dictionary<int, List<string>>();
			foreach (var list in matrix) {
				for (var fieldIndex = 0; fieldIndex < list.Count; fieldIndex++) {
					if (fieldRules.ContainsKey(fieldIndex)) {
						fieldRules[fieldIndex] = fieldRules[fieldIndex].Intersect(list[fieldIndex]).ToList();
					} else {
						fieldRules.Add(fieldIndex, list[fieldIndex]);
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
			var rulesInput = chunks[0];
			myTicket = chunks[1].Split("\n", StringSplitOptions.RemoveEmptyEntries)
				.Skip(1).First().Split(",").Select(Int64.Parse).ToArray();
			var nearbyTicketsInput = chunks[2];
			nearbyTickets = nearbyTicketsInput.Split("\n", StringSplitOptions.RemoveEmptyEntries)
				.Skip(1).Select(t => t.Split(",").Select(Int32.Parse).ToList()).ToList();
			engine = new Engine(rulesInput);
		}


		public static int SolveAdventOfCodePart1(string input) {
			Parse(input);
			return nearbyTickets.Sum(ticket => engine.FindInvalidValues(ticket).Sum());
		}

		private const string inputPart1 = @"class: 1-3 or 5-7
row: 6-11 or 33-44
seat: 13-40 or 45-50

your ticket:
7,1,14

nearby tickets:
7,3,47
40,4,50
55,2,20
38,6,12";

		private const string input = @"class: 0-1 or 4-19
row: 0-5 or 8-19
seat: 0-13 or 16-19

your ticket:
11,12,13

nearby tickets:
3,9,18
15,1,5
5,14,9";

	}


}

