using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace Day4Code {
	class Program {
		private static string[] requiredFields = {
			"byr",
			"iyr",
			"eyr",
			"hgt",
			"hcl",
			"ecl",
			"pid"
			// "cid",
		};
		static void Main(string[] args) {
			var input = File.ReadAllText("input.txt");
			var passports = Parser.Parse(input);
			var numberOfValidPassports = passports.Count(p => p.ContainsAllFields(requiredFields));
			Console.WriteLine(numberOfValidPassports);
			//foreach (var p in passports) {
			//	Console.WriteLine(String.Join(" / ", p.Fields.Keys.ToArray()));
			//	Console.WriteLine(String.Empty.PadRight(72, '='));
			//}
		}
	}
}

class Passport {
	public Dictionary<string,string> Fields { get; set; } = new Dictionary<string, string>();
	public Passport(string input) {
		this.Fields = input.Split(' ', '\n')
			.Where(s => s.Contains(":"))
			.Select(field => field.Split(':'))
			.ToDictionary(pair => pair[0], pair => pair[1]);
	}

	public bool ContainsAllFields(string[] fields) {
		return (fields.Intersect(this.Fields.Keys).ToList().Count == fields.Length);
	}
}


class Parser {
	public static List<Passport> Parse(string input) {
		var inputs = input.Replace("\r", "").Split("\n\n");
		return inputs.Select(p => new Passport(p)).ToList();
	}
}
