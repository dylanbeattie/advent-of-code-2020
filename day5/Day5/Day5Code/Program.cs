using System;
using System.IO;
using System.Linq;
using System.Runtime.ExceptionServices;

namespace Day5Code {
	class Program {
		static void Main(string[] args) {
			var boardingPassCodes = File.ReadAllLines("input.txt");
			var seatIds = boardingPassCodes.Select(code => new Seat(code).SeatId).ToList();
			Console.WriteLine(Enumerable.Range(0, 1023).Single(
				id => seatIds.Contains(id + 1)
				      && seatIds.Contains(id - 1)
				      && !seatIds.Contains(id)));
		}
	}

	public class Seat {
		public Seat(string boardingPassCode) {
			var digits = String.Join(String.Empty, boardingPassCode
				.Select(LetterToBinaryDigit)
				.ToArray()
			);
			this.SeatId = Convert.ToInt32(digits, 2);
		}

		public static char LetterToBinaryDigit(char c) {
			switch (c) {
				case 'F':
				case 'L':
					return '0';
				case 'B':
				case 'R':
					return '1';
				default:
					throw (new Exception("Unexpected character in boarding pass!"));
			}
		}

		//public int Row { get; private set; } = 0;
		//public int Column { get; private set; } = 0;
		public int SeatId { get; private set; } = 0;
	}

	public static class Extensions {

	}
}
