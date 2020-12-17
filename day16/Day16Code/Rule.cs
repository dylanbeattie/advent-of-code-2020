using System.Collections.Generic;
using System.Linq;

namespace Day16Code {
	public class Rule {
		public string Name { get; set; }
		public IEnumerable<Check> Checks { get; set; }

		public static Rule Parse(string rule) {
			var tokens = rule.Split(": ");
			return new Rule {
				Name = tokens[0],
				Checks = tokens[1].Split(" or ").Select(Check.Parse)
			};
		}
		public bool IsValid(int input) => Checks.Any(check => check.IsValid(input));
	}
}
