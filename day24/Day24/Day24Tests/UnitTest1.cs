using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Security.Permissions;
using NUnit.Framework;
using Shouldly;

namespace Day24Tests {
	public class Tests {
		private readonly string[] example = @"sesenwnenenewseeswwswswwnenewsewsw
neeenesenwnwwswnenewnwwsewnenwseswesw
seswneswswsenwwnwse
nwnwneseeswswnenewneswwnewseswneseene
swweswneswnenwsewnwneneseenw
eesenwseswswnenwswnwnwsewwnwsene
sewnenenenesenwsewnenwwwse
wenwwweseeeweswwwnwwe
wsweesenenewnwwnwsenewsenwwsesesenwne
neeswseenwwswnwswswnw
nenwswwsewswnenenewsenwsenwnesesenew
enewnwewneswsewnwswenweswnenwsenwsw
sweneswneswneneenwnewenewwneswswnese
swwesenesewenwneswnwwneseswwne
enesenwswwswneneswsenwnewswseenwsese
wnwnesenesenenwwnenwsewesewsesesew
nenewswnwewswnenesenwnesewesw
eneswnwswnwsenenwnwnwwseeswneewsenese
neswnwewnwnwseenwseesewsenwsweewe
wseweeenwnesenwwwswnew".Split(Environment.NewLine);

		[Test]
		public void Test1() {
			var hc = new Honeycomb();
			hc.CountBlackTiles().ShouldBe(0);
		}

		[Test]
		public void Flip1Tile() {
			var hc = new Honeycomb();
			hc.FlipTile("e");
			hc.CountBlackTiles().ShouldBe(1);
		}

		[Test]
		[TestCase("w", -1, 0)]
		[TestCase("e", 1, 0)]

		[TestCase("nw", 0, -1)]
		[TestCase("se", 0, 1)]

		[TestCase("ne", 1, -1)]
		[TestCase("sw", -1, 1)]

		[TestCase("ee", 2, 0)]
		[TestCase("ww", -2, 0)]
		[TestCase("ew", 0, 0)]
		[TestCase("nwse", 0, 0)]
		[TestCase("nesw", 0, 0)]

		public void CheckCoordinatesForSteps(string steps, int xpected, int ypected) {
			var hc = new Honeycomb();
			var (x, y) = hc.MapCoordinates(steps);
			x.ShouldBe(xpected);
			y.ShouldBe(ypected);
		}

		[Test]
		public void Flip1TileTwice() {
			var hc = new Honeycomb();
			hc.FlipTile("e");
			hc.FlipTile("e");
			hc.CountBlackTiles().ShouldBe(0);
		}

		[Test]
		public void TextOnExampleInput() {
			var hc = new Honeycomb();
			hc.FlipTiles(example);
			hc.CountBlackTiles().ShouldBe(10);
		}

		[Test]
		public void SolvePart1() {
			var hc = new Honeycomb();
			hc.FlipTiles(File.ReadAllLines("input.txt"));
			hc.CountBlackTiles().ShouldBe(0);
		}
	}

	public class Honeycomb {
		private readonly bool[,] cells;
		private readonly (int X, int Y) ORIGIN = (64, 64);
		public Honeycomb(int size = 128) {
			cells = new bool[size, size];
		}

		public void FlipTiles(string[] list) {
			foreach (var steps in list) this.FlipTile(steps);
		}

		public void FlipTile(string steps) {
			var (x, y) = MapCoordinates(steps);
			x += ORIGIN.X;
			y += ORIGIN.Y;
			cells[x, y] = !cells[x, y];
		}

		public (int x, int y) MapCoordinates(string steps) {
			var i = 0;
			var x = 0;
			var y = 0;
			while (i < steps.Length) {
				switch (steps[i++]) {
					case 'e': x++; break;
					case 'w': x--; break;
					case 'n':
						y--;
						if (steps[i++] == 'e') x++;
						break;
					case 's':
						y++;
						if (steps[i++] == 'w') x--;
						break;
				}
			}
			return (x, y);
		}

		public int CountBlackTiles() {
			var count = 0;
			for (var x = 0; x < cells.GetLength(0); x++) {
				for (var y = 0; y < cells.GetLength(1); y++) {
					if (cells[x, y]) count++;
				}
			}
			return (count);
		}
	}
}