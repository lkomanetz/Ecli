using System;

namespace Ecli.Parsers.Tests {

	public class MockParser : IParser {

		public bool IsRequired => false;
		public string CommandToParse => throw new NotImplementedException();
		public string Synopsis => throw new NotImplementedException();

		public IParserResult Parse(string contents) =>
			throw new NotImplementedException();
		
	}

}