using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Day20Code {
	public class FlippedTile {

		public int Top { get; set; }
		public int Bottom { get; set; }
		public int Left { get; set; }
		public int Right { get; set; }
		public Transform Transform { get; set; }
		public string[] Contents { get; set;  }

		public FlippedTile FindNeighbour(Direction d, IEnumerable<Tile> candidates) {
			return FindNeighbour(d, candidates.SelectMany(tile => tile.Flips.Values));
		}

		public FlippedTile FindNeighbour(Direction d, IEnumerable<FlippedTile> candidates) {
			var list = candidates.Where(c => c.Tile != this.Tile).ToList();
			switch (d) {
				case Direction.Right:
					var foo = list.Where(c => c.Left == this.Right);
					var bar = foo.FirstOrDefault(c => c.FindNeighbour(Direction.Up, list) == default);
					return bar;
				case Direction.Up:
					var possibles = list.Where(c => c.Bottom == this.Top).ToList();
					return possibles.FirstOrDefault();
				case Direction.Down:
					var downFoo = list
						.Where(c => c.Top == this.Bottom);
					var downBar = downFoo.FirstOrDefault(c => c.FindNeighbour(Direction.Left, list) == default);
					return downBar;
				case Direction.Left:
					var potentials = list.Where(c => c.Right == this.Left);
					return potentials.FirstOrDefault();
				default:
					throw new ArgumentOutOfRangeException(nameof(d), d, null);
			}
		}

		//public FlippedTile FindRightNeighbour(IEnumerable<FlippedTile> candidates) {
		//	return matchesWithNoUpNeighbours.FirstOrDefault();
		//}

		//public FlippedTile FindLeftNeighbour(IEnumerable<FlippedTile> candidates) {
		//	var matches = candidates.Where(c => c.Right == this.Left);
		//	return matches.FirstOrDefault();
		//}


		//public FlippedTile FindDownNeighbour(IEnumerable<FlippedTile> candidates) {
		//	var matches = candidates.Where(c => c.Top == this.Bottom);
		//	return matches.FirstOrDefault();
		//}

		//public FlippedTile FindUpNeighbour(IEnumerable<FlippedTile> candidates) {
		//	var matches = candidates.Where(c => c.Bottom == this.Top);
		//	return matches.FirstOrDefault();
		//}


		public FlippedTile(string[] rows, Tile tile, Transform transform) {
			this.Tile = tile;
			this.Transform = transform;
			this.Top = Convert.ToInt32(rows.First(), 2);
			this.Right = Convert.ToInt32(new String(rows.Select(row => row[^1]).ToArray()), 2);
			this.Bottom = Convert.ToInt32(rows.Last(), 2);
			this.Left = Convert.ToInt32(new String(rows.Select(row => row[0]).ToArray()), 2);
			// this.Contents = rows;
			this.Contents = rows[1..^1].Select(line => new String(line[1..^1])).ToArray();
		}

		public Tile Tile { get; set; }

		public IEnumerable<int> AllEdges {
			get {
				yield return Top;
				yield return Bottom;
				yield return Left;
				yield return Right;
			}
		}
		public override string ToString() {
			return ($"ID: {this.Tile.Id} : T={Top} R={Right} B={Bottom} L={Left} ({Transform})");
		}
	}
}