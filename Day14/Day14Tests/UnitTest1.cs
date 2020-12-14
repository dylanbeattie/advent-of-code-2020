using System.ComponentModel;
using System.Linq;
using Day14;
using NUnit.Framework;
using Shouldly;

namespace Day14Tests {
	public class Tests {
		[SetUp]
		public void Setup() {
		}

		[Test]
		[TestCase(11, 73, "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX1XXXX0X")]
		[TestCase(101, 101, "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX1XXXX0X")]
		[TestCase(0, 64, "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX1XXXX0X")]
		public void TestMask(long input, long output, string mask) {
			Masker.ApplyMask(mask, input).ShouldBe(output);
		}

		[Test]
		[TestCase(0, "0", new long[] { 0 })]
		[TestCase(1, "0", new long[] { 1 })]
		[TestCase(1, "1", new long[] { 1 })]
		[TestCase(0, "1", new long[] { 1 })]
		[TestCase(0, "X", new long[] { 0, 1 })]
		[TestCase(1, "X", new long[] { 0, 1 })]

		[TestCase(0, "00", new long[] { 0 })]
		[TestCase(1, "00", new long[] { 1 })]
		[TestCase(2, "00", new long[] { 2 })]
		[TestCase(3, "00", new long[] { 3 })]

		[TestCase(3, "01", new long[] { 3 })]
		[TestCase(3, "10", new long[] { 3 })]

		[TestCase(0, "11", new long[] { 3 })]
		[TestCase(1, "11", new long[] { 3 })]
		[TestCase(2, "11", new long[] { 3 })]
		[TestCase(3, "11", new long[] { 3 })]

		[TestCase(3, "0X", new long[] { 2, 3 })]
		[TestCase(3, "1X", new long[] { 2, 3 })]
		[TestCase(3, "X0", new long[] { 1, 3 })]
		[TestCase(3, "X1", new long[] { 1, 3 })]
		[TestCase(3, "XX", new long[] { 0, 1, 2, 3 })]
		[TestCase(26, "00000000000000000000000000000000X0XX", new long[] { 16, 17, 18, 19,  24, 25, 26, 27 })]
		public void TestFuzzes(long input, string mask, long[] addresses) {
			var result = Masker.FuzzMask(mask, input).ToArray();
			result.OrderBy(x => x).ShouldBe(addresses.OrderBy(x => x));
		}

		[TestCase(42, "000000000000000000000000000000X1001X", 4)]
		[TestCase(26, "00000000000000000000000000000000X0XX", 8)]
		public void CountFuzzes(int address, string mask, int count) {
			var addresses = Masker.FuzzMask(mask, address);
			addresses.ToList().Count.ShouldBe(count);
		}

	}
}