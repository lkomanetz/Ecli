using System;
using System.Collections.Generic;
using System.Text;

namespace Ecli.Parsers {

	public interface IParser {

		string CommandToParse { get; }
		bool IsRequired { get; }
		string Synopsis { get; }
		IParserResult Parse(string contents);

	}

}
