using System;

namespace Ecli.Contracts {

	public interface IConsolePage {

		Guid Id { get; }

		void Show();

	}

}