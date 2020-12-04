using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

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
		//"cid",
		};
		static void Main(string[] args) {
			var input = File.ReadAllText("input.txt");
			var passports = Parser.Parse(input);
			Console.WriteLine(passports.Count(p => p.Validate(requiredFields)));
		}
	}
}

class Passport {
	public Dictionary<string, string> Fields { get; set; } = new Dictionary<string, string>();

	public Passport(string input) {
		this.Fields = input.Split(' ', '\n')
			.Where(s => s.Contains(":"))
			.Select(field => field.Split(':'))
			.ToDictionary(pair => pair[0], pair => pair[1]);
	}

	public bool ContainsAllFields(string[] fields) {
		return (fields.Intersect(this.Fields.Keys).ToList().Count == fields.Length);
	}

	public bool Validate(string[] fieldsToValidate) {
		return fieldsToValidate.All(key => Fields.ContainsKey(key) && validators[key](Fields[key]));
	}

	private Dictionary<string, Func<string, bool>> validators = new Dictionary<string, Func<string, bool>> {
		{ "byr", s => s.IsNumberBetween(1920, 2002) },
		{ "iyr", s => s.IsNumberBetween(2010, 2020) },
		{ "eyr", s => s.IsNumberBetween(2020, 2030) },
		{ "hgt", ValidateHeight },
		{ "hcl", ValidateHairColour },
		{ "ecl", ValidateEyeColour },
		{ "pid", ValidatePassportNumber }
	};

	private static bool ValidateHairColour(string s) {
		return (Regex.IsMatch(s, "^#[0-9a-z]{6}$"));
	}

	private static bool ValidateEyeColour(string s) {
		return "amb blu brn gry grn hzl oth".Split(' ').Contains(s);
	}

	private static bool ValidatePassportNumber(string s) {
		return (Regex.IsMatch(s, "^[0-9]{9}$"));
	}

	private static bool ValidateHeight(string s) {
		if (s.EndsWith("cm")) return s.Substring(0, s.Length - 2).IsNumberBetween(150, 193);
		if (s.EndsWith("in")) return s.Substring(0, s.Length - 2).IsNumberBetween(59, 76);
		return (false);
	}
}


class Parser {
	public static List<Passport> Parse(string input) {
		var inputs = input.Replace("\r", "").Split("\n\n");
		return inputs.Select(p => new Passport(p)).ToList();
	}
}

public static class StringExtensions {
	public static bool IsNumberBetween(this string s, int x, int y) => Int32.TryParse(s, out int i) && i >= x && i <= y;
}
