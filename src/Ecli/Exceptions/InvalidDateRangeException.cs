using System;
using System.Collections.Generic;
using System.Text;

namespace Ecli.Exceptions {

	public class InvalidDateRangeException : Exception {

		public InvalidDateRangeException() { }

		public InvalidDateRangeException(string msg) : 
			base(msg) { }

		public InvalidDateRangeException(string msg, Exception inner) :
			base(msg, inner) { }

	}

}
