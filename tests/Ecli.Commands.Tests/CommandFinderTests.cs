using Ecli;
using Ecli.Contracts;
using System;
using System.Linq;
using Xunit;

namespace Ecli.Commands.Tests {

	public class CommandFinderTests {
		IFinder<ICommand> _finder;

		public CommandFinderTests() => _finder = new CommandFinder();

		[Fact]
		public void AllNecessaryCommandsFound() {
			int expectedCommandCount = 2;
			ICommand[] foundCommands = _finder.FindAll();
			Assert.True(foundCommands.Length == expectedCommandCount);
			bool typesFound = foundCommands.Any(x => x.GetType() == typeof(DbUpdate));
			Assert.True(typesFound, "Did not find all commands that implement ICommand");
		}

	}

}