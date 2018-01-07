using Ecli.Contracts;
using Ecli.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Ecli.Parsers.Tests {

	public class ExecutionPolicyParserTests {

		[Theory]
		[InlineData("all", true)]
		[InlineData("a", true)]
		[InlineData("n", false)]
		[InlineData("new", false)]
		public void ParsingExecutionPolicySucceeds(string policy, bool expectedValue) {
			var result = Parse(policy);
			Assert.True(result.ExecuteAllScripts == expectedValue, $"'{policy}' policy did not parse");
		}

		[Theory]
		[InlineData("al")]
		[InlineData("hello")]
		[InlineData("")]
		public void InvalidPolicyFailsToParse(string policy) {
			var result = Parse(policy);
			Assert.True(result.Exception.GetType() != typeof(EmptyException), $"'{policy}' policy parsed");
		}

		[Fact]
		public void NoExecutionPolicyProvidesDefault() {
			string args = "--settings -l test.xml";
			var result = (ExecutionPolicyParserResult)new ExecutionPolicyParser().Parse(args);
			Assert.True(result.ExecuteAllScripts == false, "Default value not provided when option missing");
		}

		private ExecutionPolicyParserResult Parse(string policy) {
			string args = $"--settings -l test.xml --execution-policy -{policy}";
			return (ExecutionPolicyParserResult)new ExecutionPolicyParser().Parse(args);
		}

	}

}
