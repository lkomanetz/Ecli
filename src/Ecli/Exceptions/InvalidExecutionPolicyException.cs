using System;
using System.Collections.Generic;
using System.Text;

namespace Ecli.Exceptions {

	public class InvalidExecutionPolicyException : Exception {

		public InvalidExecutionPolicyException() :
			base() { }

		public InvalidExecutionPolicyException(string msg) :
			base(msg) { }

		public InvalidExecutionPolicyException(string msg, Exception inner) :
			base(msg, inner) { }

	}

}
