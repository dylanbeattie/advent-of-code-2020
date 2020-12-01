using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day1 {
	class Program {
		static void Main(string[] args) {
            try {
			var ints = File.ReadAllLines("input.txt")
				.Select(s => Int32.Parse(s)).ToList();

                var howManyIntsToSum = 3;
                var requiredSum = 2020;

            var solution = Solve(ints, howManyIntsToSum, requiredSum);
            Console.WriteLine(solution);
            } catch(Exception ex) {
                Console.Error.WriteLine(ex.Message);
            }
		}

        // General-purpose recursive solution for finding the product of a set of integers
        // in a list that add up to a given sum.
        static int Solve(IEnumerable<int> ints, int howManyIntsToSum, int requiredSum) {
            if (howManyIntsToSum == 0 || ! ints.Any()) return 0;
            var head = ints.First();
            var tail = ints.Skip(1);
            if (howManyIntsToSum == 1 && head == requiredSum) return(head);
            var result = head * Solve(tail, howManyIntsToSum - 1, requiredSum - head);
            if (result != 0) return result;
            return Solve(tail, howManyIntsToSum, requiredSum);
        }

        // Naive solution using for() loops for part 2
		static int SolvePart2(List<int> ints) {
			for (int i = 0; i < ints.Count; i++) {
				for (int j = i + 1; j < ints.Count; j++) {
					for (int k = j + 1; k < ints.Count; k++) {
						if (ints[i] + ints[j] + ints[k] == 2020) {
							return ints[i] * ints[j] * ints[k];
						}
					}
				}
			}
			return 0;
		}

        // Naive solution using for() loops for part 1
		static int SolvePart1(List<int> ints) {
			for (int i = 0; i < ints.Count; i++) {
				for (int j = i + 1; j < ints.Count; j++) {
					if (ints[i] + ints[j] == 2020) {
						return ints[i] * ints[j];
					}
				}
			}
			return 0;
		}
	}
}
