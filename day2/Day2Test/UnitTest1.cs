using Day2Code;
using NUnit.Framework;

namespace Day2Test {
	public class Tests {
		[SetUp]
		public void Setup() {
		}

		[Test]
		[TestCase("1-1 a: a")]
		[TestCase("2-2 a: aa")]
		[TestCase("2-2 a: baab")]
		[TestCase("2-2 a: abba")]
		[TestCase("1-3 a: abcde")]
		[TestCase("2-9 c: ccccccccc")]
		public void Valid_Password_Is_Valid(string password) {
			var validator = new PasswordValidator();
			var result = validator.IsValid(password);
			Assert.IsTrue(result);
		}

		[Test]
		[TestCase("1-1 a: aa")]
		[TestCase("2-2 a: aaa")]
		[TestCase("2-2 a: bab")]
		[TestCase("2-2 a: abb")]
		[TestCase("1-3 b: cdefg")]
		public void Invalid_Password_Is_Not_Valid(string password) {
			var validator = new PasswordValidator();
			var result = validator.IsValid(password);
			Assert.IsFalse(result);
		}
	}
}