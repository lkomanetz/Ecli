using Ecli.FileReaders.SettingsFileReaders;
using Ecli.Contracts;
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;

namespace Ecli.Commands {

	public class Help : ICommand {

		private DisplayManager _displayManager;
		private IFinder<ICommand> _commandFinder;
		private IHelpTextBuilder _helpTextBuilder;

		public Help() {
			_displayManager = new DisplayManager();
			_commandFinder = new CommandFinder($@"{Directory.GetCurrentDirectory()}\ecli.dll");
			_helpTextBuilder = new HelpCommandTextBuilder();
		}

		public string HelpText => _helpTextBuilder.Build();
		public ISettingsReader SettingsReader => new EmptySettingsReader();
		public string CliCommandName => "help";

		public bool Execute(string commandArguments = "", ISettingsReaderResult settingsResult = null) {
			try {
				ICommand[] commands = _commandFinder.FindAll();
				string helpText = String.Join("\n", commands.Select(x => x.HelpText + Environment.NewLine).ToArray());
				_displayManager.Show(new HelpPage(helpText));
				return true;
			}
			catch (Exception) {
				return false;
			}
		}

	}

}