using System;
using Ecli.FileReaders.SettingsFileReaders;
using Ecli.Exceptions;

namespace Ecli.FileReaders {

	public class DbUpdateSettingsReaderResult : ISettingsReaderResult {

		public DbUpdateSettingsReaderResult(DbUpgraderSettings settings, string cliCommandName) { 
			this.Settings = settings;
			this.CommandName = cliCommandName;
		}

		public DbUpdateSettingsReaderResult(Exception err, string cliCommandName) {
			this.Exception = err;
			this.CommandName = cliCommandName;
		}

		public DbUpgraderSettings Settings { get; private set; } = new DbUpgraderSettings();
		public Exception Exception { get; private set; } = new EmptyException();
		public string CommandName { get; private set; }

	}

}