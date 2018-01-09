using System;

namespace Ecli {

	public interface IFinder<T> {

		T[] FindAll();

		T Find<TInput>() where TInput : T;

	}

}