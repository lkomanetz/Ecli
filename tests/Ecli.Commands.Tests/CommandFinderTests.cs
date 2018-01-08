using Ecli;
using Ecli.Contracts;
using System;
using System.IO;
using System.Linq;
using Xunit;

namespace Ecli.Commands.Tests {

	public class CommandFinderTests {
		IFinder<ICommand> _finder;

		public CommandFinderTests() {
			string dllFile = $@"{Directory.GetCurrentDirectory()}\Ecli.Commands.Tests.dll";
			_finder = new CommandFinder(dllFile);
		}

		[Fact]
		public void AllNecessaryCommandsFound() {
			int expectedCommandCount = 2;
			ICommand[] foundCommands = _finder.FindAll();
			Assert.True(foundCommands.Length == expectedCommandCount);
			bool typesFound = foundCommands.Any(x =>
				x.GetType() == typeof(FirstTestCommand) ||
				x.GetType() == typeof(SecondTestCommand)
			);
			Assert.True(typesFound, "Did not find all commands that implement ICommand");
		}

	}

}