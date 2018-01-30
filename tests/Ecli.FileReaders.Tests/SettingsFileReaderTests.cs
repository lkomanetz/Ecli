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
			_reader = new SettingsFileReader(
				new IFinder<ICommand>[] { new CommandFinder($"{Directory.GetCurrentDirectory()}\\Ecli.FileReaders.Tests.dll") }
			);
			_thisAssembly = Assembly.GetAssembly(typeof(SettingsFileReaderTests));
		}

		[Fact]
		public void ReaderRetrievesDbUpgraderSettings() {
			int expectedResultCount = 1;
			string settings = GetResourceContentsFrom(_thisAssembly);
			var result = (SettingsFileReaderResult)_reader.Read(settings);

			Assert.True(result.Exception.GetType() == typeof(EmptyException), result.Exception.Message);
			Assert.True(
				result.Settings.Count() == expectedResultCount,
				$"Expected {expectedResultCount} results but was {result.Settings.Count()}."
			);

			bool noExceptionsThrown = result.Settings
				.All(s => s.Exception.GetType() == typeof(EmptyException));
			Assert.True(noExceptionsThrown, "A settings reader threw an exception.");
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