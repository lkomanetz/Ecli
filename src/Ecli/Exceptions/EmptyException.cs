using System;
using System.Collections.Generic;
using System.Text;

namespace Ecli.Exceptions {

	public class EmptyException : Exception {

		public EmptyException() :
			base() { }
	}

}
