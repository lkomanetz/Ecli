using Ecli.Contracts;
using Ecli.Commands;
using Ecli.FileReaders;
using Ecli.FileReaders.SettingsFileReaders;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ecli {

	public class EntryPoint {

		public static void Main(string[] args) {
			try {
				IFinder<ICommand> cmdFinder = new CommandFinder("bin");
				IFileReader fileReader = new SettingsFileReader(cmdFinder);
				Program p = new Program(cmdFinder, args, fileReader);
				p.Run(args);
			}
			catch (Exception err) {
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine($"{err.GetType().Name} -> {err.Message}");
			}
			finally {
				Console.ForegroundColor = ConsoleColor.White;
			}
			Environment.Exit(0);
		}

	}

}