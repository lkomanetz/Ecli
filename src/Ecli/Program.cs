using Executioner.Contracts;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Ecli.Commands;
using Ecli.Contracts;
using Ecli.FileReaders;
using Ecli.Parsers;
using Ecli.Upgraders;
using Ecli.FileReaders.SettingsFileReaders;
using System.Reflection;

namespace Ecli {

	public class Program {

		private IDictionary<string, ICommand> _availableCommands;
		private DisplayManager _displayManager;
		private IFinder<ICommand> _commandFinder;
		private IFileReader _fileReader;

		public Program(IFinder<ICommand> cmdFinder, string[] appArguments, IFileReader fileReader) {
			_displayManager = new DisplayManager();
			_commandFinder = cmdFinder;	
			_fileReader = fileReader;
			_availableCommands = cmdFinder.FindAll().ToDictionary(cmd => cmd.CliCommandName);
			AddHelpCommand();
		}

		public string ExecutingDirectoryLocation =>
			new FileInfo(Assembly.GetExecutingAssembly().Location).DirectoryName;

		public void Run(string[] args) {
			if (args.Length == 0) throw new Exception("No arguments passed in");
			string cmdStr = args[0];
			string appArgs = String.Join(" ", args.Skip(1));
			_availableCommands.TryGetValue(cmdStr, out ICommand cmd);
			if (cmd == null) throw new Exception($"Unable to find command '{cmdStr}'");
			ISettingsReaderResult settingsResult = FindSettingsFor(cmd);
			bool success = Execute(cmd, appArgs, settingsResult);
			if (!success) throw new Exception($"Unable to execute command '{cmdStr}'");
		}

		private bool Execute(ICommand cmd, string arguments, ISettingsReaderResult cmdSettings) {
			Console.WriteLine($"\nExecuting '{cmd.GetType().Name}'...\n");
			return cmd.Execute(arguments, cmdSettings);
		} 

		private ISettingsReaderResult FindSettingsFor(ICommand cmd) {
			string settings = File.ReadAllText($"{ExecutingDirectoryLocation}\\appsettings.json");
			var result = (SettingsFileReaderResult)_fileReader.Read(settings);

			return result.Settings
				.Where(s => s.CommandName == cmd.CliCommandName)
				.Single();
		}

		private void AddHelpCommand() {
			ICommand cmd = new Help(_commandFinder);
			if (!_availableCommands.ContainsKey(cmd.CliCommandName)) {
				_availableCommands.Add(new KeyValuePair<string, ICommand>(cmd.CliCommandName, cmd));
			} 
			else {
				_availableCommands[cmd.CliCommandName] = cmd;
			}
		}

	}

}