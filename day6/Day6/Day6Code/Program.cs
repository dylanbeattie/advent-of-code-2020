using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;

namespace Day6Code {
	public class Program {


		public static string HotPinkNewlines 
			= "\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n";


		static void Main(string[] args) {
			Console.WriteLine(File.ReadAllText("input.txt").Replace("\r", "")
				.Split("\n\n").Select(ParseCustomsDeclarationFormUsingPart2Rules).Sum());
		}

		public static int ParseCustomsDeclarationForm(string input) {
			return Regex.Replace(input, "[^a-z]", "").Distinct().Count();
		}

		public static int ParseCustomsDeclarationFormUsingPart2Rules(string input) {
			return String.IsNullOrEmpty(input) ? 0 : input
				.Split("\n", StringSplitOptions.RemoveEmptyEntries)
				.Aggregate((a, b) => String.Concat(a.Intersect(b))).Length;
		}
	}
}
