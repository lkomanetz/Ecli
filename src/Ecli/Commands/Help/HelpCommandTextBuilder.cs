using Ecli.Contracts;
using System;

namespace Ecli.Commands {

	public class HelpCommandTextBuilder : IHelpTextBuilder {

		public string Build() => "help";

	}

}