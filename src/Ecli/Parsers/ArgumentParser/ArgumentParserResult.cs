using Ecli.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecli.Parsers {

	public class ArgumentParserResult : IParserResult {

		public ArgumentParserResult(IEnumerable<IParserResult> allParseResults) =>
			this.Results = allParseResults;

		public ArgumentParserResult(IEnumerable<IParserResult> allParseResults, Exception exception) :
			this(allParseResults) => this.Exception = exception;

		public IEnumerable<IParserResult> Results { get; }
		public bool Success => this.Exception.GetType() == typeof(EmptyException);
		public Exception Exception { get; private set; } = new EmptyException();

	}

}
