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
			int expectedFoundParserCount = 2;

			IParser[] foundParsers = _parserFinder.FindAll();
			Assert.True(foundParsers.Length == expectedFoundParserCount);

			var nonArgumentParsers = foundParsers.Where(p => p.GetType() != typeof(ArgumentParser));
			Assert.True(nonArgumentParsers.Count() == expectedFoundParserCount);

			bool argumentParserFound = foundParsers.Any(p => p.GetType() == typeof(ArgumentParser));
			Assert.False(argumentParserFound);
		}

	}

}