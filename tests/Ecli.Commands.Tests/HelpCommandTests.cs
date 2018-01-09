using Ecli;
using Ecli.Commands;
using Ecli.Contracts;
using System;
using System.IO;
using System.Linq;
using Xunit;

namespace Ecli.Commands.Tests {

    public class HelpCommandTests {

        [Fact]
        public void HelpCommandExecutesSuccessfully() {
            string dllPath = $@"{Directory.GetCurrentDirectory()}\ecli.dll";
            IFinder<ICommand> cmdFinder = new CommandFinder(dllPath);
            ICommand cmd = new Help(cmdFinder);
            bool succeeded = cmd.Execute(String.Empty, null);
            Assert.True(succeeded);
        }

    }

}