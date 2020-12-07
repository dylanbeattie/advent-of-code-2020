using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day7Code {
	public class Policy {

		public struct BagRule {
			public string Name;
			public int Count;

			public static BagRule FromToken(string token) {
				var pair = token.Split(new char[] {' '}, 2);
				return new BagRule() {
					Name = pair[1],
					Count = Int32.Parse(pair[0])
				};
			}
		}

		public static (string Name, Dictionary<string, int> Rules) ParseRule(string rule) {
			rule = Regex.Replace(rule, @" bags?\.?", "");
			var tokens = rule.Split(" contain ");
			var ruleName = tokens[0]; // eg "dotted black";
			var rules = new Dictionary<string, int>();
			if (tokens[1] == "no other") return (ruleName, rules);
			rules = tokens[1].Split(", ")
				// Parse "6 faded blue" into { "faded blue", 6 }
				.Select(colorRule => BagRule.FromToken(colorRule))
				.ToDictionary(bagRule => bagRule.Name, bagRule => bagRule.Count);
			return (ruleName, rules);
		}

		public Dictionary<string, Dictionary<string, int>> BagRules;

		public static Policy ParseRules(string rules) {
			var policy = new Policy() {
				BagRules = rules
					.Split(Environment.NewLine)
					.Select(rule => ParseRule(rule))
					.ToDictionary(t => t.Name, t => t.Rules)
			};
			return policy;
		}

		public int CheckBag(string bag) {
			return 0;
		}
	}
}