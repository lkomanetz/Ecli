using Ecli;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Xunit;

namespace Ecli.Commands.Tests {

	public class CommandFinderTests {

		public CommandFinderTests() { }

		[Theory]
		[InlineData(2, "Ecli.Commands.Tests.dll")]
		public void AllNecessaryCommandsFound(int expectedLength, params string[] dllNames) {
			IList<string> assemblyNames = new List<string>();
			foreach (string dllName in dllNames) {
				string dllFile = $@"{Directory.GetCurrentDirectory()}\{dllName}";
				assemblyNames.Add(dllFile);
			}
			IFinder<ICommand> finder = new CommandFinder(assemblyNames.ToArray());
			ICommand[] foundCommands = finder.FindAll();
			Assert.True(foundCommands.Length == expectedLength);
			bool typesFound = foundCommands.Any(x =>
				x.GetType() == typeof(FirstTestCommand) ||
				x.GetType() == typeof(SecondTestCommand)
			);
			Assert.True(typesFound, "Did not find all commands that implement ICommand");
		}

		[Theory]
		[InlineData("Ecli.Commands.Tests.dll")]
		public void CommandFinderCanFindSingleCommand(string assemblyName) {
			string dllFile = $@"{Directory.GetCurrentDirectory()}\{assemblyName}";
			IFinder<ICommand> cmdFinder = new CommandFinder(dllFile);
			ICommand cmd = cmdFinder.Find<FirstTestCommand>();
			Assert.True(cmd != null, $"Did not find '{typeof(FirstTestCommand).Name}'");
			Assert.True(cmd.GetType() == typeof(FirstTestCommand));
		}

		[Fact]
		public void CommandFinderCanFindFromAssembly() {
			Assembly assembly = this.GetType().Assembly;
			IFinder<ICommand> cmdFinder = new CommandFinder(assembly);
			ICommand cmd = cmdFinder.Find<FirstTestCommand>();
			Assert.True(cmd != null && cmd.GetType() == typeof(FirstTestCommand));
		}

	}

}