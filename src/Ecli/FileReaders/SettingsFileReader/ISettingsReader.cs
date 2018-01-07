using System;

namespace Ecli.FileReaders.SettingsFileReaders {

	public interface ISettingsReader {

		ISettingsReaderResult Read(string cliCommandName, string settings);

	}

}