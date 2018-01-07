using System;
using System.Collections.Generic;
using System.Text;
using Ecli.Contracts;
using Ecli.Exceptions;

namespace Ecli.Parsers {

	public class DateRangeParserResult : IParserResult {

		public DateRangeParserResult() {}

		public DateRangeParserResult(Exception exception) => this.Exception = exception;
		public DateRangeParserResult(DateRange dateRange) => this.DateRange = dateRange;

		public DateRange DateRange { get; set; } = new DateRange(DateTime.MinValue, DateTime.MaxValue);
		public bool Success => this.Exception.GetType() == typeof(EmptyException);
		public Exception Exception { get; private set; } = new EmptyException();

	}

}
