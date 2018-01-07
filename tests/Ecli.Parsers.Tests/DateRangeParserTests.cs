using System;
using Ecli.Parsers;
using Xunit;
using Ecli.Contracts;
using Ecli.Exceptions;

namespace Ecli.Parsers.Tests {

	public class DateRangeParserTests {

		[Theory]
		[InlineData("2017-11-01", "2017-11-15")]
		[InlineData("2017-10-15", "2017-11-20")]
		[InlineData("2017-11-20", "2017-11-20")]
		[InlineData("2017-11-20", "")]
		[InlineData("", "2017-11-01")]
		public void ParsingDateRangeSucceeds(string fromStr, string toStr) {
			string args = BuildArgumentString($"--settings -l --date-range", fromStr, toStr);
			IParserResult result = Parse(args);
			AssertDateRange(result, fromStr, toStr);
		}

		[Theory]
		[InlineData("2017-11-20", "hello")]
		[InlineData("hello", "2017-11-20")]
		[InlineData("hello", "exception")]
		[InlineData("", "")]
		public void ParseInvalidArgumentsThrowsException(string fromStr, string toStr) {
			Assert.Throws<ArgumentException>(() => {
				string args = BuildArgumentString($"--settings -l --date-range", fromStr, toStr);
				Parse(args);
			});
		}

		[Theory]
		[InlineData("2017-11-20", "2017-11-01")]
		[InlineData("2017-11-21", "2017-11-20")]
		public void ParseInvalidDateRangeFails(string fromStr, string toStr) {
			string args = BuildArgumentString($"--settings -l --date-range", fromStr, toStr);
			IParserResult result = Parse(args);
			Assert.True(!result.Success, "Parse succeeded when it should have failed");
		}

		private IParserResult Parse(string args) {
			IParser parser = new DateRangeParser();
			return parser.Parse(args);
		}

		private string BuildArgumentString(string argumentString, string fromStr, string toStr) {
			string args = argumentString;
			args += (fromStr != String.Empty) ? $" -from {fromStr}" : fromStr;
			args += (toStr != String.Empty) ? $" -to {toStr}" : toStr;

			return args;
		}

		private void AssertDateRange(IParserResult result, string expectedFrom, string expectedTo) {
			if (!DateTime.TryParse(expectedFrom, out DateTime expectedFromDt)) {
				expectedFromDt = DateTime.MinValue;
			}
			if (!DateTime.TryParse(expectedTo, out DateTime expectedToDt)) {
				expectedToDt = DateTime.MaxValue;
			}
			DateRangeParserResult parserResult = (DateRangeParserResult)result;
			DateRange range = parserResult.DateRange;
			string errorMsg = $"Was:{range.From.ToString("yyyy-MM-dd")} To:{range.To.ToString("yyyy-MM-dd")}\n";
			errorMsg += $"Expected From:{expectedFrom} To:{expectedTo}";

			Assert.True(
				parserResult.DateRange.From == expectedFromDt && parserResult.DateRange.To == expectedToDt,
				errorMsg
			);

		}
	}

}
