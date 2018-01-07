using Ecli.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Ecli.Commands {

	public class CommandFinder : IFinder<ICommand> {

		public ICommand[] FindAll() {
			var assemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
			IEnumerable<Type> commandTypes = assemblies.SelectMany(x => x.DefinedTypes)
				.Where(ti => ti.ImplementedInterfaces.Contains(typeof(ICommand)))
				.Select(ti => ti.AsType());

			return commandTypes
				.Select(t => (ICommand)Activator.CreateInstance(t))
				.ToArray();
		}

	}

}