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
			var suffixes = mask[^1] switch {
				'X' => new List<long> {0, 1},
				'0' => new List<long> {address & 1},
				_ => new List<long> {1L}
			};

			foreach (var suffix in suffixes) {
				if (mask.Length == 1) {
					yield return suffix;
				} else {
					foreach (var prefix in FuzzMask(mask[..^1], address >> 1)) {
						yield return prefix << 1 | suffix;
					}
				}
			}
		}
	}
}
