using Ecli.Contracts;
using Ecli.FileReaders.SettingsFileReaders;
using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Xml;

namespace Ecli.FileReaders {

	public class DbUpdateSettingsReader : ISettingsReader {

		private DataContractJsonSerializer _serializer;

		public DbUpdateSettingsReader() =>
			_serializer = new DataContractJsonSerializer(typeof(DbUpgraderSettings));

		public ISettingsReaderResult Read(string cliCommandName, string contents) {
			try {
				byte[] settingsBytes = Encoding.Unicode.GetBytes(contents);
				using (var ms = new MemoryStream(settingsBytes)) {
					var settings = (DbUpgraderSettings)_serializer.ReadObject(ms);
					settings.RawContents = contents;
					return new DbUpdateSettingsReaderResult(settings, cliCommandName);
				}
			}
			catch (Exception err) {
				return new DbUpdateSettingsReaderResult(err, cliCommandName);
			}
		}

	}

}