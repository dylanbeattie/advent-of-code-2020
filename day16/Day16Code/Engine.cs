using System.Collections.Generic;
using System.Linq;

namespace Day16Code {
	public class Engine {
		private IEnumerable<Rule> rules;

		public Engine(string rules) {
			this.rules = rules.Split("\n").Select(Rule.Parse);
		}

		private bool Invalid(int value) => this.rules.All(r => ! r.IsValid(value));

		public IList<int> FindInvalidValues(IList<int> ticket) => ticket.Where(Invalid).ToList();

		public bool IsValid(IList<int> ticket) => ! ticket.Any(Invalid);

		public IList<IList<Rule>> ListRulesForTicket(IList<int> ticket) => ticket.Select(ListRulesSatisfiedBy).ToList();

		public IList<Rule> ListRulesSatisfiedBy(int input) => rules.Where(rule => rule.IsValid(input)).ToList();

	}
}