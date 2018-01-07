using Ecli.Contracts;
using Ecli.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Ecli.Parsers {

	public class ExecutionPolicyParser : IParser {

		private readonly string _regexPattern;
		private IDictionary<string, bool> _allowedValues;

		public ExecutionPolicyParser() {
			_allowedValues = new Dictionary<string, bool>() {
				{ "a" , true },
				{ "all" , true },
				{ "n" , false },
				{ "new" , false }
			};
			_regexPattern = $@"{CommandToParse}\s-\b(\w+)\b";
		}

		public string CommandToParse => "execution-policy";
		public string Synopsis => @"--execution-policy [-|/][a|all] | [n|new](default)";
		public bool IsRequired => false;

		public IParserResult Parse(string contents) {
			if (!contents.Contains(CommandToParse)) return new ExecutionPolicyParserResult();

			Match match = Regex.Match(contents, _regexPattern);
			if (!match.Success) {
				return new ExecutionPolicyParserResult(
					false,
					new ArgumentException($"'{contents}' is not a valid argument string")
				);
			}

			string matchedPolicy = match.Groups[1].Value;
			bool matchFound = _allowedValues.ContainsKey(matchedPolicy);
			if (!matchFound) {
				return new ExecutionPolicyParserResult(
					false,
					new InvalidExecutionPolicyException($"'{matchedPolicy}' is not a valid execution policy.")
				);
			}

			bool executeAll = _allowedValues[matchedPolicy];
			return new ExecutionPolicyParserResult(executeAll, new EmptyException());
		}

	}
	
}
