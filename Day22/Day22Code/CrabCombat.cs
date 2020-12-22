using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day22Code {
	public class CrabCombat {
		public static int CalculateScore(IList<int> winningHand) {
			var multiplier = 1;
			return winningHand.Reverse().Sum(card => card * multiplier++);
		}

		public static (IList<int>, IList<int>) PlayGame(IList<int> player1, IList<int> player2) {
			while (true) {
				if (player1.Count == 0 || player2.Count == 0) return (player1, player2);
				var card1 = player1.First();
				player1.RemoveAt(0);
				var card2 = player2.First();
				player2.RemoveAt(0);
				if (card1 > card2) {
					player1.Add(card1);
					player1.Add(card2);
				} else {
					player2.Add(card2);
					player2.Add(card1);
				}
			}
		}

		private static string CreateHashKey(List<int> list1, List<int> list2) {
			return String.Join(",", list1.ToArray()) + "/" + String.Join(",", list2.ToArray());
		}


		public static (IList<int>, IList<int>) PlayRecursiveGame(List<int> player1, List<int> player2, int game = 1) {
			var round = 0;
			var seen = new HashSet<string>();
			//Console.WriteLine($"=== Game {game} ===");
			//Console.WriteLine();
			while (true) {
				var hashKey = CreateHashKey(player1, player2);
				if (seen.Contains(hashKey)) return (new List<int> { 1 }, new List<int>());
				seen.Add(hashKey);
				round++;
				//Console.WriteLine($"-- Round {round} (Game {game}) --");
				//Console.WriteLine($"Player 1's deck: {String.Join(", ", player1.ToArray())}");
				//Console.WriteLine($"Player 2's deck: {String.Join(", ", player2.ToArray())}");
				if (player1.Count == 0 || player2.Count == 0) return (player1, player2);
				var card1 = player1.First();
				player1.RemoveAt(0);
				var card2 = player2.First();
				player2.RemoveAt(0);

				//Console.WriteLine($"Player 1 plays: {card1}");
				//Console.WriteLine($"Player 2 plays: {card2}");
				List<int> winner;
				List<int> cards;
				if (player1.Count >= card1 && player2.Count >= card2) {
					var (subGameResult1, subGameResult2) = PlayRecursiveGame(player1.Take(card1).ToList(), player2.Take(card2).ToList(), game + 1);
					if (subGameResult1.Any()) {
						winner = player1;
						cards = new List<int> { card1, card2 };
					} else {
						winner = player2;
						cards = new List<int> { card2, card1 };
					}
				} else {
					if (card1 > card2) {
						winner = player1;
						cards = new List<int> { card1, card2 };
					} else {
						winner = player2;
						cards = new List<int> { card2, card1 };
					}
					//Console.WriteLine($"Player {(winner == player1 ? 1 : 2)} wins round {round} of game {game}!");
				}
				winner.AddRange(cards);
				//Console.WriteLine();
			}
		}

		public static (List<int>, List<int>) LoadGame(string filename) {
			var player1 = new List<int>();
			var player2 = new List<int>();
			var target = player1;
			foreach (var line in File.ReadAllLines(filename)) {
				if (line.StartsWith("Player ")) continue;
				if (String.IsNullOrEmpty(line)) {
					target = player2;
					continue;
				}
				target.Add(Int32.Parse(line));
			}
			return (player1, player2);
		}
	}
}