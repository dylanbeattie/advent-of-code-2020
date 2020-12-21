using System;
using System.IO;
using Day20Code;
using NUnit.Framework;
using Shouldly;

namespace Day20Tests {
	public class Tests {
		//[SetUp]
		//public void Setup() {
		//}

		[Test]
		public void Test1() {
			var input = File.ReadAllText("input.txt").Replace("\r", "")
				.Split("\n\n", StringSplitOptions.RemoveEmptyEntries);
			var result = Program.SolvePart1(input);
			result.ShouldBe(16192267830719);
		}

		[Test]
		public void RotateStringLeftWorks() {
			var s = new[] { "abc", "def", "ghi" };
			s.RotateLeft().ShouldBe(new[] {"cfi", "beh", "adg"});
		}

		[Test]
		public void RotateStringRightWorks() {
			var s = new[] { "abc", "def", "ghi" };
			s.RotateRight().ShouldBe(new[] { "gda", "heb", "ifc" });
		}


	}
}