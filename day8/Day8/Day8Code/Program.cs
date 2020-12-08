using System;
using System.Diagnostics;
using System.IO;

namespace Day8Code {
	class Program {
		private const string listing = @"nop +0
acc +1
jmp +4
acc +3
jmp -3
acc -99
acc +1
jmp -4
acc +6";
		static void Main(string[] args) {
			var prog = new Computer(File.ReadAllText("input.txt"));
			for (int victim = 0; victim < prog.Instructions.Count; victim++) {
				var mutation = prog.Mutate(victim);
				var mutatedResult = mutation.Run();
				if (mutatedResult == 0) {
					Console.WriteLine($"YOLO! when mutating instruction {victim}");
					Console.WriteLine(mutation.Accumulator);
				}
			}
		}
	}

	abstract class Instruction {
		protected Instruction(int arg) {
			Arg = arg;
		}
		public int Arg { get; set; }

		public virtual Instruction Mutate() {
			return this;
		}
	}

	class Acc : Instruction {
		public Acc(int arg) : base(arg) { }
	}

	class Jmp : Instruction {
		public Jmp(int arg) : base(arg) { }
		public override Instruction Mutate() {
			return new Nop(Arg);
		}
	}

	class Nop : Instruction {
		public Nop(int arg) : base(arg) { }

		public override Instruction Mutate() {
			return new Jmp(Arg);
		}

	}
}
