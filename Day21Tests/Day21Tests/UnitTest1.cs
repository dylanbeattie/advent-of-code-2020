using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Shouldly;

namespace Day21Tests {
	public class Tests {
		/*
		 * mxmxvkd kfcds sqjhc nhms (contains dairy, fish)
		 * trh fvjkl sbzzf mxmxvkd (contains dairy)
		 * sqjhc fvjkl (contains soy)
		 * sqjhc mxmxvkd sbzzf (contains fish)
		 */
		[Test]
		public void ParserParsesInputFile() {
			var foods = Parser.ReadFile("example.txt").ToList();
			foods.Count().ShouldBe(4);
			foods[0].Allergens.ShouldBe(new[] { "dairy", "fish" });
			foods[1].Ingredients.ShouldBe(new[] { "trh", "fvjkl", "sbzzf", "mxmxvkd" });
			foods[3].Allergens.ShouldContain("fish");
		}

		[Test]
		public void FindAllergens() {
			var foods = Parser.ReadFile("example.txt").ToList();
			var a = new Analyzer();
			var safe = a.ListAllergens(foods);
			safe.Count.ShouldBe(3);
			
			//safe.ShouldContain(s => s == "kfcds", 1);
			//safe.ShouldContain(s => s == "nhms", 1);
			//safe.ShouldContain(s => s == "sbzzf", 2);
			//safe.ShouldContain(s => s == "trh", 1);
		}

		[Test]
		public void SolvePart1() {
			var foods = Parser.ReadFile("input.txt").ToList();
			var a = new Analyzer();
			a.SolvePart1(foods).ShouldBe(2389);
		}

		[Test]
		public void SolvePart2() {
			var foods = Parser.ReadFile("input.txt").ToList();
			var a = new Analyzer();
			var list = a.SolvePart2(foods);
			list.ShouldBe("fsr,skrxt,lqbcg,mgbv,dvjrrkv,ndnlm,xcljh,zbhp");
		}

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

	public class Analyzer {
		public Dictionary<string,string> ListAllergens(IEnumerable<Food> foods) {
			var candidates = new Dictionary<string, List<List<string>>>();
			foreach (var food in foods) {
				foreach (var allergen in food.Allergens) {
					if (!candidates.ContainsKey(allergen)) candidates.Add(allergen, new List<List<string>>());
					candidates[allergen].Add(food.Ingredients);
				}
			}

			var shortlist = candidates.Keys.ToDictionary(alg => alg,
				alg => candidates[alg].Aggregate((l1, l2) => l1.Intersect(l2).ToList()));
			var results = new Dictionary<string, string>();

			while (shortlist.Any()) {
				var matches = shortlist.Where(r => r.Value.Count == 1).ToList();
				foreach (var match in matches) {
					shortlist.Remove(match.Key);
					results.Add(match.Key, match.Value.First());
				}

				foreach (var thing in shortlist) {
					foreach (var food in results.Values) thing.Value.Remove(food);
				}
			}

			return results;
		}

		public string SolvePart2(IEnumerable<Food> foods) {
			var results = this.ListAllergens(foods);
			return String.Join(",", results.OrderBy(pair => pair.Key).Select(pair => pair.Value).ToArray());
		}

		public int SolvePart1(IEnumerable<Food> foods) {
			var results = this.ListAllergens(foods);
			var allIngredientsInList = foods.SelectMany(food => food.Ingredients);
			var snacks = allIngredientsInList.Where(i => ! results.Values.Contains(i)).ToList();
			return snacks.Count;
		}
	}

	public class Food {
		public Food(string line) {
			var halves = line.TrimEnd(')').Split(" (contains ");
			this.Ingredients = halves[0].Split(" ").ToList();
			this.Allergens = halves[1].Split(", ").ToList();
		}

		public List<string> Ingredients { get; set; }
		public List<string> Allergens { get; set; }
	}

	public class Parser {
		public static IEnumerable<Food> Parse(string input) {
			var lines = input.Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries);
			return lines.Select(line => new Food(line));
		}

		public static IEnumerable<Food> ReadFile(string filename) {
			return (Parse(File.ReadAllText(filename)));
		}
	}
}