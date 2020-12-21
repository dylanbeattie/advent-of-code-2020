using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Day20Code {
	public static class StringExtensions {
		public static string Reverse(this string s) {
			var charArray = s.ToCharArray();
			Array.Reverse(charArray);
			return new string(charArray);
		}

		public static string[] RotateLeft(this IEnumerable<string> lines) {
			/* [ "abc", "def",  "ghi" ] => [ "cfi", "beh", "adg" ] */
			var input = lines.ToArray();
			var output = input[0].Select(_ => "").ToList();
			for (var c = input[0].Length - 1; c >= 0; c--) {
				foreach (var t in input) output[c] += t[c];
			}
			output.Reverse();
			return output.ToArray();
		}
		/*
		 * 			var s = new[] { "abc", "def", "ghi" };
			s.RotateRight().ShouldBe(new[] { "gda", "heb", "ifc" });

		 */
		public static string[] RotateRight(this IEnumerable<string> lines) {
			var input = lines.ToArray();
			var output = input[0].Select(_ => "").ToList();
			for (int c = 0; c < input[0].Length; c++) {
				for (int l = input.Length-1; l >= 0; l--) {
					output[c] += input[l][c];
				}
			}
			return output.ToArray();

		}

		public static int Width(this IList<string> thing) {
			return (thing[0].Length);
		}
	}
}