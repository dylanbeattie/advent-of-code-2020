using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Linq;

namespace Day20Code {
	class Program {
		static void Main(string[] args) {
			var input = File.ReadAllText("input.txt").Replace("\r", "")
				.Split("\n\n", StringSplitOptions.RemoveEmptyEntries);
			var tiles = input.SelectMany(Tile.Parse).ToList();
			var tileIds = new Dictionary<int, List<long>>();
			foreach (var tile in tiles) {
				foreach (var edge in tile.AllEdges) {
					if (!tileIds.ContainsKey(edge)) tileIds.Add(edge, new List<long>());
					tileIds[edge].Add(tile.Id);
				}
			}

			var allTheNumbers = tiles.SelectMany(tile => tile.AllEdges).ToList();
			var groups = allTheNumbers.GroupBy(n => n).OrderBy(group => group.Count()).ToList();
			var edges = groups.Where(g => g.Count() == 2).Select(g => g.Key).ToList();
			var groupings = edges.SelectMany(edge => tileIds[edge]).GroupBy(tileId => tileId).ToList();
			var orderings = groupings.OrderByDescending(group => group.Count());
			var cornerIds = orderings.Take(4).Select(group => group.Key);
			var product = cornerIds.Aggregate((a, b) => a * b);
			Console.WriteLine(product);
			//foreach (var id in cornerIds) Console.WriteLine(id);
			Console.ReadKey(false);
		}
	}

	class Tile {
		public long Id { get; set; }
		public int Top { get; set; }
		public int Bottom { get; set; }
		public int Left { get; set; }
		public int Right { get; set; }

		public static IEnumerable<Tile> Parse(string input) {
			var lines = input.Split("\n");
			var id = Int64.Parse(lines.First().Replace("Tile ", "").Replace(":", ""));
			var rows = lines.Skip(1).Select(s => s.Replace(".", "0").Replace("#", "1")).ToList();
			var t = rows.First();
			var b = rows.Last();
			var l = new String(rows.Select(row => row[0]).ToArray());
			var r = new String(rows.Select(row => row[^1]).ToArray());
			yield return new Tile(id, t, r, b, l);
			yield return new Tile(id, t, r.Flip(), b, l.Flip());
			yield return new Tile(id, t.Flip(), r.Flip(), b.Flip(), l.Flip());
			yield return new Tile(id, t.Flip(), r, b.Flip(), l);
		}

		public IEnumerable<int> AllEdges {
			get {
				yield return Top;
				yield return Bottom;
				yield return Left;
				yield return Right;
			}
		}

		private Tile(long id, string top, string right, string bottom, string left) {
			this.Id = id;
			this.Top = Convert.ToInt32(top, 2);
			this.Right = Convert.ToInt32(right, 2);
			this.Bottom = Convert.ToInt32(bottom, 2);
			this.Left = Convert.ToInt32(left, 2);
		}

		public override string ToString() {
			return ($"ID {Id} : {Top} {Right} {Bottom} {Left}");
		}
	}

	public static class StringExtensions {
		public static string Flip(this string s) {
			var charArray = s.ToCharArray();
			Array.Reverse(charArray);
			return new string(charArray);
		}
	}
}
