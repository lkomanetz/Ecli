using System;

namespace Ecli {

	public interface IConsolePage {

		Guid Id { get; }

		void Show();

	}

}