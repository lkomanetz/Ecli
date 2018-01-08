using Ecli.Commands;
using System;
using Ecli.FileReaders.SettingsFileReaders;

namespace Ecli.Commands.Tests {

	/*
	 * This file is just a single place to store all of the classes that implement ICommand.
	 */

	public class FirstTestCommand : ICommand {

		public string CliCommandName => String.Empty;
		public string HelpText => String.Empty;
		public ISettingsReader SettingsReader => throw new NotImplementedException();

		public bool Execute(string commandArguments, ISettingsReaderResult settingsResult) => true;

	}

	public class SecondTestCommand : ICommand {

		public string CliCommandName => String.Empty;
		public string HelpText => String.Empty;
		public ISettingsReader SettingsReader => throw new NotImplementedException();

		public bool Execute(string args, ISettingsReaderResult settingsResult) => true;

	}

}