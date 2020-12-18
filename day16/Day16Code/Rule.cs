using System;
using System.Collections.Generic;
using System.Linq;

namespace Day16Code {
	public class Rule {
		public string Name { get; set; }
		public IEnumerable<Func<int, bool>> checks;

		public static Rule Parse(string rule) {
			var tokens = rule.Split(": ");
			return new Rule {
				Name = tokens[0],
				checks = tokens[1].Split(" or ").Select<String, Func<int, bool>>(pair => {
					var ints = pair.Split("-").Select(Int32.Parse);
					return i => ints.First() <= i && ints.Last() >= i;
				})
			};
		}

		public bool IsValid(int input) => checks.Any(check => check(input));
	}
}

