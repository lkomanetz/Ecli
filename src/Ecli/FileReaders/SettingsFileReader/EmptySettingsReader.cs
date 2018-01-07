using System;

namespace Ecli.FileReaders.SettingsFileReaders {

	public class EmptySettingsReader : ISettingsReader {

		public ISettingsReaderResult Read(string cliCommandName, string settings) =>
			new EmptySettingsReaderResult(cliCommandName);

	}

}