using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Day20Code {
	public class Tile {
		public long Id { get; set; }
		public Dictionary<Transform, FlippedTile> Flips { get; set; } = new Dictionary<Transform, FlippedTile>();

		public Tile(string input) {
			var lines = input.Split("\n");
			this.Id = Int64.Parse(lines.First().Replace("Tile ", "").Replace(":", ""));
			var rows = lines.Skip(1).Select(s => s.Replace(".", "0").Replace("#", "1")).ToArray();
			this.Flips.Add(Transform.None, new FlippedTile(rows, this, Transform.None));
			this.Flips.Add(Transform.FlipTopBottom, new FlippedTile(rows.Reverse().ToArray(), this, Transform.FlipTopBottom));
			this.Flips.Add(Transform.FlipLeftRight, new FlippedTile(rows.Select(r => r.Reverse()).ToArray(), this, Transform.FlipLeftRight));
			this.Flips.Add(Transform.RotateLeft, new FlippedTile(rows.RotateLeft(), this, Transform.RotateLeft));
			this.Flips.Add(Transform.RotateRight, new FlippedTile(rows.RotateRight(), this, Transform.RotateRight));

			var bothFlippedRows = rows.Select(r => r.Reverse()).Reverse().ToArray();
			this.Flips.Add(Transform.FlipBoth, new FlippedTile(bothFlippedRows, this, Transform.FlipBoth));

			this.Flips.Add(Transform.FlipVerticalThenRotateLeft, new FlippedTile(rows.Reverse().RotateLeft(), this, Transform.FlipVerticalThenRotateLeft));
			this.Flips.Add(Transform.FlipVerticalThenRotateRight, new FlippedTile(rows.Reverse().RotateRight(), this, Transform.FlipVerticalThenRotateRight));
		}

		public FlippedTile FindNeighbour(Direction d, Transform transform, IEnumerable<Tile> tiles) {
			return Flips[transform].FindNeighbour(d, tiles.SelectMany(t => t.Flips.Values));
		}

		public IEnumerable<int> AllEdges {
			get { return Flips.SelectMany(kvp => kvp.Value.AllEdges); }
		}

		public override string ToString() {
			return $"{Id}:" +
				   $"  None: {this.Flips[Transform.None]}" +
				   $"  ToBo: {this.Flips[Transform.FlipTopBottom]}" +
				   $"  LeRi: {this.Flips[Transform.FlipLeftRight]}" +
				   $"  FlipBoth: {this.Flips[Transform.FlipBoth]}";
		}
	}
}