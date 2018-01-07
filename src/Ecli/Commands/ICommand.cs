using Ecli.FileReaders.SettingsFileReaders;
using System;

namespace Ecli.Commands {

	public interface ICommand {

		string CliCommandName { get; }
		string HelpText { get; }
		ISettingsReader SettingsReader { get; }
		bool Execute(string commandArguments, ISettingsReaderResult settingsResult);

	}

}