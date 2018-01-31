using Ecli.Commands;
using Ecli.FileReaders;
using Ecli.FileReaders.SettingsFileReaders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Ecli {

	public class EntryPoint {

		private const string DLL_DIR = "Plugins";

		public static void Main(string[] args) {
			try {
				string rootDir = new FileInfo($"{Assembly.GetEntryAssembly().Location}").DirectoryName;
				string pluginDir = $"{rootDir}\\{DLL_DIR}";
				if (!Directory.Exists(pluginDir)) Directory.CreateDirectory(pluginDir);
				IFinder<ICommand> cmdFinder = InitializeCommandFinder(pluginDir);
				IFileReader fileReader = new SettingsFileReader(new IFinder<ICommand>[] { cmdFinder });
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

		private static IFinder<ICommand> InitializeCommandFinder(string rootDir) {
			string[] filePaths = Directory.GetFiles(rootDir, "*.dll", SearchOption.AllDirectories);
			return new CommandFinder(filePaths);
		}

	}

}