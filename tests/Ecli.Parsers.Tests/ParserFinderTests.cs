using Ecli;
using System;
using System.Linq;
using Xunit;

namespace Ecli.Parsers.Tests {

	public class ParserFinderTests {

		private IFinder<IParser> _parserFinder;

		public ParserFinderTests() => _parserFinder = new ParserFinder();

		[Fact]
		public void ParserFinderFindsAllNecessaryParsers() {
			int expectedFoundParserCount = 1;
			IParser[] foundParsers = _parserFinder.FindAll();
			Assert.True(
				foundParsers.Length == expectedFoundParserCount,
				$"Expected {expectedFoundParserCount} but was {foundParsers.Length}."
			);

			IParser mockParser = foundParsers
				.Where(p => p is MockParser)
				.SingleOrDefault();
			
			Assert.NotNull(mockParser);
			Assert.IsType(typeof(MockParser), mockParser);
		}

	}

}