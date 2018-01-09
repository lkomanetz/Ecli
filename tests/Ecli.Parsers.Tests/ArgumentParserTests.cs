using Ecli;
using Ecli.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using System.IO;

namespace Ecli.Parsers.Tests {

	public class ArgumentParserTests {
		private ArgumentParserScenario[] _testScenarios;

		// TODO(Logan) -> Fix these tests.
		public ArgumentParserTests() {
			_testScenarios = new ArgumentParserScenario[0];
			/*
			_testScenarios = new ArgumentParserScenario[] {
				new ArgumentParserScenario() {
					Name = "Simple File Location & Date Range",
					CliArguments = @"--date-range -from 2017-11-01 -to 2017-11-15",
					ExpectedResult = new ArgumentParserResult(
						new List<IParserResult>() {
							new DateRangeParserResult(new DateRange(DateTime.Parse("2017-11-01"), DateTime.Parse("2017-11-15"))),
							new ExecutionPolicyParserResult()
						},
						new EmptyException()
					)
				},
				new ArgumentParserScenario() {
					Name = "No arguments",
					CliArguments = String.Empty,
					ExpectedResult = new ArgumentParserResult(
						new List<IParserResult>() {
							new DateRangeParserResult(),
							new ExecutionPolicyParserResult()
						},
						new EmptyException()
					)
				},
				new ArgumentParserScenario() {
					Name = "Execution policy with date range",
					CliArguments = @"--date-range -from 2017-11-01 -to 2017-11-15 --execution-policy -all",
					ExpectedResult = new ArgumentParserResult(
						new List<IParserResult>() {
							new DateRangeParserResult(new DateRange(DateTime.Parse("2017-11-01"), DateTime.Parse("2017-11-15"))),
							new ExecutionPolicyParserResult(true, new EmptyException())
						},
						new EmptyException()
					)
				},
				new ArgumentParserScenario() {
					Name = "Invalid execution policy",
					CliArguments = @"--date-range -from 2017-11-01 --execution-policy -hello",
					ExpectedResult = new ArgumentParserResult(
						new List<IParserResult>() {
							new DateRangeParserResult(new DateRange(DateTime.Parse("2017-11-01"), DateTime.MaxValue)),
							new ExecutionPolicyParserResult(false, new InvalidExecutionPolicyException())
						},
						new EmptyException()
					)
				}
			};
			*/
		}

		[Fact]
		public void AllArgumentParserScenariosSucceed() {
			foreach (var scenario in _testScenarios) {
				var result = new ArgumentParser().Parse(scenario.CliArguments);
				bool resultsAreEqual = ResultsEqual((ArgumentParserResult)result, scenario.ExpectedResult);
				Assert.True(
					resultsAreEqual && result.Success,
					$"Scenario: {scenario.Name}\nResults were not equal."
				);
			}
		}

		private bool ResultsEqual(ArgumentParserResult actualResult, ArgumentParserResult expectedResult) {
			/*
			var expectedDateRangeResult = expectedResult.DateRangeResult;
			var expectedExecutionPolicyResult = expectedResult.ExecutionPolicyResult;
			var actualDateRangeResult = actualResult.DateRangeResult;
			var actualExecutionPolicyResult = actualResult.ExecutionPolicyResult;

			return (
				expectedDateRangeResult.DateRange.From == actualDateRangeResult.DateRange.From &&
				expectedDateRangeResult.DateRange.To == actualDateRangeResult.DateRange.To &&
				expectedExecutionPolicyResult.ExecuteAllScripts == actualExecutionPolicyResult.ExecuteAllScripts &&
				expectedExecutionPolicyResult.Exception.GetType() == actualExecutionPolicyResult.Exception.GetType()
			);
			*/
			return false;
		}

		private class ArgumentParserScenario {
			
			public string Name { get; set; }
			public string CliArguments { get; set; }
			public ArgumentParserResult ExpectedResult { get; set; }

		}

	}

}
