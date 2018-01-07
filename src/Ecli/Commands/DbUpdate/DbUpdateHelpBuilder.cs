using Ecli.Contracts;
using System;
using System.Linq;
using System.Text;
using Ecli.Parsers;

namespace Ecli.Commands {

	public class DbUpdateHelpBuilder : IHelpTextBuilder {

		private IFinder<IParser> _parserFinder;

		public DbUpdateHelpBuilder(IFinder<IParser> parserFinder) => _parserFinder = parserFinder;

		public string Build() {
			StringBuilder parserBuilder = new StringBuilder();

			IParser[] parsers = _parserFinder.FindAll();
			parserBuilder.Append("dbupdate");
			foreach (IParser parser in parsers) {
				parserBuilder.AppendFormat(
					" [{0} {1}]",
					parser.Synopsis,
					(parser.IsRequired) ? "*REQUIRED*" : String.Empty
				);
			}

			return parserBuilder.ToString();
		}

	}

}