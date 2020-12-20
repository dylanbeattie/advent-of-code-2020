using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Day20Code {
	class Program {
		static void Main(string[] args) {
			var input = File.ReadAllText("input.txt").Replace("\r", "")
				.Split("\n\n", StringSplitOptions.RemoveEmptyEntries);
			var tiles = input.Select(block => new Tile(block)).ToList();
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

	class FlippedTile {

		public int Top { get; set; }
		public int Bottom { get; set; }
		public int Left { get; set; }
		public int Right { get; set; }
		public Flip Flip { get; set; }

		public FlippedTile(string[] rows, Flip flip) {
			this.Flip = flip;
			this.Top = Convert.ToInt32(rows.First(), 2);
			this.Right = Convert.ToInt32(new String(rows.Select(row => row[^1]).ToArray()), 2);
			this.Bottom = Convert.ToInt32(rows.Last(), 2);
			this.Left = Convert.ToInt32(new String(rows.Select(row => row[0]).ToArray()), 2);
		}
		public IEnumerable<int> AllEdges {
			get {
				yield return Top;
				yield return Bottom;
				yield return Left;
				yield return Right;
			}
		}
		public override string ToString() {
			return ($"{Top} {Right} {Bottom} {Left} ({Flip})");
		}
	}

	class Tile {
		public long Id { get; set; }
		public Dictionary<Flip, FlippedTile> Flips { get; set; } = new Dictionary<Flip, FlippedTile>();

		public Tile(string input) {
			var lines = input.Split("\n");
			this.Id = Int64.Parse(lines.First().Replace("Tile ", "").Replace(":", ""));
			var rows = lines.Skip(1).Select(s => s.Replace(".", "0").Replace("#", "1")).ToArray();
			this.Flips.Add(Flip.None, new FlippedTile(rows, Flip.None));
			this.Flips.Add(Flip.TopBottom, new FlippedTile(rows.Reverse().ToArray(), Flip.TopBottom));
			this.Flips.Add(Flip.LeftRight, new FlippedTile(rows.Select(r => r.Flip()).ToArray(), Flip.LeftRight));
			this.Flips.Add(Flip.Both, new FlippedTile(rows.Select(r => r.Flip()).Reverse().ToArray(), Flip.Both));
		}

		public IEnumerable<int> AllEdges {
			get { return Flips.SelectMany(kvp => kvp.Value.AllEdges); }
		}

		public override string ToString() {
			return $"{Id}:" +
				   $"  None: {this.Flips[Flip.None]}" +
				   $"  ToBo: {this.Flips[Flip.TopBottom]}" +
				   $"  LeRi: {this.Flips[Flip.LeftRight]}" +
				   $"  Both: {this.Flips[Flip.Both]}";
		}
	}

	public enum Flip {
		None,
		LeftRight,
		TopBottom,
		Both
	}

	public static class StringExtensions {
		public static string Flip(this string s) {
			var charArray = s.ToCharArray();
			Array.Reverse(charArray);
			return new string(charArray);
		}
	}
}
