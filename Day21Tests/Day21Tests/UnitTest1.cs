using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Shouldly;

namespace Day21Tests {
	public class Tests {

		[Test]
		public void ParserReturnsEmptyListFOrEmptyString() {
			Parser.Parse("").ShouldBeEmpty();
		}

		[Test]
		public void ParserFindsSingleFoodWithSingleIngredientAndAllergen() {
			var foods = Parser.Parse("zskm (contains fish)").ToList();
			foods.Count.ShouldBe(1);
			var food = foods.First();
			food.Ingredients.Count.ShouldBe(1);
			food.Ingredients.ShouldContain("zskm");
			food.Allergens.Count.ShouldBe(1);
			food.Allergens.ShouldContain("fish");
		}

		[Test]
		[TestCase("zskm (contains fish)\nfzzz (contains wheat)")]
		[TestCase("zskm (contains fish)\nfzzz (contains wheat)\n\n\n")]
		[TestCase("zskm (contains fish)\r\nfzzz (contains wheat)")]
		[TestCase("zskm (contains fish)\r\nfzzz (contains wheat)\r\n")]
		public void ParserFindsTwoSingleFoodsWithSingleIngredientAndAllergen(string input) {
			var foods = Parser.Parse(input).ToList();
			foods.Count.ShouldBe(2);
			foods[0].Ingredients.Count.ShouldBe(1);
			foods[0].Ingredients.ShouldContain("zskm");
			foods[0].Allergens.Count.ShouldBe(1);
			foods[0].Allergens.ShouldContain("fish");
			foods[1].Ingredients.Count.ShouldBe(1);
			foods[1].Ingredients.ShouldContain("fzzz");
			foods[1].Allergens.Count.ShouldBe(1);
			foods[1].Allergens.ShouldContain("wheat");
		}

	}

	public class Food {
		public Food(string line) {
			var halves = line.TrimEnd(')').Split(" (contains ");
			this.Ingredients = halves[0].Split(" ").ToList();
			this.Allergens = halves[1].Split(" ").ToList();
		}

		public List<string> Ingredients { get; set; }
		public List<string> Allergens { get; set; }
	}

	public class Parser {
		public static IEnumerable<Food> Parse(string input) {
			var lines = input.Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries);
			return lines.Select(line => new Food(line));
		}
	}
}