using System.IO;
using Day16Code;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using NUnit.Framework;
using NUnit.Framework.Internal;
using Shouldly;
using Program = Day16Code.Program;

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

		[Test]
		public void TestPart1OnRealInput() {
			var input = File.ReadAllText("input.txt");
			var solution = Program.SolveAdventOfCodePart1(input);
			solution.ShouldBe(29019);
		}

		[Test]
		public void TestParse2OnRealInput() {
			var input = File.ReadAllText("input.txt");
			var solution = Program.SolveAdventOfCodePart2(input);
			solution.ShouldBe(517827547723L);
		}

		[Test]
		public void TestPart1OnExampleInput() {
			var input = @"class: 1-3 or 5-7
row: 6-11 or 33-44
seat: 13-40 or 45-50

your ticket:
7,1,14

nearby tickets:
7,3,47
40,4,50
55,2,20
38,6,12";

			var solution = Program.SolveAdventOfCodePart1(input);
			solution.ShouldBe(71);
		}
	}
}