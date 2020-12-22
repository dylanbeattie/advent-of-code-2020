using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using Day22Code;
using NUnit.Framework;
using Shouldly;

namespace Day22Test {
	public class Tests {
		[SetUp]
		public void Setup() { }

		[Test]
		public void ParserWorksOnExample() {
			var (player1, player2) = CrabCombat.LoadGame("example1.txt");
			player1.ShouldBe(new List<int> { 9, 2, 6, 3, 1 });
			player2.ShouldBe(new List<int> { 5, 8, 4, 7, 10 });
		}

		[Test]
		public void Part1GamePlayWorksOnExample() {
			var (player1, player2) = CrabCombat.LoadGame("example1.txt");
			var (result1, result2) = CrabCombat.PlayGame(player1, player2);
			result1.ShouldBeEmpty();
			result2.ShouldBe(new List<int> { 3, 2, 10, 6, 8, 5, 9, 4, 7, 1 });
		}

		[Test]
		public void Part2GamePlayWorksOnExample() {
			var (player1, player2) = CrabCombat.LoadGame("example1.txt");
			var (result1, result2) = CrabCombat.PlayRecursiveGame(player1, player2);
			result1.ShouldBeEmpty();
			result2.ShouldBe(new List<int> { 7, 5, 6, 2, 4, 1, 10, 8, 9, 3 });
		}


		[Test]
		public void Part1GamePlayWorksOnRealInput() {
			var (player1, player2) = CrabCombat.LoadGame("input.txt");
			var (result1, result2) = CrabCombat.PlayGame(player1, player2);
			result1.ShouldBeEmpty();
			result2.ShouldBe(new List<int> {
				47, 30, 43, 22, 44, 31, 32, 17, 36, 24, 27, 9, 5, 2, 50, 37, 39, 21, 45, 15, 11, 1, 49, 41, 26, 14, 46,
				38, 25, 7, 48, 12, 23, 6, 34, 33, 16, 3, 42, 29, 40, 19, 35, 28, 20, 10, 18, 8, 13, 4
			});
		}

		[Test]
		public void ScoreCalculatorWorksOnExample() {
			var (player1, player2) = CrabCombat.LoadGame("example1.txt");
			var (result1, result2) = CrabCombat.PlayGame(player1, player2);
			CrabCombat.CalculateScore(result2).ShouldBe(306);
		}

		[Test]
		public void ScoreCalculatorWorksOnRealInput() {
			var (player1, player2) = CrabCombat.LoadGame("input.txt");
			var (result1, result2) = CrabCombat.PlayGame(player1, player2);
			CrabCombat.CalculateScore(result2).ShouldBe(35397);
		}

		[Test]
		public void Test1() {
			Assert.Pass();
		}
	}




}