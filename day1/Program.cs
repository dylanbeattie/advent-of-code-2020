using System;
using System.IO;
using System.Linq;

namespace day1 {
	class Program {
		static void Main(string[] args) {
			var ints = File.ReadAllLines("input.txt")
            .Select(s => Int32.Parse(s)).ToList();
            for (int i = 0; i < ints.Count; i++) {
                var first = ints[i];
                for(int j = i+1; j < ints.Count; j++) {
                    var second = ints[j];
                    if (ints[i] + ints[j] == 2020) {
                        Console.WriteLine(ints[i] * ints[j]);
                        return;
                    }
                }
            }
		}
	}
}
