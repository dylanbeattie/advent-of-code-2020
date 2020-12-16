using Day16Code;
using NUnit.Framework;
using NUnit.Framework.Internal;
using Shouldly;

namespace Day16Test {
	public class Tests {
		[SetUp]
		public void Setup() {
		}

		[Test]
		[TestCase("1-2", 2, true)]
		[TestCase("1-2", 0, false)]
		[TestCase("800-900", 800, true)]
		[TestCase("800-900", 900, true)]
		[TestCase("800-900", 799, false)]
		[TestCase("800-900", 901, false)]
		public void CheckWorks(string range, int value, bool valid) {
			var c = Check.Parse(range);
			Assert.AreEqual(c.IsValid(value), valid);
		}

		[Test]
		[TestCase("class: 28-138 or 145-959", 27, false)]
		[TestCase("class: 28-138 or 145-959", 28, true)]
		[TestCase("class: 28-138 or 145-959", 138, true)]
		[TestCase("class: 28-138 or 145-959", 139, false)]
		[TestCase("class: 28-138 or 145-959", 144, false)]
		[TestCase("class: 28-138 or 145-959", 960, false)]
		[TestCase("class: 28-138 or 145-959", 145, true)]
		[TestCase("class: 28-138 or 145-959", 959, true)]
		public void TestRule(string ruleString, int value, bool valid) {
			var rule = Rule.Parse(ruleString);
			rule.IsValid(value).ShouldBe(valid);
		}

		[Test]
		[TestCase("big dog: 1-2 or 3-4", "big dog")]
		[TestCase("gold shiny bag: 100-200 or 300-400", "gold shiny bag")]
		public void TestRuleNameParser(string ruleString, string name) {
			var rule = Rule.Parse(ruleString);
			rule.Name.ShouldBe(name);

		}
	}
}