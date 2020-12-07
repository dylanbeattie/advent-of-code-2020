using System.Security.Authentication.ExtendedProtection;
using Day7Code;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using NUnit.Framework;
using Shouldly;

namespace Day7Test {

	public class ExampleTests {
		private Policy policy;

		private const string rules = @"light red bags contain 1 bright white bag, 2 muted yellow bags.
dark orange bags contain 3 bright white bags, 4 muted yellow bags.
bright white bags contain 1 shiny gold bag.
muted yellow bags contain 2 shiny gold bags, 9 faded blue bags.
shiny gold bags contain 1 dark olive bag, 2 vibrant plum bags.
dark olive bags contain 3 faded blue bags, 4 dotted black bags.
vibrant plum bags contain 5 faded blue bags, 6 dotted black bags.
faded blue bags contain no other bags.
dotted black bags contain no other bags.";

		public ExampleTests() {
			this.policy = Policy.ParseRules(rules);
		}

		[TestCase("shiny gold", 4)]
		public void CheckBags(string bag, int count) {
			policy.CountRootBags(bag).ShouldBe(count);
		}

	}

	public class TwoRuleTests {
		private Policy policy;

		private const string rules = @"shiny gold bags contain 1 dark olive bag.
dark olive bags contain no other bags";

		public TwoRuleTests() {
			this.policy = Policy.ParseRules(rules);
		}

		[TestCase("dark olive", 1)]
		[TestCase("shiny gold", 0)]
		public void CheckBags(string bag, int count) {
			policy.CountRootBags(bag).ShouldBe(count);
		}
	}
	public class OneRuleTests {
		private Policy policy;

		public OneRuleTests() {
			this.policy = Policy.ParseRules(rules);
		}

		private const string rules = @"dotted black bags contain no other bags.";
		[Test]
		[TestCase("dotted black", 0)]
		[TestCase("shiny gold", 0)]
		public void CheckBags(string bag, int count) {
			policy.CountRootBags(bag).ShouldBe(count);
		}
	}
}