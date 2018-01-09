using Ecli.Contracts;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace Ecli.Parsers {

	public class ParserFinder : IFinder<IParser> {

		public IParser[] FindAll() {
			var assemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
			IEnumerable<Type> parserTypes = assemblies.SelectMany(x => x.DefinedTypes)
				.Where(ti =>
					ti.ImplementedInterfaces.Contains(typeof(IParser)) &&
					ti.AsType() != typeof(ArgumentParser)
				)
				.Select(ti => ti.AsType());

			return parserTypes
				.Select(t => (IParser)Activator.CreateInstance(t))
				.ToArray();
		}

		public IParser Find<T>() where T : IParser =>
			FindAll().Where(p => p.GetType() == typeof(T)).SingleOrDefault();

	}

}