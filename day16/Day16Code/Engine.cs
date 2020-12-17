using System.Collections.Generic;
using System.Linq;

namespace Day16Code {
	public class Engine {
		private readonly IEnumerable<Rule> rules;

		public Engine(string rules) {
			this.rules = rules.Split("\n").Select(Rule.Parse);
		}
		private bool Invalid(int value) => this.rules.All(r => !r.IsValid(value));
		public IEnumerable<int> FindInvalidValues(int[] ticket) => ticket.Where(Invalid);
		public bool IsValid(int[] ticket) => !ticket.Any(Invalid);
		public string[][] ListRulesForTicket(int[] ticket) => ticket.Select(ListRulesSatisfiedBy).ToArray();
		public string[] ListRulesSatisfiedBy(int input) => rules.Where(rule => rule.IsValid(input)).Select(rule => rule.Name).ToArray();
	}
}