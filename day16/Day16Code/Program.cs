using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;

namespace Day16Code {
	public class Program {

		static void Main(string[] args) {
			var input = File.ReadAllText("input.txt");
			var solution = SolveAdventOfCodePart1(input);
			Console.WriteLine(solution);
		}

		public static int SolveAdventOfCodePart1(string input) {
			var chunks = input.Replace("\r", "").Split("\n\n");
			var rulesInput = chunks[0];
			var myTicketInput = chunks[1];
			var nearbyTicketsInput = chunks[2];
			var nearbyTickets = nearbyTicketsInput.Split("\n", StringSplitOptions.RemoveEmptyEntries)
				.Skip(1).Select(t => t.Split(",").Select(Int32.Parse));
			var engine = new Engine(rulesInput);
			var sum = 0;
			foreach (var ticket in nearbyTickets) {
				sum += engine.FindInvalidValues(ticket).Sum();
			}

			return sum;
		}

		private const string input = @"class: 1-3 or 5-7
row: 6-11 or 33-44
seat: 13-40 or 45-50

your ticket:
7,1,14

nearby tickets:
7,3,47
40,4,50
55,2,20
38,6,12";


	}
}
	
	