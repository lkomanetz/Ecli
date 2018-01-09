using Ecli.Contracts;
using Ecli.FileReaders.SettingsFileReaders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Ecli.FileReaders {

	// TODO(Logan) -> Look at possibly removing this class.  I don't think it is needed because ICommand has an ISettingsReader property on it.
	public class SettingsReaderFinder : IFinder<ISettingsReader> {

		public ISettingsReader[] FindAll() {
			var assemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
			IEnumerable<Type> commandTypes = assemblies.SelectMany(x => x.DefinedTypes)
				.Where(ti => ti.ImplementedInterfaces.Contains(typeof(ISettingsReader)))
				.Select(ti => ti.AsType());

			return commandTypes
				.Select(t => (ISettingsReader)Activator.CreateInstance(t))
				.ToArray();
		}

		public ISettingsReader Find<T>() where T : ISettingsReader =>
			FindAll().Where(s => s.GetType() == typeof(T)).SingleOrDefault();

	}

}