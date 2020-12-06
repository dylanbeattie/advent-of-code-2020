using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using NUnit.Framework;
using Shouldly;

namespace Day6Test {
	public class Tests {

		public static IEnumerable<(string input, int count)> GeneratePart1TestData() {
			yield return ("abc", 3);
			yield return ("a\nb\nc", 3);
			yield return ("ab\nac", 3);
			yield return ("a\na\na\na", 1);
			yield return ("b", 1);
			yield return ("abcdefghijklmnopqrstuvwxyz", 26);
			yield return ("abcdefghijklmnopqrstuvwxyz\nabcdefghijklmnopqrstuvwxyz", 26);
			yield return ("abcde\nabcde\nabcde\nabcde", 5);
			yield return ("", 0);
		}

		public static IEnumerable<(string input, int count)> GeneratePart2TestData() {
			yield return ("abc", 3);
			yield return ("a\nb\nc", 0);
			yield return ("ab\nac", 1);
			yield return ("a\na\na\na", 1);
			yield return ("b", 1);
			yield return ("abcdefghijklmnopqrstuvwxyz", 26);
			yield return ("abcdefghijklmnopqrstuvwxyz\nabcdefghijklmnopqrstuvwxyz", 26);
			yield return ("abcde\nabcde\nabcde\nabcde", 5);
			yield return ("", 0);
			yield return ("abcde\nfghij\nklmnop\nqrstuv\nwxyz", 0);
			yield return ("abc\nabc\nabc\n", 3);
		}


		[Test]
		[TestCaseSource(nameof(GeneratePart2TestData))]
		public void Test1((string input, int count) tuple) {
			var result = Day6Code.Program.ParseCustomsDeclarationFormUsingPart2Rules(tuple.input);
			result.ShouldBe(tuple.count);
		}
	}
}