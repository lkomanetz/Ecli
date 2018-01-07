using Ecli.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecli.Parsers {

	public class ExecutionPolicyParserResult : IParserResult {

		public ExecutionPolicyParserResult() {
			this.Exception = new EmptyException();
			this.Success = false;
			this.ExecuteAllScripts = false;
		}

		public ExecutionPolicyParserResult(
			bool executeAllScripts,
			Exception exception
		) {
			this.Exception = exception ?? new EmptyException();
			this.ExecuteAllScripts = executeAllScripts;
			this.Success = (this.Exception.GetType() == typeof(EmptyException));
		}

		public bool Success { get; private set; }
		public bool ExecuteAllScripts { get; private set; }
		public Exception Exception { get; private set; }

	}

}
