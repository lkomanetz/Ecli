using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Ecli.Contracts;
using Ecli.Exceptions;

namespace Ecli.Parsers {

	public class DateRangeParser : IParser {

		private const string DATE_REGEX_PATTERN = @"(-from|-to)\s([\w|\/|-]+)";
		private readonly string _regexPattern;

		public DateRangeParser() {
			_regexPattern = $@"{CommandToParse}\s(-(from|to)\s[\w|\/|-]+\s*)*";
		}

		public string CommandToParse => "date-range";
		public string Synopsis => @"--date-range [-|/]from <DATE> [[-|/]to <DATE>]";
		public bool IsRequired => false;

		public IParserResult Parse(string dateRangeArguments) {
			if (!dateRangeArguments.Contains(CommandToParse)) return new DateRangeParserResult();

			Match match = Regex.Match(dateRangeArguments, _regexPattern);
			if (!match.Success) throw new ArgumentException($"'{dateRangeArguments}' is not a valid argument string");
			(DateTime startingFrom, DateTime goingTo) = GetDates(match.Value);

			if (goingTo < startingFrom) return new DateRangeParserResult(new InvalidDateRangeException());
			return new DateRangeParserResult(new DateRange(startingFrom, goingTo));
		}

		private void ThrowIfInvalid(string date) {
			DateTime result = DateTime.MinValue;
			if (!DateTime.TryParse(date, out result)) {
				if (!String.IsNullOrEmpty(date)) {
					throw new ArgumentException($"'{date}' is not a valid date format.");
				}
			}
		}

		private (DateTime From, DateTime To) GetDates(string dateArguments) {
			DateTime fromDate = DateTime.MinValue;
			DateTime toDate = DateTime.MaxValue;

			MatchCollection matches = Regex.Matches(dateArguments, DATE_REGEX_PATTERN);
			foreach (Match dateMatch in matches) {
				ThrowIfInvalid(dateMatch.Groups[2].Value);

				DateTime parsedDt;
				DateTime.TryParse(dateMatch.Groups[2].Value, out parsedDt);

				if (dateMatch.Groups[1].Value.Contains("from")) fromDate = parsedDt;
				else if (dateMatch.Groups[1].Value.Contains("to")) toDate = parsedDt;
			}
			return (fromDate, toDate);
		}

	}

}