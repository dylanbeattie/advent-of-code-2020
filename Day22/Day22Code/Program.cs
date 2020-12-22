using System;
using System.Linq;

namespace Day22Code {
	class Program {
		static void Main(string[] args) {
			var (player1, player2) = CrabCombat.LoadGame("input.txt");
			var (result1, result2) = CrabCombat.PlayRecursiveGame(player1, player2);
			Console.WriteLine($"Player 1: {String.Join(", ", result1.ToArray())}");
			Console.WriteLine($"Player 2: {String.Join(", ", result2.ToArray())}");
			var score = CrabCombat.CalculateScore(result2);
			Console.WriteLine(score);
		}
	}
}
