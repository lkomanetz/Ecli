using Microsoft.Extensions.DependencyModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;

namespace Ecli.Commands {

	public class CommandFinder : IFinder<ICommand> {

		private string[] _dllNames;

		public CommandFinder(string dllName) => _dllNames = new string[] { dllName };
		public CommandFinder(string[] dllNames) => _dllNames = dllNames;

		public CommandFinder(Assembly assembly) =>
			_dllNames = new string[] { Path.GetFileName(assembly.Location) };

		public CommandFinder(Assembly[] assemblies) =>
			_dllNames = assemblies.Select(a => Path.GetFileName(a.Location)).ToArray();

		public ICommand[] FindAll() {
			IEnumerable<Assembly> assemblies = _dllNames.Select(s => Assembly.LoadFrom(s));
			IEnumerable<Type> commandTypes = assemblies.SelectMany(a => a.DefinedTypes)
				.Where(ti => ti.ImplementedInterfaces.Contains(typeof(ICommand)))
				.Select(ti => ti.AsType());

			return commandTypes
				.Select(t => (ICommand)Activator.CreateInstance(t))
				.ToArray();
		}

		public ICommand Find<T>() where T : ICommand =>
			FindAll().Where(c => c.GetType() == typeof(T)).SingleOrDefault();
		
	}

}