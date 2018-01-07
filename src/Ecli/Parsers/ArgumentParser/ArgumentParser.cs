using System;
using System.Collections.Generic;
using System.Linq;
using Ecli.Contracts;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;

namespace Ecli.Parsers {

	public class ArgumentParser : IParser {

		private IList<IParser> _parsers;

		public ArgumentParser() => _parsers = LoadParsersFromAssembly();

		public string CommandToParse => String.Empty;
		public string Synopsis => String.Empty;
		public bool IsRequired => true;

		public IParserResult Parse(string argumentContent) {
			string[] arguments = argumentContent.Split(new string[] { "--" }, StringSplitOptions.RemoveEmptyEntries);
			IList<IParserResult> results = new List<IParserResult>();

			foreach (IParser parser in _parsers) results.Add(parser.Parse(argumentContent));
			return new ArgumentParserResult(results);
		}

		private IList<IParser> LoadParsersFromAssembly() {
			var parserTypes = AppDomain.CurrentDomain.GetAssemblies()
				.SelectMany(x => x.DefinedTypes)
				.Where(t =>
					t.GetInterfaces().Any(i => i.Name.Equals(typeof(IParser).Name)) &&
					t.AsType().Name != typeof(ArgumentParser).Name
				);

			return parserTypes
				.Select(t => Activator.CreateInstance(t.AsType()) as IParser)
				.ToList();
		}

	}

}