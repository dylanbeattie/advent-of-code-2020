using System;
using System.IO;
using System.Linq;

namespace Day2Code {
	class Program {
		static void Main(string[] args) {
			var validator = new PasswordValidator();
			var count = File.ReadAllLines("input.txt").Where(validator.IsValid).Count();
			Console.WriteLine(count);
		}
	}
}
