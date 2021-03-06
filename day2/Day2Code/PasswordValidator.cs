using System;
using System.ComponentModel;
using System.Linq;

namespace Day2Code {
	public abstract class Policy {
		public int Digit1 { get; set; }
		public int Digit2 { get; set; }
		public char Letter { get; set; }
		public string Password { get; set; }

		protected Policy(string input) {
			var pair = input.Split(":").Select(s => s.Trim()).ToArray();
			var policy = pair[0];
			var bits = policy.Split(" ");
			var digits = bits[0].Split("-").Select(Int32.Parse).ToArray();

			this.Password = pair[1];

			this.Digit1 = digits[0];
			this.Digit2 = digits[1];
			this.Letter = bits[1][0];
		}

		public abstract bool IsValid();
	}

	public class Part1Policy : Policy {

		public Part1Policy(string input) : base(input) { }

		public override bool IsValid() {
			var count = this.Password.Count(c => c == this.Letter);
			if (count < this.Digit1) return (false);
			if (count > this.Digit2) return (false);
			return (true);
		}
	}

	public class Part2Policy : Policy {
		public Part2Policy(string input) : base(input) { }

		public override bool IsValid() {
			var firstLetter = this.Password[this.Digit1 - 1];
			var secondLetter = this.Password[this.Digit2 - 1];
			return (
				firstLetter == this.Letter
				^
				secondLetter == this.Letter
			);
		}
	}

	public class PasswordValidator {

		public bool IsValid(string input) {
			var p = new Part2Policy(input);
			return p.IsValid();
		}
	}
}