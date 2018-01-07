using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Ecli.Commands;
using Ecli.FileReaders.SettingsFileReaders;
using Ecli.Exceptions;
using Xunit;

namespace Ecli.FileReaders.Tests {

	public class SettingsFileReaderTests {

		private IFileReader _reader;
		private Assembly _thisAssembly;

		public SettingsFileReaderTests() {
			_reader = new SettingsFileReader(new CommandFinder());
			_thisAssembly = Assembly.GetAssembly(typeof(SettingsFileReaderTests));
		}

		[Fact]
		public void ReaderRetrievesDbUpgraderSettings() {
			string settings = GetResourceContentsFrom(_thisAssembly);
			var result = (SettingsFileReaderResult)_reader.Read(settings);

			Assert.True(result.Exception.GetType() == typeof(EmptyException), result.Exception.Message);

			foreach (ISettingsReaderResult settingResult in result.Settings) {
				switch (settingResult) {
					case DbUpdateSettingsReaderResult x:
						Assert.True(x.Exception.GetType() == typeof(EmptyException));
						break;
					case EmptySettingsReaderResult x:
						Assert.True(x.Exception.GetType() == typeof(EmptyException));
						Assert.True(
							x.CommandName.Equals("help"),
							$"Expected help command but was '{x.CommandName}'"
						);
						break;
					default: throw new Exception("No valid settings results where found");
				}
			}
		}

		private string GetResourceContentsFrom(Assembly assembly) {
			string resourceName = assembly.GetManifestResourceNames()
				.Where(n => n.Contains("appsettings"))
				.Single();

			using (Stream manifestStream = assembly.GetManifestResourceStream(resourceName))
			using (StreamReader sr = new StreamReader(manifestStream)) {
				return sr.ReadToEnd();
			}

		}

	}
	
}