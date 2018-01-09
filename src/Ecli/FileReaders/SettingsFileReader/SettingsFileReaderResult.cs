using Ecli.Exceptions;
using System;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Ecli.FileReaders.SettingsFileReaders {

	public class SettingsFileReaderResult : FileReaderResult {

		public SettingsFileReaderResult(Exception exception) :
			base(exception) { }

		public SettingsFileReaderResult(IEnumerable<ISettingsReaderResult> settings, JObject settingsJson) :
			base(new SettingsFileContents(settingsJson)) => this.Settings = settings;
		
		public IEnumerable<ISettingsReaderResult> Settings { get; private set; }

	}

}