using System;
using System.Linq;
using Xunit;
using Ecli.Exceptions;
using Ecli.FileReaders.SettingsFileReaders;

namespace Ecli.FileReaders.Tests {

    public class DbUpdateSettingsFileReaderTests {

        private const string CLI_COMMAND_NAME = "dbupdate";
        private ISettingsReader _fileReader;

        public DbUpdateSettingsFileReaderTests() => _fileReader = new DbUpdateSettingsReader();

        [Fact]
        public void SettingsFileReaderGetsCorrectSettings() {
            var result = (DbUpdateSettingsReaderResult)_fileReader.Read(CLI_COMMAND_NAME, BuildXml());
            Assert.True(result.Exception.GetType() == typeof(EmptyException));
            Assert.True(result.Settings.GetType() == typeof(DbUpgraderSettings));
        }

        private string BuildXml() =>
            "{ \"DbUpgraders\": [ { \"LogLocation\": \"C:\\\\DbUpgrader\\\\Logs\", \"ScriptsLocation\": \"C:\\\\DbUpgrader\\\\Scripts\", \"SqlConnectionString\": \"my connection string\"} ] }";

    }

}
