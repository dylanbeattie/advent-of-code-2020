using System;
using System.Collections.Generic;
using System.Linq;

namespace Day8Code {
	class Computer {
		public List<Instruction> Instructions { get; set; }
		private Dictionary<int, int> Counters { get; set; } = new Dictionary<int, int>();
		public int Accumulator { get; set; }
		public int Pointer { get; set; }

		private Computer(List<Instruction> instructions) {
			this.Instructions = instructions;
		}

		public Computer(string listing) {
			var lines = listing
				.Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries).Select(line => line.Trim()).ToList();
			this.Instructions = lines.Select(Parse).ToList();
		}

		public Computer Mutate(int instruction) {
			var newInstructions = Instructions.Select(item => item).ToList();
			newInstructions[instruction] = newInstructions[instruction].Mutate();
			return new Computer(newInstructions);
		}

		public int Run() {
			while (true) {
				if (Pointer >= Instructions.Count) return 0;
				var instr = Instructions[Pointer];
				if (!Counters.ContainsKey(Pointer)) Counters.Add(Pointer, 0);
				if (Counters[Pointer] > 0) return 1;
				Counters[Pointer]++;
				switch (instr) {
					case Acc acc:
						Accumulator += acc.Arg;
						Pointer++;
						break;
					case Jmp jmp:
						Pointer += jmp.Arg;
						break;
					case Nop nop:
						Pointer++;
						break;
				}
			}
		}

		private Instruction Parse(string line) {
			var tokens = line.Split(" ");
			var arg = Int32.Parse(tokens[1]);
			return tokens[0] switch
			{
				"acc" => new Acc(arg),
				"jmp" => new Jmp(arg),
				"nop" => new Nop(arg),
				_ => throw new NotImplementedException($"We cannot parse instructions with code {tokens[0]}")
			};
		}
	}
}