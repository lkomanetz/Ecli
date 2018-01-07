using Ecli.Exceptions;
using System;

namespace Ecli.FileReaders.SettingsFileReaders {

	public class EmptySettingsReaderResult : ISettingsReaderResult {

		public EmptySettingsReaderResult(string cliCommandName) => this.CommandName = cliCommandName;

		public Exception Exception { get; } = new EmptyException();
		public string CommandName { get; private set; }

	}

}