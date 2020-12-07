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
				.Select(BagRule.FromToken)
				.ToDictionary(bagRule => bagRule.Name, bagRule => bagRule.Count);
			return (ruleName, rules);
		}

		private void UpdateMagicListThing() {
			var parents = new Dictionary<string, List<string>>();
			foreach (var thing in BagRules) {
				foreach (var rule in thing.Value) {
					if (!parents.ContainsKey(rule.Key)) parents[rule.Key] = new List<string>();
					parents[rule.Key].Add(thing.Key);
				}
			}

			this.MagicList = parents;
		}

		public Dictionary<string, List<string>> MagicList { get; set; }

		public Dictionary<string, Dictionary<string, int>> BagRules { get; set; }
	

		public static Policy ParseRules(string rules) {
			var policy = new Policy() {
				BagRules = rules
					.Split('\n', StringSplitOptions.RemoveEmptyEntries)
					.Select(line => line.Trim())
					.Select(ParseRule)
					.ToDictionary(t => t.Name, t => t.Rules)
			};
			policy.UpdateMagicListThing();
			return policy;
		}

		public int CountChildBags(string bag) {
			if (this.BagRules[bag].Count == 0) return 0;
			var rootRule = this.BagRules[bag];
			return rootRule.Sum(rule => rule.Value + (rule.Value * CountChildBags(rule.Key)));
		}

		public int CountRootBags(string bag) {
			var possibleRootBags = CheckBag(bag).ToList();
			return possibleRootBags.Distinct().ToList().Count;
		}

		public IEnumerable<string> CheckBag(string bag) {
			if (!MagicList.ContainsKey(bag)) return new List<string>();
			var directParents = MagicList[bag];
			var ancestors = directParents.SelectMany(CheckBag);
			return directParents.Concat(ancestors);
		}
	}
}