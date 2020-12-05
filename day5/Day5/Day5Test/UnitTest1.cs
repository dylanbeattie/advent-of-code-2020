using Day5Code;
using NUnit.Framework;
using Shouldly;

namespace Day5Test {
	public class Tests {
		//[SetUp]
		//public void Setup()
		//{
		//}

		[Test]
		[TestCase("BFFFBBFRRR", 70, 7, 567)]
		[TestCase("FFFBBBFRRR", 14, 7, 119)]
		[TestCase("BBFFBBFRLL", 102, 4, 820)]
		public void TestSeatParser(string boardingPassCode, int row, int column, int seatId) {

			var seat = new Seat(boardingPassCode);
			//seat.Row.ShouldBe(row);
			//seat.Column.ShouldBe(column);
			seat.SeatId.ShouldBe(seatId);
		}
	}
}