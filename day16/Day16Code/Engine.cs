using System.Collections.Generic;
using System.Linq;

namespace Day16Code {
	public class Engine {
		private IEnumerable<Rule> rules;

		public Engine(string rules) {
			this.rules = rules.Split("\n").Select(Rule.Parse);
		}

		private bool Validate(int value) => this.rules.Any(r => r.IsValid(value));

		public IEnumerable<int> FindInvalidValues(IEnumerable<int> ticket) {
			return ticket.Where(value => !Validate(value));
		}
	}
}