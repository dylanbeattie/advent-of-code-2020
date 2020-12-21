using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace Day20Code {
	public class Program {
		static void Main(string[] args) {
			var input = File.ReadAllText("input.txt").Replace("\r", "")
				.Split("\n\n", StringSplitOptions.RemoveEmptyEntries);
			Console.WriteLine(SolvePart2(input));
		}

		public static int SolvePart2(string[] input) {
			var tiles = input.Select(block => new Tile(block)).ToList();
			var cornerIds = FindCornerIds(tiles);
			var corner = tiles.First(t => cornerIds.Contains(t.Id));
			var allOtherTiles = tiles.Except(new[] { corner }).ToList();
			var puzzle = new FlippedTile[12, 12];

			foreach (Transform flip in Enum.GetValues(typeof(Transform))) {
				var neighbor = corner.FindNeighbour(Direction.Right, flip, allOtherTiles);
				if (neighbor == default) continue;
				puzzle[0, 0] = corner.Flips[flip];
				allOtherTiles.Remove(neighbor.Tile);
				puzzle[1, 0] = neighbor;
				break;
			}

			for (var y = 0; y < 12; y++) {
				for (var x = 0; x < 11; x++) {
					if (x == 0 && y == 0) continue; // top left corner already done!
					var rightNeighbour = puzzle[x, y].FindNeighbour(Direction.Right, allOtherTiles);
					allOtherTiles.Remove(rightNeighbour.Tile);
					puzzle[x + 1, y] = rightNeighbour;
				}

				if (y == 11) break;
				var downNeighbour = puzzle[0, y].FindNeighbour(Direction.Down, allOtherTiles);
				allOtherTiles.Remove(downNeighbour.Tile);
				puzzle[0, y + 1] = downNeighbour;
			}
			// SOLVED PUZZLE
			var image = new List<string>();
			for (var y = 0; y < 12; y++) {
				for (var r = 0; r < puzzle[0, y].Contents.Length; r++) {
					var dump = "";
					for (var x = 0; x < 12; x++) {
						dump += puzzle[x, y].Contents[r].Replace("0", " ").Replace("1", "#");
					}
					image.Add(dump);
				}
			}

			const int HASHES_PER_MONSTER = 15;
			var correctedImage = image.RotateRight(); // Found this manually. It's fine.
			var howManyMonsters = FindMonsters(correctedImage).Count();
			var totalHashCount = CountHashes(correctedImage);

			Console.WriteLine($"Image has {howManyMonsters} monsters. {totalHashCount} total hashes.");
			var result = totalHashCount - (howManyMonsters * HASHES_PER_MONSTER);
			return result;
		}

		public static int CountHashes(IList<string> image) => image.Sum(line => line.Count(c => c == '#'));
		

		public static IEnumerable<(int, int)> FindMonsters(IList<string> image) {
			for (var x = 0; x <= image.Width() - 20; x++) {
				for (var y = 0; y <= image.Count - 3; y++) {
					if (FindSeaMonster(image, x, y)) yield return (x, y);
				}
			}
		}

		public static bool FindSeaMonster(IList<string> image, int x, int y) {
			var seaMonsterHead = new Regex("^..................#.");
			var seaMonsterBody = new Regex("^#....##....##....###");
			var seaMonsterFeet = new Regex("^.#..#..#..#..#..#...");
			return seaMonsterHead.IsMatch(image[y].Substring(x))
				   && seaMonsterBody.IsMatch(image[y + 1].Substring(x))
				   && seaMonsterFeet.IsMatch(image[y + 2].Substring(x));
		}


		public static long SolvePart1(IEnumerable<string> input) {
			var tiles = input.Select(block => new Tile(block)).ToList();
			var cornerIds = FindCornerIds(tiles);
			var product = cornerIds.Aggregate((a, b) => a * b);
			return (product);
		}

		public static long[] FindCornerIds(List<Tile> tiles) {
			var tileIds = new Dictionary<int, List<long>>();
			foreach (var tile in tiles) {
				foreach (var edge in tile.AllEdges) {
					if (!tileIds.ContainsKey(edge)) tileIds.Add(edge, new List<long>());
					tileIds[edge].Add(tile.Id);
				}
			}

			var allTheNumbers = tiles.SelectMany(tile => tile.AllEdges).ToList();
			var groups = allTheNumbers.GroupBy(n => n).OrderBy(group => group.Count()).ToList();
			var edges = groups.Where(g => g.Count() == 4).Select(g => g.Key).ToList();
			var groupings = edges.SelectMany(edge => tileIds[edge]).GroupBy(tileId => tileId).ToList();
			var orderings = groupings.OrderByDescending(group => group.Count());
			var cornerIds = orderings.Take(4).Select(group => group.Key).ToArray();
			return cornerIds;
		}
	}
}
