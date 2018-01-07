using System;

namespace Ecli.Contracts {

	public interface IFinder<T> {

		T[] FindAll();

	}

}