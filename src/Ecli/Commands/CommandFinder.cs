using Ecli.Contracts;
using Microsoft.Extensions.DependencyModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;

namespace Ecli.Commands {

	public class CommandFinder : IFinder<ICommand> {

		private string _dllName;

		public CommandFinder(string dllName) => _dllName = dllName;

		public ICommand[] FindAll() {
			Assembly assembly = Assembly.LoadFrom(_dllName);
			IEnumerable<Type> commandTypes = assembly.DefinedTypes
				.Where(ti => ti.ImplementedInterfaces.Contains(typeof(ICommand)))
				.Select(ti => ti.AsType());

			return commandTypes
				.Select(t => (ICommand)Activator.CreateInstance(t))
				.ToArray();
		}

		private IEnumerable<string> GetDllFileNames(string directoryPath) {
			DirectoryInfo info = new DirectoryInfo(directoryPath);
			FileInfo[] foundFiles = info.GetFiles("*.dll");
			return foundFiles.Select(f => f.FullName);
		}

	}

}