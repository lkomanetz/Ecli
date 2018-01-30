using System;
using Ecli.Commands;
using Ecli.Exceptions;
using Ecli.FileReaders.SettingsFileReaders;

namespace Ecli.FileReaders.Tests {

	public class MockCommand : ICommand {

		public string CliCommandName => "MockCommand";
		public string HelpText => throw new NotImplementedException();
		public ISettingsReader SettingsReader => new MockSettingsReader();
		public bool Execute(string commandArguments, ISettingsReaderResult settingsResult) => true;

	}

	public class MockSettingsReader : ISettingsReader {

		public ISettingsReaderResult Read(string cliCommandName, string settings) =>
			new MockSettingsReaderResult(settings, cliCommandName);

	}

	public class MockSettingsReaderResult : ISettingsReaderResult {

		public MockSettingsReaderResult(string setting, string cliCommandName) {
			this.CommandName = cliCommandName;
			this.Setting = setting;
		}

		public string Setting { get; private set; }
		public string CommandName { get; private set; }
		public Exception Exception { get; private set;} = new EmptyException();
	}

}