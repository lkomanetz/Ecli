using System;

namespace Ecli.Contracts {

	public interface IFinder<T> {

		T[] FindAll();

		T Find<TInput>() where TInput : T;

	}

}