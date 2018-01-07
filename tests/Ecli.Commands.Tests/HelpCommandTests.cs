using Ecli;
using Ecli.Commands;
using Ecli.Contracts;
using System;
using Xunit;

namespace Ecli.Commands.Tests {

    public class HelpCommandTests {

        [Fact]
        public void HelpCommandExecutesSuccessfully() {
            ICommand cmd = new Help();
            bool succeeded = cmd.Execute(String.Empty, null);
            Assert.True(succeeded);
        }

        private class MockFinder : IFinder<ICommand> {

            public ICommand[] FindAll() => new ICommand[0];

        }

    }

}