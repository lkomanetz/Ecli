using Ecli.Contracts;
using Ecli.Commands;
using Ecli.FileReaders;
using Ecli.FileReaders.SettingsFileReaders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Ecli {

	public class EntryPoint {

		private const string DLL_DIR = "Plugins";

		public static void Main(string[] args) {
			try {
				IFinder<ICommand> cmdFinder = InitializeCommandFinder();
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

		private static IFinder<ICommand> InitializeCommandFinder() {
			string[] filePaths = Directory.GetFiles(DLL_DIR, "*.dll", SearchOption.AllDirectories);
			return new CommandFinder(filePaths);
		}

	}

}