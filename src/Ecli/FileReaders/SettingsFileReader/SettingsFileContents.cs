using Newtonsoft.Json.Linq;
using Ecli.FileReaders;
using System;

namespace Ecli.FileReaders.SettingsFileReaders {

	public class SettingsFileContents : IFileReaderContents {

		public SettingsFileContents(JObject settingsJson) => this.RawContents = settingsJson.ToString();

		public string RawContents { get; private set; }

	}

}