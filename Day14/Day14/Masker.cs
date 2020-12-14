using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;

namespace Day14 {
	public class Masker {
		public static long ApplyMask(string mask, in long input) {
			// Split the mask into a mask of 1s and a mask of 0s
			// base2 parse on each mask to get a 64-bit int
			// bitwise OR with the 1s and a bitwise AND with the 0s
			// return the result
			var orMaskString = mask.Replace("X", "0");
			var andMaskString = mask.Replace("X", "1");
			var orMask = Convert.ToInt64(orMaskString, 2);
			var andMask = Convert.ToInt64(andMaskString, 2);
			var result = (input | orMask) & andMask;
			return result;
		}

		public static IEnumerable<long> FuzzMask(string mask, long address) {
			return FuzzMask(mask.ToCharArray(), address);
		}

		public static IEnumerable<long> FuzzMask(char[] mask, long address) {
			var tails = mask[^1] switch {
				'X' => new[] {0L, 1},
				'0' => new[] {address & 1},
				_ => new[] {1L}
			};
			return mask.Length == 1 
				? tails 
				: tails
					.SelectMany(tail => FuzzMask(mask[..^1], address >> 1)
					.Select(head => head << 1 | tail));
		}
	}
}
