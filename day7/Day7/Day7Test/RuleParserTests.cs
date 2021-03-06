using Day7Code;
using NUnit.Framework;
using Shouldly;

namespace Day7Test {
	public class RuleParserTests {
		[Test]
		public void ParseEmptyRuleSet() {
			var rules = Policy.ParseRule("dotted black bags contain no other bags.");
			rules.Name.ShouldBe("dotted black");
			rules.Rules.ShouldBeEmpty();
		}

		[Test]
		public void ParseSingleRuleSet() {
			var rules = Policy.ParseRule("bright white bags contain 1 shiny gold bag.");
			rules.Name.ShouldBe("bright white");
			rules.Rules.Count.ShouldBe(1);
			rules.Rules["shiny gold"].ShouldBe(1);
		}

		[Test]
		public void ParseTwoRuleSet() {
			var rules = Policy.ParseRule("muted yellow bags contain 2 shiny gold bags, 9 faded blue bags.");
			rules.Name.ShouldBe("muted yellow");
			rules.Rules.Count.ShouldBe(2);
			rules.Rules["shiny gold"].ShouldBe(2);
			rules.Rules["faded blue"].ShouldBe(9);
		}

		[Test]
		public void ParseReallyComplicatedRuleSet() {
			var rules = Policy.ParseRule("hot pink bags contain 12345678 neon yellow bags, 8 luminous green bags, 1 mustard yellow bag, 5 pillarbox red bags, 1 sea green bag.");
			rules.Name.ShouldBe("hot pink");
			rules.Rules.Count.ShouldBe(5);
			rules.Rules["neon yellow"].ShouldBe(12345678);
			rules.Rules["luminous green"].ShouldBe(8);
			rules.Rules["mustard yellow"].ShouldBe(1);
			rules.Rules["pillarbox red"].ShouldBe(5);
			rules.Rules["sea green"].ShouldBe(1);
		}
	}
}