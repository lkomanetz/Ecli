using Ecli.Exceptions;
using System;
using Ecli.Contracts;
using System.Collections.Generic;
using System.Text;

namespace Ecli.Parsers {

	public class ArgumentParserResult : IParserResult {

		public ArgumentParserResult(IEnumerable<IParserResult> allParseResults) {
			foreach (IParserResult result in allParseResults) {
				switch (result) {
					case DateRangeParserResult dateRangeResult:
						this.DateRangeResult = dateRangeResult;
						break;
					case ExecutionPolicyParserResult executionPolicyResult:
						this.ExecutionPolicyResult = executionPolicyResult;
						break;
				}
			}
		}

		public ArgumentParserResult(IEnumerable<IParserResult> allParseResults, Exception exception) :
			this(allParseResults) => this.Exception = exception;

		public DateRangeParserResult DateRangeResult { get; private set; } = new DateRangeParserResult();
		public ExecutionPolicyParserResult ExecutionPolicyResult { get; private set; } = new ExecutionPolicyParserResult();
		public bool Success => this.Exception.GetType() == typeof(EmptyException);
		public Exception Exception { get; private set; } = new EmptyException();

	}

}
