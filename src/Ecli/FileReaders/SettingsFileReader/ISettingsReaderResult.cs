using System;

namespace Ecli.FileReaders.SettingsFileReaders {

	public interface ISettingsReaderResult {

		string CommandName { get; }
		Exception Exception { get; }

	}

}