//using System;

//namespace Day16Code {
//	public class Check {
//		private int min;
//		private int max;

//		public static Check Parse(string range) {
//			var values = range.Split("-");
//			return new Check {
//				min = Int32.Parse(values[0]),
//				max = Int32.Parse(values[1])
//			};
//		}
//		public bool IsValid(int value) => (this.min <= value && this.max >= value);
//	}
//}